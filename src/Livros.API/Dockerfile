#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["src/Livros.API/Livros.API.csproj", "src/Livros.API/"]
COPY ["src/Livros.Aplicacao/Livros.Aplicacao.csproj", "src/Livros.Aplicacao/"]
COPY ["src/Livros.Dominio/Livros.Dominio.csproj", "src/Livros.Dominio/"]
COPY ["src/Livros.Infraestrutura/Livros.Infraestrutura.csproj", "src/Livros.Infraestrutura/"]
RUN dotnet restore "./src/Livros.API/Livros.API.csproj"
COPY . .
WORKDIR "/src/src/Livros.API"
RUN dotnet build "./Livros.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "./Livros.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Livros.API.dll"]

## Usar o usuário root para instalar dependências
#USER root
#
## Instalar dependências
#RUN apt-get update && \
    #apt-get install -y --no-install-recommends libgdiplus libc6-dev && \
    #apt-get clean && \
    #rm -rf /var/lib/apt/lists/*
#
## Criar link simbólico para libgdiplus
#RUN ln -s libgdiplus.so /usr/lib/libgdiplus.so