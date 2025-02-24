# Stage 1: Build & Publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy file solution
COPY ManageRevenue.sln ./

# Copy file .csproj của từng project để tận dụng cache khi restore
COPY ManageRevenue/ManageRevenue.csproj ManageRevenue/
COPY ManageRevenue.BLL/ManageRevenue.BLL.csproj ManageRevenue.BLL/
COPY ManageRevenue.DAL/ManageRevenue.DAL.csproj ManageRevenue.DAL/
COPY ManageRevenue.Domain/ManageRevenue.Domain.csproj ManageRevenue.Domain/

# Restore nuget packages cho toàn bộ solution
RUN dotnet restore

# Copy toàn bộ source code còn lại
COPY . .

# Build & publish ở chế độ Release
RUN dotnet publish ManageRevenue.sln -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Đặt biến môi trường để ứng dụng chỉ chạy HTTP
ENV ASPNETCORE_URLS=http://+:80

# Copy kết quả publish từ stage build
COPY --from=build /app/publish .

# Mở cổng 80
EXPOSE 80

# Khởi chạy ứng dụng (thay ManageRevenue.dll bằng tên dll của project startup)
ENTRYPOINT ["dotnet", "ManageRevenue.dll"]
