FROM --platform=$BUILDPLATFORM node:19.2-alpine3.16
# /app

# cd app
WORKDIR /app

# Dest /app
COPY package.json ./

# Se instalan las dependencias
RUN npm install

COPY . .

# Realizar testing
RUN npm run test

# Eliminar archivos y directorios no necesarios en producción.
# Se eliminan node_modules para poder construirlos de nuevos, pero ahora solo los de prod.
RUN rm -rf tests && rm -rf node_modules

# Se instalan las dependencias de producción
RUN npm install --prod

# Se ejecuta el comando (script definido en package.json) para correr app
# CMD se usa para la última instrucción/es a ejecutar.
CMD [ "node", "app.js" ]