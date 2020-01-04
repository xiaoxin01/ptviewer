FROM microsoft/dotnet:3.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:3.1-sdk AS build
WORKDIR /src
COPY ["src/PtViewer/PtViewer.csproj", "src/PtViewer/"]
RUN dotnet restore "src/PtViewer/PtViewer.csproj"
COPY . .
WORKDIR "/src/src/PtViewer"
RUN dotnet build "PtViewer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PtViewer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PtViewer.dll"]