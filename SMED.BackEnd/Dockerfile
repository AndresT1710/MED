# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

RUN ln -fs /usr/share/zoneinfo/America/Ecuador /etc/localtime && \
	dpkg-reconfigure --frontend noninteractive tzdata


USER $APP_UID

# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SMED.BackEnd/SMED.BackEnd.csproj", "SMED.BackEnd/"]
COPY ["SMED.Shared/SMED.Shared.csproj", "SMED.Shared/"]
RUN dotnet restore "./SMED.BackEnd/SMED.BackEnd.csproj"
COPY . .
WORKDIR "/src/SMED.BackEnd"
RUN dotnet build "./SMED.BackEnd.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SMED.BackEnd.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .



ENTRYPOINT ["dotnet", "SMED.BackEnd.dll"]