# Utiliser une image de base avec le SDK .NET
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copier les fichiers du projet et restaurer les dépendances
COPY *.csproj ./
RUN dotnet restore

# Copier tous les fichiers et publier l'application
COPY . ./
RUN dotnet publish -c Release -o out

# Utiliser une image runtime .NET pour exécuter l'application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Gateway.dll"]
