#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["jwks-csharp/jwks-csharp.csproj", "jwks-csharp/"]
RUN dotnet restore "jwks-csharp/jwks-csharp.csproj"
COPY . .
WORKDIR "/src/jwks-csharp"
RUN dotnet build "jwks-csharp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "jwks-csharp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "jwks-csharp.dll"]