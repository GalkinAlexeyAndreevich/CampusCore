FROM node:20-alpine AS frontend
WORKDIR /src/client-app

COPY src/CampusCore.BackOffice/client-app/package*.json ./
RUN npm ci

COPY src/CampusCore.BackOffice/client-app/ ./
RUN npm run production

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY CampusCore.sln Directory.Build.props ./
COPY src/ ./src/
COPY --from=frontend /src/wwwroot/dist/ ./src/CampusCore.BackOffice/wwwroot/dist/

RUN dotnet restore src/CampusCore.BackOffice/CampusCore.BackOffice.csproj
RUN dotnet publish src/CampusCore.BackOffice/CampusCore.BackOffice.csproj \
	--configuration Release \
	--output /app/publish \
	--no-restore \
	/p:BuildClientApp=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish/ ./

EXPOSE 8080
ENTRYPOINT ["dotnet", "CampusCore.BackOffice.dll"]
