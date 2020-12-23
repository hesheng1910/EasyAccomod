using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EasyAccomod.Core.EF;
using EasyAccomod.Core.Entities;
using EasyAccomod.Core.Services.Comments;
using EasyAccomod.Core.Services.Notifications;
using EasyAccomod.Core.Services.Posts;
using EasyAccomod.Core.Services.Reports;
using EasyAccomod.Core.Services.RequestExtends;
using EasyAccomod.Core.Services.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace EasyAccomod
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EasyAccDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("EasyAccomodDb")));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<EasyAccDbContext>()
                .AddDefaultTokenProviders();

            services.AddDistributedMemoryCache();           // Đăng ký dịch vụ lưu cache trong bộ nhớ 
            services.AddSession(cfg => {                    // Đăng ký dịch vụ Session
                cfg.Cookie.Name = "EasyAccomod";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
                cfg.IdleTimeout = new TimeSpan(0, 60, 0);    // Thời gian tồn tại của Session
            });
            //Declare DI
            services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IRequestExtendService, RequestExtendService>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger eShop Solution", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseAuthorization();

                app.UseSession();

                app.UseStaticFiles();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
                app.UseSwagger();
                app.UseSwaggerUI(config =>
                {
                    config.SwaggerEndpoint("v1/swagger.json", "Backend Api v1");
                    config.DocExpansion(DocExpansion.None);
                    config.ShowExtensions();
                });
        }
        }
    }
