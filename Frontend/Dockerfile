FROM node:18 AS build
WORKDIR /app

# Installer les dépendances
COPY package*.json ./
RUN npm install

# Copier le reste des fichiers et construire l'application
COPY . ./
RUN npm run build

FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
