# EasyAccomod
Web_Project

- Thêm connectstring vào đầu 3 file appsettings.json

 "ConnectionStrings": {
 "EasyAccomodDb": "Data Source=ServerName;Initial Catalog=EasyAccomodDb;Persist Security Info=True;User ID=sa;Password=123456"
 },
  
 (copy nguyên từ dấu " đến dấu ,)
 
 Thay ServerName bằng tên ServerName của máy.
 
- Vào SQL Server Tool tạo database tên EasyAccomodDb
- Mở cmdline trong VS chạy tập lệnh
- Đặt EasyAccomod.Core là start up project

dotnet tool install --global dotnet-ef

dotnet ef --project EasyAccomod.Core database update IntialCreate

#Đặt EasyAccomod.BackendApi hoặc EasyAccomod.FrontendApi để Run
 Tài khoản admin:
 admin
 Abc@1234
