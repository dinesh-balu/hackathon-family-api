FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/FamilyApp.Api/FamilyApp.Api.csproj", "src/FamilyApp.Api/"]
COPY ["src/FamilyApp.Services/FamilyApp.Services.csproj", "src/FamilyApp.Services/"]
COPY ["src/FamilyApp.Repositories/FamilyApp.Repositories.csproj", "src/FamilyApp.Repositories/"]
COPY ["src/FamilyApp.Database/FamilyApp.Database.csproj", "src/FamilyApp.Database/"]
COPY ["src/FamilyApp.Models/FamilyApp.Models.csproj", "src/FamilyApp.Models/"]
RUN dotnet restore "src/FamilyApp.Api/FamilyApp.Api.csproj"
COPY . .
WORKDIR "/src/src/FamilyApp.Api"
RUN dotnet build "FamilyApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FamilyApp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FamilyApp.Api.dll"]
