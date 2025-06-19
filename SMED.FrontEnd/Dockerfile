# Etapa 1: build del proyecto WASM
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . ./
RUN dotnet publish -c Release -o /app/dist

# Etapa 2: servidor NGINX para los archivos estáticos
FROM nginx:alpine
# Copiar los archivos publicados del build anterior
COPY --from=build /app/dist/wwwroot /usr/share/nginx/html

#Copiar la configuración personalizada de nginx
COPY SMED.FrontEnd/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
