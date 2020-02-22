#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["RandomChat.WebApi/RandomChat.WebApi.csproj", "RandomChat.WebApi/"]
RUN dotnet restore "RandomChat.WebApi/RandomChat.WebApi.csproj"
COPY . .
WORKDIR "/src/RandomChat.WebApi"
RUN dotnet build "RandomChat.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RandomChat.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RandomChat.WebApi.dll"]