FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/WebCore.Web.Host/WebCore.Web.Host.csproj", "src/WebCore.Web.Host/"]
COPY ["src/WebCore.Web.Core/WebCore.Web.Core.csproj", "src/WebCore.Web.Core/"]
COPY ["src/WebCore.Application/WebCore.Application.csproj", "src/WebCore.Application/"]
COPY ["src/WebCore.Core/WebCore.Core.csproj", "src/WebCore.Core/"]
COPY ["src/WebCore.EntityFrameworkCore/WebCore.EntityFrameworkCore.csproj", "src/WebCore.EntityFrameworkCore/"]
WORKDIR "/src/src/WebCore.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/WebCore.Web.Host", "src/WebCore.Web.Host"]
COPY ["src/WebCore.Web.Core", "src/WebCore.Web.Core"]
COPY ["src/WebCore.Application", "src/WebCore.Application"]
COPY ["src/WebCore.Core", "src/WebCore.Core"]
COPY ["src/WebCore.EntityFrameworkCore", "src/WebCore.EntityFrameworkCore"]
WORKDIR "/src/src/WebCore.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "WebCore.Web.Host.dll"]
