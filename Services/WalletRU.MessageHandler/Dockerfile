FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WalletRU.MessageHandler/WalletRU.MessageHandler.csproj", "WalletRU.MessageHandler/"]
RUN dotnet restore "WalletRU.MessageHandler/WalletRU.MessageHandler.csproj"
COPY . .
WORKDIR "/src/WalletRU.MessageHandler"
RUN dotnet build "WalletRU.MessageHandler.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WalletRU.MessageHandler.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WalletRU.MessageHandler.dll"]
