# Dependencias de desarrollo
FROM node:19.2-alpine3.16 as deps
# cd app
WORKDIR /app
# Dest /app
COPY package.json ./
# Se instalan las dependencias
RUN npm install


# Build y test
FROM node:19.2-alpine3.16 as builder
# cd app
WORKDIR /app
# Se toman las dependencias instaladas de la etapa anterior
# Etapa Origen Destino
COPY --from=deps /app/node_modules ./node_modules
COPY . .
# Realizar testing
RUN npm run test
# Acá iría línea de código para hacer el build de la aplicación


# Dependencias de producción
FROM node:19.2-alpine3.16 as prod-deps
# cd app
WORKDIR /app
COPY package.json ./
# Se instalan las dependencias de producción
RUN npm install --prod


# Ejecutar la App
FROM node:19.2-alpine3.16 as runner
WORKDIR /app
COPY --from=prod-deps ./app/node_modules ./node_modules
COPY app.js ./
COPY tasks/ ./tasks
CMD [ "node", "app.js" ]