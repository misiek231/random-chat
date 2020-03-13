#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 443

FROM node:13-alpine as build-node
WORKDIR /ClientApp
COPY RandomChat.WebApi/ClientApp/package.json .
COPY RandomChat.WebApi/ClientApp/package-lock.json .
RUN npm install
COPY RandomChat.WebApi/ClientApp/ . 
RUN npm run build 

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
ENV BuildingDocker true
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
COPY --from=build-node /ClientApp/build ./ClientApp/build
ENTRYPOINT ["dotnet", "RandomChat.WebApi.dll"]