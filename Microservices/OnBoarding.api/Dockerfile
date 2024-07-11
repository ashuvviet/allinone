#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["Directory.Build.targets", "."]
COPY ["OnBoarding.api/OnBoarding.api.csproj", "OnBoarding.api/"]
COPY ["Messaging.Core/Messaging.Core.csproj", "Messaging.Core/"]
COPY ["RabbitMQ.Infra/RabbitMQ.Infra.csproj", "RabbitMQ.Infra/"]
RUN dotnet restore "./OnBoarding.api/./OnBoarding.api.csproj"
COPY . .
WORKDIR "/src/OnBoarding.api"
RUN dotnet build "./OnBoarding.api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OnBoarding.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnBoarding.api.dll"]