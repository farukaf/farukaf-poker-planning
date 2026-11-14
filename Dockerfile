FROM mcr.microsoft.com/dotnet/sdk:10.0.403-alpine3.24@sha256:5562fd626eec39f6f53218f9b7d7856ab7195d51d846568304fbe85071a3b6e6 AS build
WORKDIR /src
COPY ["src/Directory.Build.props", "src/Directory.Build.props"]
COPY ["src/PokerPlanning/", "src/PokerPlanning/"]
RUN dotnet restore "src/PokerPlanning/PokerPlanning.csproj" --locked-mode
COPY . .
RUN dotnet publish "src/PokerPlanning/PokerPlanning.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:11.0.0-alpine3.24@sha256:d39b72ad1534fa0faf52b488bffb0762df7ba269d120f0338713d4dda2a41bf5 AS final
WORKDIR /app
EXPOSE 8080
HEALTHCHECK --interval=30s --timeout=3s --start-period=10s \
  CMD wget -q -O /dev/null http://127.0.0.1:8080/ || exit 1
USER $APP_UID
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PokerPlanning.dll"]
