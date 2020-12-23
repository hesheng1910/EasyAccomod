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

dotnet ef --project EasyAccomod.Core database update AddInfrastructure

dotnet ef --project EasyAccomod.Core database update RequestExtend

dotnet ef --project EasyAccomod.Core database update DateViewPost

#Đặt EasyAccomod.BackendApi hoặc EasyAccomod.FrontendApi để Run

Tài khoản admin:

admin

Abc@1234

Cấu trúc Project:

Phần Api : Chứa controller

Phần Core(Data) : Chứa những xử lý dữ liệu, xử lý nghiệp vụ

Common : Chứa những class dùng chung cho tất cả project

Configurations : Chứa những config về những trường của cơ sở dữ liệu

EF : chứa cài đặt kết nối với cơ sở dữ liệu

Entities : Chứa các đối tượng ( các bảng trong csdl)

Migrations : Chứa các bản migration 

Extension: Hiện tại đang chứa các data mẫu

Model : Chứa các class request

Service : Nơi xử lý nghiệp vụ


