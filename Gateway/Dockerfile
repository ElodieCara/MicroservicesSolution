FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copier le fichier projet et restaurer les dépendances
COPY Gateway.csproj ./
RUN dotnet restore

# Copier le reste des fichiers
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "Gateway.dll"]
