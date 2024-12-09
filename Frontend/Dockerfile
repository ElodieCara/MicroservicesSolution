# Utiliser une image de base Node.js
FROM node:18 AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

# Utiliser une image de base Nginx pour servir les fichiers
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
