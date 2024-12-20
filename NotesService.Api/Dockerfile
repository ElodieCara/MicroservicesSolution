# Étape 1 : Utiliser l'image de base avec le SDK .NET pour construire l'application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copier les fichiers du projet et restaurer les dépendances
COPY *.csproj ./
RUN dotnet restore

# Copier tous les fichiers et publier l'application
COPY . ./
RUN dotnet publish -c Release -o /out

# Étape 2 : Utiliser une image runtime pour exécuter l'application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

# Définir le port exposé
EXPOSE 8080

# Définir le point d'entrée
ENTRYPOINT ["dotnet", "NotesService.Api.dll"]
