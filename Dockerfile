FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:12.14.0-alpine3.9 AS nodeBuild
RUN npm config set registry https://registry.npm.taobao.org --global && \
    npm config set disturl https://npm.taobao.org/dist --global
WORKDIR /app
COPY ./src/ptviewer/ClientApp/package.json /app/package.json
RUN npm install --production
COPY ./src/ptviewer/ClientApp /app
RUN npm run build

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/ptviewer/PtViewer.csproj", "src/ptviewer/"]
RUN dotnet restore "src/ptviewer/PtViewer.csproj"
COPY ./src ./src
WORKDIR /src/src/ptviewer
RUN dotnet build "PtViewer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PtViewer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=nodeBuild /app/build ./ClientApp/build
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PtViewer.dll"]
