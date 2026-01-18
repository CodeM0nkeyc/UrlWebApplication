FROM node:20 AS angular-build
WORKDIR /src/UrlShortener.Web/Client

COPY UrlShortener.Web/Client/package*.json ./
RUN npm install

COPY UrlShortener.Web/Client/ ./

RUN npm run build --output-path=../wwwroot/angular

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["UrlShortener.Web/UrlShortener.Web.csproj", "UrlShortener.Web/"]
COPY ["UrlShortener.Application/UrlShortener.Application.csproj", "UrlShortener.Application/"]
COPY ["UrlShortener.Infrastructure/UrlShortener.Infrastructure.csproj", "UrlShortener.Infrastructure/"]
RUN dotnet restore "UrlShortener.Web/UrlShortener.Web.csproj"

COPY . .

COPY --from=angular-build /src/UrlShortener.Web/wwwroot/angular ./UrlShortener.Web/wwwroot/angular

WORKDIR "/src/UrlShortener.Web"
RUN dotnet build "./UrlShortener.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UrlShortener.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 7208

ENTRYPOINT ["dotnet", "UrlShortener.Web.dll"]
