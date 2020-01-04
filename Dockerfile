FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs
RUN npm config set registry https://registry.npm.taobao.org --global && \
    npm config set disturl https://npm.taobao.org/dist --global
WORKDIR /src
COPY ["src/ptviewer/PtViewer.csproj", "src/ptviewer/"]
RUN dotnet restore "src/ptviewer/PtViewer.csproj"
COPY ./src ./src
WORKDIR "/src/src/ptviewer"
RUN dotnet build "PtViewer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PtViewer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PtViewer.dll"]
