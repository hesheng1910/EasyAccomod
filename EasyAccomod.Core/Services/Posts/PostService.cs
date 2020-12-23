using EasyAccomod.Core.Entities;
using EasyAccomod.Core.Model.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using EasyAccomod.Core.EF;
using AGID.Core.Exceptions;
using Microsoft.AspNetCore.Identity;
using EasyAccomod.Core.Common;
using EasyAccomod.Core.Enums;

namespace EasyAccomod.Core.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly EasyAccDbContext context;
        private readonly UserManager<AppUser> userManager;

        public PostService(EasyAccDbContext context, UserManager<AppUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<bool> CheckUserAndRole(long accessId, string role)
        {
            var user = await userManager.FindByIdAsync(accessId.ToString());
            if (user == null) throw new ServiceException("Tài khoản không tồn tại");

            return await userManager.IsInRoleAsync(user, role);
        }
        public async Task<PostViewModel> AddPost(AddPostModel model )
        {
            var imgs = "";
            if (model.fileimgs.Count() < 3) throw new ServiceException("Cần upload ít nhất 3 ảnh");
            foreach (var img in model.fileimgs)
            {
                var filePath = @"Content/img_2/" + img.FileName;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                    imgs += filePath + ";";
                }
            }
            var roomcategory = context.RoomCategories.Where(x => x.RoomCategoryName == model.RoomCategoryName).FirstOrDefault();
            if (roomcategory == null) throw new ServiceException("Loại phòng không tồn tại");
            var user = await userManager.FindByIdAsync(model.UserId.ToString());
            if ((model.ExpireTime - DateTime.Now).Days < 7) throw new ServiceException("Thời gian bài đăng có hiệu lực ít nhất 1 tuần"); 
            Infrastructure infrastructure = new Infrastructure()
            {
                AirCond = model.AirCond,
                Balcony = model.Balcony,
                Bath = model.Bath,
                ElecPrice = model.ElecPrice,
                Fridge = model.Fridge,
                Kitchen = model.Kitchen,
                WaterHeater = model.WaterHeater,
                WaterPrice = model.WaterPrice
            };
            await context.Infrastructures.AddAsync(infrastructure);
            await context.SaveChangesAsync();
            var education = "";
            var medical = "";
            var busstation = "";
            foreach(var s in model.Educations)
            {
                education += s + ";";
            }
            foreach (var s in model.Medicals)
            {
                medical += s + ";";
            }
            foreach (var s in model.BusStations)
            {
                busstation += s + ";";
            }
            AddressNearBy addressNearBy = new AddressNearBy()
            {
                Medical = medical,
                Education = education,
                BusStation = busstation
            };
            await context.AddressNearBies.AddAsync(addressNearBy);
            await context.SaveChangesAsync();
            Post post = new Post()
            {
                UserId = model.UserId,
                City = model.City,
                District = model.District,
                Street = model.Street,
                AddressNearById = context.AddressNearBies.OrderBy(x => x.Id).Last().Id,
                RoomCategoryId = (RoomCategoryEnum)roomcategory.Id,
                Price = model.Price,
                Area = model.Area,
                InfrastructureId = context.Infrastructures.OrderBy(x => x.Id).Last().Id,
                Images = imgs,
                Hired = model.Hired,
                ExpireTime = model.ExpireTime,
                TotalLike = 0,
                TotalView = 0,
                PostStatus = PostStatusEnum.Request
            };
            
            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
            PostViewModel postVM = new PostViewModel()
            {
                PostId = context.Posts.OrderBy(p => p.PostId).Last().PostId,
                City = model.City,
                District = model.District,
                Street = model.Street,
                AddressNearBy = addressNearBy,
                RoomCategoryId = (RoomCategoryEnum)roomcategory.Id,
                Price = model.Price,
                Area = model.Area,
                Infrastructure = infrastructure,
                Images = imgs,
                Hired = model.Hired,
                Contact = user.PhoneNumber,
                FullNameOwner = user.LastName + " " + user.FirstName,
                EmailOwner = user.Email,
                ExpireTime = model.ExpireTime,
                TotalLike = 0,
                TotalView = 0,
                PostStatus = post.PostStatus
            };
            return postVM;
        }
        public async Task<PostViewModel> AddPostForMod(AddPostModel model)
        {
            var imgs = "";
            if (model.fileimgs.Count() < 3) throw new ServiceException("Cần upload ít nhất 3 ảnh");
            foreach (var img in model.fileimgs)
            {
                var filePath = @"Content/img/" + img.FileName;
                var stream = new FileStream(filePath, FileMode.Create);
                await img.CopyToAsync(stream);
                imgs += filePath + ";";
            }
            var roomcategory = context.RoomCategories.Where(x => x.RoomCategoryName == model.RoomCategoryName).FirstOrDefault();
            if (roomcategory == null) throw new ServiceException("Loại phòng không tồn tại");
            if ((model.ExpireTime - DateTime.Now).Days < 7) throw new ServiceException("Thời gian bài đăng có hiệu lực ít nhất 1 tuần");
            var user = await userManager.FindByIdAsync(model.UserId.ToString());
            Infrastructure infrastructure = new Infrastructure()
            {
                AirCond = model.AirCond,
                Balcony = model.Balcony,
                Bath = model.Bath,
                ElecPrice = model.ElecPrice,
                Fridge = model.Fridge,
                Kitchen = model.Kitchen,
                WaterHeater = model.WaterHeater,
                WaterPrice = model.WaterPrice
            };
            await context.Infrastructures.AddAsync(infrastructure);
            await context.SaveChangesAsync();
            var education = "";
            var medical = "";
            var busstation = "";
            foreach (var s in model.Educations)
            {
                education += s + ";";
            }
            foreach (var s in model.Medicals)
            {
                medical += s + ";";
            }
            foreach (var s in model.BusStations)
            {
                busstation += s + ";";
            }
            AddressNearBy addressNearBy = new AddressNearBy()
            {
                Medical = medical,
                Education = education,
                BusStation = busstation
            };
            Post post = new Post()
            {
                UserId = model.UserId,
                City = model.City,
                District = model.District,
                Street = model.Street,
                AddressNearById = context.AddressNearBies.OrderBy(x => x.Id).Last().Id,
                RoomCategoryId = (RoomCategoryEnum)roomcategory.Id,
                Price = model.Price,
                Area = model.Area,
                InfrastructureId = context.Infrastructures.OrderBy(x => x.Id).Last().Id,
                Images = imgs,
                Hired = model.Hired,
                ExpireTime = model.ExpireTime,
                TotalLike = 0,
                TotalView = 0,
                PostStatus = PostStatusEnum.Accepted

            };
            await context.Infrastructures.AddAsync(infrastructure);
            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
            PostViewModel postVM = new PostViewModel()
            {
                PostId = context.Posts.OrderBy(p => p.PostId).Last().PostId,
                City = model.City,
                District = model.District,
                Street = model.Street,
                AddressNearBy = addressNearBy,
                RoomCategoryId = (RoomCategoryEnum)roomcategory.Id,
                Price = model.Price,
                Area = model.Area,
                Infrastructure = infrastructure,
                Images = imgs,
                Hired = model.Hired,
                Contact = user.PhoneNumber,
                FullNameOwner = user.LastName + " " + user.FirstName,
                EmailOwner = user.Email,
                ExpireTime = model.ExpireTime,
                TotalLike = 0,
                TotalView = 0,
                PostStatus = post.PostStatus
            };
            return postVM;
        }
        public List<PostViewModel> GetAllPost()
        {
            var posts = context.Infrastructures.Join(context.Posts, i => i.Id, p => p.InfrastructureId,
                                                (i, p) => new
                                                {
                                                    p.PostId,
                                                    i.AirCond,
                                                    i.Balcony,
                                                    i.Bath,
                                                    i.ElecPrice,
                                                    i.Fridge,
                                                    i.Kitchen,
                                                    i.WaterHeater,
                                                    i.WaterPrice,
                                                    p.UserId,
                                                    p.AddressNearById,
                                                    p.Area,
                                                    p.City,
                                                    p.Description,
                                                    p.District,
                                                    p.ExpireTime,
                                                    p.Hired,
                                                    p.Images,
                                                    p.InfrastructureId,
                                                    p.IsDetele,
                                                    p.PostStatus,
                                                    p.Price,
                                                    p.RoomCategoryId,
                                                    p.Rooms,
                                                    p.Street,
                                                    p.TotalLike,
                                                    p.TotalView,
                                                    p.WithOwner
                                                }).Join(context.AddressNearBies, i => i.AddressNearById, a => a.Id,
                                                (i, a) => new
                                                {
                                                    i.PostId,
                                                    i.AirCond,
                                                    i.Balcony,
                                                    i.Bath,
                                                    i.ElecPrice,
                                                    i.Fridge,
                                                    i.Kitchen,
                                                    i.WaterHeater,
                                                    i.WaterPrice,
                                                    i.UserId,
                                                    a.Medical,
                                                    a.BusStation,
                                                    a.Education,
                                                    i.Area,
                                                    i.City,
                                                    i.Description,
                                                    i.District,
                                                    i.ExpireTime,
                                                    i.Hired,
                                                    i.Images,
                                                    i.InfrastructureId,
                                                    i.IsDetele,
                                                    i.PostStatus,
                                                    i.Price,
                                                    i.RoomCategoryId,
                                                    i.Rooms,
                                                    i.Street,
                                                    i.TotalLike,
                                                    i.TotalView,
                                                    i.WithOwner
                                                }).Join(userManager.Users, i => i.UserId, u => u.Id,
                                                    (i, u) => new
                                                    {
                                                        i.PostId,
                                                        i.AirCond,
                                                        i.Balcony,
                                                        i.Bath,
                                                        i.ElecPrice,
                                                        i.Fridge,
                                                        i.Kitchen,
                                                        i.WaterHeater,
                                                        i.WaterPrice,
                                                        i.Medical,
                                                        i.BusStation,
                                                        i.Education,
                                                        u.PhoneNumber,
                                                        u.LastName,
                                                        u.FirstName,
                                                        u.Email,
                                                        i.Area,
                                                        i.City,
                                                        i.Description,
                                                        i.District,
                                                        i.ExpireTime,
                                                        i.Hired,
                                                        i.Images,
                                                        i.InfrastructureId,
                                                        i.IsDetele,
                                                        i.PostStatus,
                                                        i.Price,
                                                        i.RoomCategoryId,
                                                        i.Rooms,
                                                        i.Street,
                                                        i.TotalLike,
                                                        i.TotalView,
                                                        i.WithOwner
                                                    }).Where(p => p.IsDetele == false && p.PostStatus == PostStatusEnum.Accepted && p.ExpireTime < DateTime.Now);
            List<PostViewModel> models = new List<PostViewModel>();
            foreach(var post in posts)
            {
                Infrastructure infrastructure = new Infrastructure
                {
                    AirCond = post.AirCond,
                    Balcony = post.Balcony,
                    Bath = post.Bath,
                    ElecPrice = post.ElecPrice,
                    WaterPrice = post.WaterPrice,
                    Fridge = post.Fridge,
                    Kitchen = post.Kitchen,
                    WaterHeater = post.WaterHeater
                };
                AddressNearBy addressNearBy = new AddressNearBy()
                {
                    Medical = post.Medical,
                    BusStation = post.BusStation,
                    Education = post.Education,
                };
                PostViewModel model = new PostViewModel()
                {
                    PostId = post.PostId,
                    City = post.City,
                    District = post.District,
                    Street = post.Street,
                    AddressNearBy = addressNearBy,
                    Price = post.Price,
                    Area = post.Area,
                    Images = post.Images,
                    Hired = post.Hired,
                    Infrastructure = infrastructure,
                    Contact = post.PhoneNumber,
                    FullNameOwner = post.LastName + " " + post.FirstName,
                    EmailOwner = post.Email,
                    ExpireTime = post.ExpireTime,
                    TotalLike = post.TotalLike,
                    TotalView = post.TotalView,
                    PostStatus = post.PostStatus,
                    RoomCategoryId = post.RoomCategoryId
                };
                models.Add(model);
            }
            return models;                                                
        }
        public List<PostViewModel> GetAllPostForOwner()
        {
            var posts = context.Infrastructures.Join(context.Posts, i => i.Id, p => p.InfrastructureId,
                                                (i, p) => new
                                                {
                                                    p.PostId,
                                                    i.AirCond,
                                                    i.Balcony,
                                                    i.Bath,
                                                    i.ElecPrice,
                                                    i.Fridge,
                                                    i.Kitchen,
                                                    i.WaterHeater,
                                                    i.WaterPrice,
                                                    p.UserId,
                                                    p.AddressNearById,
                                                    p.Area,
                                                    p.City,
                                                    p.Description,
                                                    p.District,
                                                    p.ExpireTime,
                                                    p.Hired,
                                                    p.Images,
                                                    p.InfrastructureId,
                                                    p.IsDetele,
                                                    p.PostStatus,
                                                    p.Price,
                                                    p.RoomCategoryId,
                                                    p.Rooms,
                                                    p.Street,
                                                    p.TotalLike,
                                                    p.TotalView,
                                                    p.WithOwner
                                                }).Join(context.AddressNearBies, i => i.AddressNearById, a => a.Id,
                                                (i, a) => new
                                                {
                                                    i.PostId,
                                                    i.AirCond,
                                                    i.Balcony,
                                                    i.Bath,
                                                    i.ElecPrice,
                                                    i.Fridge,
                                                    i.Kitchen,
                                                    i.WaterHeater,
                                                    i.WaterPrice,
                                                    i.UserId,
                                                    a.Medical,
                                                    a.BusStation,
                                                    a.Education,
                                                    i.Area,
                                                    i.City,
                                                    i.Description,
                                                    i.District,
                                                    i.ExpireTime,
                                                    i.Hired,
                                                    i.Images,
                                                    i.InfrastructureId,
                                                    i.IsDetele,
                                                    i.PostStatus,
                                                    i.Price,
                                                    i.RoomCategoryId,
                                                    i.Rooms,
                                                    i.Street,
                                                    i.TotalLike,
                                                    i.TotalView,
                                                    i.WithOwner
                                                }).Join(userManager.Users, i => i.UserId, u => u.Id,
                                                    (i, u) => new
                                                    {
                                                        i.PostId,
                                                        i.AirCond,
                                                        i.Balcony,
                                                        i.Bath,
                                                        i.ElecPrice,
                                                        i.Fridge,
                                                        i.Kitchen,
                                                        i.WaterHeater,
                                                        i.WaterPrice,
                                                        i.Medical,
                                                        i.BusStation,
                                                        i.Education,
                                                        u.PhoneNumber,
                                                        u.LastName,
                                                        u.FirstName,
                                                        u.Email,
                                                        i.Area,
                                                        i.City,
                                                        i.Description,
                                                        i.District,
                                                        i.ExpireTime,
                                                        i.Hired,
                                                        i.Images,
                                                        i.InfrastructureId,
                                                        i.IsDetele,
                                                        i.PostStatus,
                                                        i.Price,
                                                        i.RoomCategoryId,
                                                        i.Rooms,
                                                        i.Street,
                                                        i.TotalLike,
                                                        i.TotalView,
                                                        i.WithOwner
                                                    }).Where(p => p.IsDetele == false);
            List<PostViewModel> models = new List<PostViewModel>();
            foreach (var post in posts)
            {
                Infrastructure infrastructure = new Infrastructure
                {
                    AirCond = post.AirCond,
                    Balcony = post.Balcony,
                    Bath = post.Bath,
                    ElecPrice = post.ElecPrice,
                    WaterPrice = post.WaterPrice,
                    Fridge = post.Fridge,
                    Kitchen = post.Kitchen,
                    WaterHeater = post.WaterHeater
                };
                AddressNearBy addressNearBy = new AddressNearBy()
                {
                    Medical = post.Medical,
                    BusStation = post.BusStation,
                    Education = post.Education,
                };
                PostViewModel model = new PostViewModel()
                {
                    PostId = post.PostId,
                    City = post.City,
                    District = post.District,
                    Street = post.Street,
                    AddressNearBy = addressNearBy,
                    Price = post.Price,
                    Area = post.Area,
                    Images = post.Images,
                    Hired = post.Hired,
                    Infrastructure = infrastructure,
                    Contact = post.PhoneNumber,
                    FullNameOwner = post.LastName + " " + post.FirstName,
                    EmailOwner = post.Email,
                    ExpireTime = post.ExpireTime,
                    TotalLike = post.TotalLike,
                    TotalView = post.TotalView,
                    PostStatus = post.PostStatus,
                    RoomCategoryId = post.RoomCategoryId
                };
                models.Add(model);
            }
            return models;


        }
        public async Task<PostViewModel> ViewPost(long postId)
        {
            var post = context.Posts.Where(x => x.PostId == postId && x.ExpireTime < DateTime.Now && x.PostStatus == PostStatusEnum.Accepted).FirstOrDefault();
            if (post == null) throw new ServiceException("Bài đăng không tồn tại");
            var user = await userManager.FindByIdAsync(post.UserId.ToString());
            var infrastructure = context.Infrastructures.Where(x => x.Id == post.InfrastructureId).FirstOrDefault();
            var addressNearBy = context.AddressNearBies.Where(x => x.Id == post.AddressNearById).FirstOrDefault();
            PostViewModel model = new PostViewModel()
            {
                PostId = post.PostId,
                City = post.City,
                District = post.District,
                Street = post.Street,
                AddressNearBy = addressNearBy,
                Price = post.Price,
                Area = post.Area,
                Images = post.Images,
                Hired = post.Hired,
                Infrastructure = infrastructure,
                Contact = user.PhoneNumber,
                FullNameOwner = user.LastName + " " + user.FirstName,
                EmailOwner = user.Email,
                ExpireTime = post.ExpireTime,
                TotalLike = post.TotalLike,
                TotalView = post.TotalView+1,
                PostStatus = post.PostStatus,
                RoomCategoryId = post.RoomCategoryId
            };
            post.TotalView += 1;
            context.Posts.Update(post);
            await context.SaveChangesAsync();
            return model;
        }

        public async Task<PostViewModel> UpdatePost(long postId, UpdatePostModel model)
        {
            var post = context.Posts.Where(x => x.PostId == postId && x.PostStatus == PostStatusEnum.Request && x.IsDetele == false).FirstOrDefault();
            if (post == null) throw new ServiceException("Bài đăng không tồn tại hoặc đã được confirm");
            var user = await userManager.FindByIdAsync(post.UserId.ToString());
            if (model.fileimgs.Count() < 3) throw new ServiceException("Cần upload ít nhất 3 ảnh");
            if ((model.ExpireTime - DateTime.Now).Days < 7) throw new ServiceException("Thời gian bài đăng có hiệu lực ít nhất 1 tuần");
            foreach (var img in model.fileimgs)
            {
                var filePath = @"Content/img/" + img.FileName;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                    post.Images += filePath + ";";
                }    
                    
            }
            var roomcategory = context.RoomCategories.Where(x => x.RoomCategoryName == model.RoomCategoryName).FirstOrDefault();
            if (roomcategory == null) throw new ServiceException("Loại phòng không tồn tại");
            var infrastructure = context.Infrastructures.Where(x => x.Id == post.InfrastructureId).First();
            var addressNearBy = context.AddressNearBies.Where(x => x.Id == post.AddressNearById).First();
            infrastructure.AirCond = model.AirCond;
            infrastructure.Balcony = model.Balcony;
            infrastructure.Bath = model.Bath;
            infrastructure.ElecPrice = model.ElecPrice;
            infrastructure.WaterHeater = model.WaterHeater;
            infrastructure.WaterPrice = model.WaterPrice;
            infrastructure.Fridge = model.Fridge;
            infrastructure.Kitchen = model.Kitchen;
            var education = "";
            var medical = "";
            var busstation = "";
            foreach (var s in model.Educations)
            {
                education += s + ";";
            }
            foreach (var s in model.Medicals)
            {
                medical += s + ";";
            }
            foreach (var s in model.BusStations)
            {
                busstation += s + ";";
            }
            addressNearBy.Medical = medical;
            addressNearBy.BusStation = busstation;
            addressNearBy.Education = education;
            post.City = model.City;
            post.District = model.District;
            post.Street = model.Street;
            post.Price = model.Price;
            post.Area = model.Area;
            post.Hired = model.Hired;
            post.ExpireTime = model.ExpireTime;
            context.AddressNearBies.Update(addressNearBy);
            context.Infrastructures.Update(infrastructure);
            context.Posts.Update(post);
            await context.SaveChangesAsync();
            PostViewModel viewModel = new PostViewModel()
            {
                City = post.City,
                District = post.District,
                Street = post.Street,
                AddressNearBy = addressNearBy,
                Price = post.Price,
                Area = post.Area,
                Images = post.Images,
                Hired = post.Hired,
                Infrastructure = infrastructure,
                Contact = user.PhoneNumber,
                FullNameOwner = user.LastName + " " + user.FirstName,
                EmailOwner = user.Email,
                ExpireTime = post.ExpireTime,
                TotalLike = post.TotalLike,
                TotalView = post.TotalView,
                PostStatus = post.PostStatus,
                RoomCategoryId = post.RoomCategoryId,
                PostId = post.PostId
            };
            return viewModel;
        }
        public async Task<Post> UpdateStatusPost(long userId, long postId, bool hired)
        {
            var post = context.Posts.Where(x => x.PostId == postId && x.ExpireTime < DateTime.Now && x.PostStatus == PostStatusEnum.Accepted && x.UserId == userId).FirstOrDefault();
            if (post == null) throw new ServiceException("Bài đăng không tồn tại");
            var user = await userManager.FindByIdAsync(userId.ToString());
            post.Hired = hired;
            context.Posts.Update(post);
            string content;
            if (hired) content = CommonConstants.HAS_HIRED;
            else content = CommonConstants.NO_TENANTS;
            Notification notification = new Notification()
            {
                UserName = user.UserName,
                PostId = postId,
                Content = content,
                NotifTime = DateTime.UtcNow,
                OfMod = true,
                IsDelete = false
            };
            await context.Notifications.AddAsync(notification);
            await context.SaveChangesAsync();
            return post;
        }
        public async Task<bool> LikePost(long postId,long userId)
        {
            var post = context.Posts.Where(x => x.PostId == postId && x.ExpireTime < DateTime.Now && x.PostStatus == PostStatusEnum.Accepted).FirstOrDefault();
            if (post == null) throw new ServiceException("Bài đăng không tồn tại");
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new ServiceException("Tài khoản không tồn tại");
            var userLikePost = context.UserLikePosts.Where(x => x.PostId == postId && x.UserId == userId).FirstOrDefault();
            if(userLikePost != null)
            {
                context.UserLikePosts.Remove(userLikePost);
                post.TotalLike -= 1;
                context.Posts.Update(post);
                await context.SaveChangesAsync();
                return false;
            }
            userLikePost = new UserLikePost()
            {
                PostId = postId,
                UserId = userId
            };
            await context.UserLikePosts.AddAsync(userLikePost);
            post.TotalLike += 1;
            context.Posts.Update(post);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<List<PostViewModel>> GetFavouritePosts(long userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new ServiceException("Tài khoản không tồn tại");
            var userLikePosts = context.UserLikePosts.Where(x => x.UserId == userId);
            var posts = context.Infrastructures.Join(context.Posts, i => i.Id, p => p.InfrastructureId,
                                                (i, p) => new
                                                {
                                                    p.PostId,
                                                    i.AirCond,
                                                    i.Balcony,
                                                    i.Bath,
                                                    i.ElecPrice,
                                                    i.Fridge,
                                                    i.Kitchen,
                                                    i.WaterHeater,
                                                    i.WaterPrice,
                                                    p.UserId,
                                                    p.AddressNearById,
                                                    p.Area,
                                                    p.City,
                                                    p.Description,
                                                    p.District,
                                                    p.ExpireTime,
                                                    p.Hired,
                                                    p.Images,
                                                    p.InfrastructureId,
                                                    p.IsDetele,
                                                    p.PostStatus,
                                                    p.Price,
                                                    p.RoomCategoryId,
                                                    p.Rooms,
                                                    p.Street,
                                                    p.TotalLike,
                                                    p.TotalView,
                                                    p.WithOwner
                                                }).Join(context.AddressNearBies, i => i.AddressNearById, a => a.Id,
                                                (i, a) => new
                                                {
                                                    i.PostId,
                                                    i.AirCond,
                                                    i.Balcony,
                                                    i.Bath,
                                                    i.ElecPrice,
                                                    i.Fridge,
                                                    i.Kitchen,
                                                    i.WaterHeater,
                                                    i.WaterPrice,
                                                    i.UserId,
                                                    a.Medical,
                                                    a.BusStation,
                                                    a.Education,
                                                    i.Area,
                                                    i.City,
                                                    i.Description,
                                                    i.District,
                                                    i.ExpireTime,
                                                    i.Hired,
                                                    i.Images,
                                                    i.InfrastructureId,
                                                    i.IsDetele,
                                                    i.PostStatus,
                                                    i.Price,
                                                    i.RoomCategoryId,
                                                    i.Rooms,
                                                    i.Street,
                                                    i.TotalLike,
                                                    i.TotalView,
                                                    i.WithOwner
                                                }).Join(userManager.Users, i => i.UserId, u => u.Id,
                                                    (i, u) => new
                                                    {
                                                        i.PostId,
                                                        i.AirCond,
                                                        i.Balcony,
                                                        i.Bath,
                                                        i.ElecPrice,
                                                        i.Fridge,
                                                        i.Kitchen,
                                                        i.WaterHeater,
                                                        i.WaterPrice,
                                                        i.Medical,
                                                        i.BusStation,
                                                        i.Education,
                                                        u.PhoneNumber,
                                                        u.LastName,
                                                        u.FirstName,
                                                        u.Email,
                                                        i.Area,
                                                        i.City,
                                                        i.Description,
                                                        i.District,
                                                        i.ExpireTime,
                                                        i.Hired,
                                                        i.Images,
                                                        i.InfrastructureId,
                                                        i.IsDetele,
                                                        i.PostStatus,
                                                        i.Price,
                                                        i.RoomCategoryId,
                                                        i.Rooms,
                                                        i.Street,
                                                        i.TotalLike,
                                                        i.TotalView,
                                                        i.WithOwner
                                                    }).Join(context.UserLikePosts, i => i.PostId, u => u.PostId,
                                                    (i, u) => new
                                                    {
                                                        i.PostId,
                                                        i.AirCond,
                                                        i.Balcony,
                                                        i.Bath,
                                                        i.ElecPrice,
                                                        i.Fridge,
                                                        i.Kitchen,
                                                        i.WaterHeater,
                                                        i.WaterPrice,
                                                        i.Medical,
                                                        i.BusStation,
                                                        i.Education,
                                                        i.PhoneNumber,
                                                        i.LastName,
                                                        i.FirstName,
                                                        i.Email,
                                                        i.Area,
                                                        i.City,
                                                        i.Description,
                                                        i.District,
                                                        i.ExpireTime,
                                                        i.Hired,
                                                        i.Images,
                                                        i.InfrastructureId,
                                                        i.IsDetele,
                                                        i.PostStatus,
                                                        i.Price,
                                                        i.RoomCategoryId,
                                                        i.Rooms,
                                                        i.Street,
                                                        i.TotalLike,
                                                        i.TotalView,
                                                        i.WithOwner,
                                                        UserRenterId = u.UserId
                                                    }).Where(p => p.IsDetele == false && p.PostStatus == PostStatusEnum.Accepted && p.UserRenterId == userId && p.ExpireTime < DateTime.Now);
            List<PostViewModel> models = new List<PostViewModel>();
            foreach (var post in posts)
            {
                Infrastructure infrastructure = new Infrastructure
                {
                    AirCond = post.AirCond,
                    Balcony = post.Balcony,
                    Bath = post.Bath,
                    ElecPrice = post.ElecPrice,
                    WaterPrice = post.WaterPrice,
                    Fridge = post.Fridge,
                    Kitchen = post.Kitchen,
                    WaterHeater = post.WaterHeater
                };
                AddressNearBy addressNearBy = new AddressNearBy()
                {
                    Medical = post.Medical,
                    BusStation = post.BusStation,
                    Education = post.Education,
                };
                PostViewModel model = new PostViewModel()
                {
                    PostId = post.PostId,
                    City = post.City,
                    District = post.District,
                    Street = post.Street,
                    AddressNearBy = addressNearBy,
                    Price = post.Price,
                    Area = post.Area,
                    Images = post.Images,
                    Hired = post.Hired,
                    Infrastructure = infrastructure,
                    Contact = post.PhoneNumber,
                    FullNameOwner = post.LastName + " " + post.FirstName,
                    EmailOwner = post.Email,
                    ExpireTime = post.ExpireTime,
                    TotalLike = post.TotalLike,
                    TotalView = post.TotalView,
                    PostStatus = post.PostStatus,
                    RoomCategoryId = post.RoomCategoryId
                };
                models.Add(model);
            }
            return models;
        }
        public async Task<Post> ConfirmPost(long postId)
        {
            var post = context.Posts.Where(x => x.PostStatus == PostStatusEnum.Request && x.PostId == postId && x.IsDetele == false).FirstOrDefault();
            if (post == null) throw new ServiceException("Bai dang khong ton tai");
            var user = userManager.Users.Where(x => x.Id == post.UserId && x.IsConfirm).FirstOrDefault();
            Notification notification = new Notification()
            {
                UserName = user.UserName,
                PostId = post.PostId,
                Content = CommonConstants.POST_ACCEPTED,
                NotifTime = DateTime.UtcNow,
                OfMod = false,
                IsDelete = false
            };
            await context.Notifications.AddAsync(notification);
            post.PostStatus = PostStatusEnum.Accepted;
            context.Posts.Update(post);
            await context.SaveChangesAsync();
            return post;
        }
        public async Task<Post> RejectPost(long postId)
        {
            var post = context.Posts.Where(x => x.PostStatus == PostStatusEnum.Request && x.PostId == postId).FirstOrDefault();
            if (post == null) throw new ServiceException("Bai dang khong ton tai");
            var user = userManager.Users.Where(x => x.Id == post.UserId && x.IsConfirm).FirstOrDefault();
            Notification notification = new Notification()
            {
                UserName = user.UserName,
                PostId = post.PostId,
                Content = CommonConstants.POST_REJECTED,
                NotifTime = DateTime.UtcNow,
                OfMod = false,
                IsDelete = false
            };
            await context.Notifications.AddAsync(notification);
            post.PostStatus = PostStatusEnum.Rejected;
            context.Posts.Update(post);
            await context.SaveChangesAsync();
            return post;
        }
        public List<PostViewModel> GetPostsNeedConfirm()
        {
            var posts = context.Infrastructures.Join(context.Posts, i => i.Id, p => p.InfrastructureId,
                                                (i, p) => new
                                                {
                                                    p.PostId,
                                                    i.AirCond,
                                                    i.Balcony,
                                                    i.Bath,
                                                    i.ElecPrice,
                                                    i.Fridge,
                                                    i.Kitchen,
                                                    i.WaterHeater,
                                                    i.WaterPrice,
                                                    p.UserId,
                                                    p.AddressNearById,
                                                    p.Area,
                                                    p.City,
                                                    p.Description,
                                                    p.District,
                                                    p.ExpireTime,
                                                    p.Hired,
                                                    p.Images,
                                                    p.InfrastructureId,
                                                    p.IsDetele,
                                                    p.PostStatus,
                                                    p.Price,
                                                    p.RoomCategoryId,
                                                    p.Rooms,
                                                    p.Street,
                                                    p.TotalLike,
                                                    p.TotalView,
                                                    p.WithOwner
                                                }).Join(context.AddressNearBies, i => i.AddressNearById, a => a.Id,
                                                (i, a) => new
                                                {
                                                    i.PostId,
                                                    i.AirCond,
                                                    i.Balcony,
                                                    i.Bath,
                                                    i.ElecPrice,
                                                    i.Fridge,
                                                    i.Kitchen,
                                                    i.WaterHeater,
                                                    i.WaterPrice,
                                                    i.UserId,
                                                    a.Medical,
                                                    a.BusStation,
                                                    a.Education,
                                                    i.Area,
                                                    i.City,
                                                    i.Description,
                                                    i.District,
                                                    i.ExpireTime,
                                                    i.Hired,
                                                    i.Images,
                                                    i.InfrastructureId,
                                                    i.IsDetele,
                                                    i.PostStatus,
                                                    i.Price,
                                                    i.RoomCategoryId,
                                                    i.Rooms,
                                                    i.Street,
                                                    i.TotalLike,
                                                    i.TotalView,
                                                    i.WithOwner
                                                }).Join(userManager.Users, i => i.UserId, u => u.Id,
                                                    (i, u) => new
                                                    {
                                                        i.PostId,
                                                        i.AirCond,
                                                        i.Balcony,
                                                        i.Bath,
                                                        i.ElecPrice,
                                                        i.Fridge,
                                                        i.Kitchen,
                                                        i.WaterHeater,
                                                        i.WaterPrice,
                                                        i.Medical,
                                                        i.BusStation,
                                                        i.Education,
                                                        u.PhoneNumber,
                                                        u.LastName,
                                                        u.FirstName,
                                                        u.Email,
                                                        i.Area,
                                                        i.City,
                                                        i.Description,
                                                        i.District,
                                                        i.ExpireTime,
                                                        i.Hired,
                                                        i.Images,
                                                        i.InfrastructureId,
                                                        i.IsDetele,
                                                        i.PostStatus,
                                                        i.Price,
                                                        i.RoomCategoryId,
                                                        i.Rooms,
                                                        i.Street,
                                                        i.TotalLike,
                                                        i.TotalView,
                                                        i.WithOwner
                                                    }).Where(p => p.IsDetele == false && p.PostStatus == PostStatusEnum.Request);
            List<PostViewModel> models = new List<PostViewModel>();
            foreach (var post in posts)
            {
                Infrastructure infrastructure = new Infrastructure
                {
                    AirCond = post.AirCond,
                    Balcony = post.Balcony,
                    Bath = post.Bath,
                    ElecPrice = post.ElecPrice,
                    WaterPrice = post.WaterPrice,
                    Fridge = post.Fridge,
                    Kitchen = post.Kitchen,
                    WaterHeater = post.WaterHeater
                };
                AddressNearBy addressNearBy = new AddressNearBy()
                {
                    Medical = post.Medical,
                    BusStation = post.BusStation,
                    Education = post.Education,
                };
                PostViewModel model = new PostViewModel()
                {
                    PostId = post.PostId,
                    City = post.City,
                    District = post.District,
                    Street = post.Street,
                    AddressNearBy = addressNearBy,
                    Price = post.Price,
                    Area = post.Area,
                    Images = post.Images,
                    Hired = post.Hired,
                    Infrastructure = infrastructure,
                    Contact = post.PhoneNumber,
                    FullNameOwner = post.LastName + " " + post.FirstName,
                    EmailOwner = post.Email,
                    ExpireTime = post.ExpireTime,
                    TotalLike = post.TotalLike,
                    TotalView = post.TotalView,
                    PostStatus = post.PostStatus,
                    RoomCategoryId = post.RoomCategoryId
                };
                models.Add(model);
            }
            return models;
        }
        public async Task<Post> DeletePost(long postId)
        {
            var post = context.Posts.Where(x => x.PostId == postId && x.IsDetele == false && x.PostStatus == PostStatusEnum.Request).FirstOrDefault();
            if (post == null) throw new ServiceException("Bài đăng không tồn tại hoặc không trong trạng thái request");
            post.IsDetele = true;
            context.Posts.Update(post);
            await context.SaveChangesAsync();
            return post;
        }

        public async Task<Post> RecoverRejectPost(long postId)
        {
            var post = context.Posts.Where(x => x.PostStatus == PostStatusEnum.Rejected && x.PostId == postId).FirstOrDefault();
            if (post == null) throw new ServiceException("Bai dang khong ton tai");
            var user = userManager.Users.Where(x => x.Id == post.UserId && x.IsConfirm).FirstOrDefault();
            Notification notification = new Notification()
            {
                UserName = user.UserName,
                PostId = post.PostId,
                Content = CommonConstants.POST_ACCEPTED,
                NotifTime = DateTime.UtcNow,
                OfMod = false,
                IsDelete = false
            };
            await context.Notifications.AddAsync(notification);
            post.PostStatus = PostStatusEnum.Accepted;
            context.Posts.Update(post);
            await context.SaveChangesAsync();
            return post;
        }
    }
}
