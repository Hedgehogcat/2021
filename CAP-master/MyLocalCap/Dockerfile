#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["MyLocalCap/MyLocalCap.csproj", "MyLocalCap/"]
RUN dotnet restore "MyLocalCap/MyLocalCap.csproj"
COPY . .
WORKDIR "/src/MyLocalCap"
RUN dotnet build "MyLocalCap.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyLocalCap.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyLocalCap.dll"]