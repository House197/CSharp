FROM node:19.2-alpine3.16
# /app

# cd app
WORKDIR /app

# Dest /app
COPY package.json ./

# Se instalan las dependencias
RUN npm install

COPY app.js ./

# Se ejecuta el comando (script definido en package.json) para correr app
# CMD se usa para la última instrucción/es a ejecutar.
CMD [ "node", "app.js" ]