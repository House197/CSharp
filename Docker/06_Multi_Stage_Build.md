# Multi-Stage Build
- Consiste en separar las capas de la imagen en en etapas que se pueden considerar sub imágenes.
- De cada etapa se puede pasar información a otra, permitiéndo manejar diferentes entornos sin tener que eliminar dependencias de forma manual.
- Se asignan alias:

``` dockerfile
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
```

- Se aprecia que para el comando de COPY se debe er granular con el nombre del destino, ya que no se especifica entonces copia el contenido de la carpeta de origen y lo coloca directamente en el destino:
    - Por ejemplo, copiaría el contenido de la carpeta de tasks y lo colocaría directamente en el working directory sin crear una carpeta llamada tasks.

## Build con otras arquitecturas
1. Se corre el contenedor de buildx.
``` docker
docker buildx create --name mybuilder --bootstrap --use
```
2. Se usa el builder. (El comando anterior ya lo hizo)
``` docker
docker buildx use mybuilder
```
3. Se enlistan las arquitecturas que mensaje el builder.
``` docker
docker buildx inspect
```
4. Se ejecuta el build con las arquitecturas:
``` docker
docker buildx build --platform linux/amd64,linux/amd64/v2,linux/arm64 \
-t houser97/cron-ticker:toy --push .
```

## Docker compose build
- Las variables de entorno que se definen en el docker compose están disponibles en el dockerfile.
- EL build crea la imagen.
### Ejecutar partes específicas del Dockerfile
- Para usar un dockerfile en el docker compose se deben especificar los campos build y context, en donde context especifica en donde se encuentra el archivo de dockerfile relativo a la ubicación del docker compose.
- En este ejemplo se crea un nuevo stage para el paso de desarrollo en el Dockerfile.
    - También es posible crear varios dockerfiles y deferenciarlos por medio de su nombre (dockerfile.dev, odockerfile.prod).
- En el docker composer se hace un mapeo adicional. 
    - EN el caso en que no se tengan los módulos de node_module en el contenedor, entonces se pasan de forma local. (ya hcer el mapeo, no el volumen)
    - Los módulos se van a construir hasta el punto del comando de RUN yarn install, entonces también se debe mapear de manera individual. /app/node_modules
- Se pueden eliminar los volumenes en conjunto con docker compose down --volumes
- Se ejecuta docker compose build, lo que hace es que corre únicamente en el docker file la primera sección definida, ya que se especificó por su nombre en la sección de build en el docker compose.

```dockerfile
FROM node:19-alpine3.15 as dev
WORKDIR /app
COPY package.json package.json
RUN yarn install
CMD [ "yarn","start:dev"]




FROM node:19-alpine3.15 as dev-deps
WORKDIR /app
COPY package.json package.json
RUN yarn install --frozen-lockfile


FROM node:19-alpine3.15 as builder
WORKDIR /app
COPY --from=dev-deps /app/node_modules ./node_modules
COPY . .
# RUN yarn test
RUN yarn build

FROM node:19-alpine3.15 as prod-deps
WORKDIR /app
COPY package.json package.json
RUN yarn install --prod --frozen-lockfile


FROM node:19-alpine3.15 as prod
EXPOSE 3000
WORKDIR /app
ENV APP_VERSION=${APP_VERSION}
COPY --from=prod-deps /app/node_modules ./node_modules
COPY --from=builder /app/dist ./dist

CMD [ "node","dist/main.js"]
```

``` yaml
version: '3'

services:

  app:
    build:
        context: .
        target: dev
        dockerfile: Dockerfile

    volumes:
      - .:/app
      - /app/node_modules
    container_name: nest-app
    ports:
      - ${PORT}:${PORT}
    environment:
      APP_VERSION: ${APP_VERSION}
      STAGE: ${STAGE}
      DB_PASSWORD: ${DB_PASSWORD}
      DB_NAME: ${DB_NAME}
      DB_HOST: ${DB_HOST}
      DB_PORT: ${DB_PORT}
      DB_USERNAME: ${DB_USERNAME}
      PORT: ${PORT}
      HOST_API: ${HOST_API}
      JWT_SECRET: ${JWT_SECRET}

  db:
    image: postgres:14.3
    restart: always
    ports:
      - "5432:5432"
    environment:
      POSTGRES_PASSWORD: ${DB_PASSWORD}
      POSTGRES_DB: ${DB_NAME}
    container_name: ${DB_NAME}
    volumes:
      - postgres-db:/var/lib/postgresql/data

volumes:
  postgres-db:
    external: false

```

- En la variable de entorno DB_HOST el valor no puede ser localhost, ya que el contenedor de nest-app no tiene la bases de datos en su localhost.
    - Se recuerda que debe ser el DNS de la red del contenedor de la base de datos, por lo que el valor es TesloDB.

#### Explicación de porqué colocar /app/node_modules en volúmenes
When docker builds the image, the node_modules directory is created within the worker directory, and all the dependencies are installed there. Then on runtime the worker directory from outside docker is mounted into the docker instance (which does not have the installed node_modules), hiding the node_modules you just installed. You can verify this by removing the mounted volume from your docker-compose.yml.

A workaround is to use a data volume to store all the node_modules, as data volumes copy in the data from the built docker image before the worker directory is mounted. This can be done in the docker-compose.yml like this:

``` yaml
redis:
    image: redis
worker:
    build: ./worker
    command: npm start
    ports:
        - "9730:9730"
    volumes:
        - ./worker/:/worker/
        - /worker/node_modules
    links:
        - redis
```

https://stackoverflow.com/questions/67114425/docker-compose-node-modules-mouting-as-volume
/app/node_modules creates a directory inside the container and the Docker Engine automatically creates an anonymous volume for this (i.e. it should will probably be empty). This is from the docs about the compose file spec in the "Short Syntax" section.

n response to followups regarding why using /app/node_modules works but the other syntax does not:

Your yarn install command creates a node_modules folder inside the Docker image. This created folder conflicts with the existing frontend/node_modules folder you have locally when trying to run with ./frontend/node_modules:/app/node_modules. When you specify /app/node_modules, the container uses the directory created during the build step.

- Entonces, ya que se hace un bind volume y no se tienen los módulos de node instalados en la parte local, los va a 'esconder' en el contenedor, por lo que los node_modulos instalados durante el build de la imagen se guardan en un data volume (volumen anónimo) para evitar este problema.

## Production build
- Se tiene la variable de entorno en .env llamada STAGE.
- Se cambia su valor a prod o dev según se desee.
- Se coloca la variable en target, ubicado en build el docker compose.
- Docker ejecuta todos los pasos que se encuentran antes del stage deseado en el Dockerfile.
- En la parte de producción no se desea el bind volume ni el volume anónimo para node_modules.
- Se crea el documento docker-compose.prod.yml.
    - Se eliminan los volumenes de app en este archivo.
- Se colocan los tags en este archivo.
    - En el apartado de image se coloca: "houser97/teslo-shop-backend:tag"
- Se ejecutan los siguiente comandos de docker que especifica el yml de producción.

``` bash
docker compose -f docker-compose.prod.yml build
```

``` bash
docker compose -f docker-compose.prod.yml up
```

- Como buena práctica solo se hace la imagen del servicio de app.
``` bash
docker compose -f docker-compose.prod.yml build app
```

## Buenas prácticas
### Imágenes
- Reconstruir de vez en cuando toda la imagen.
``` bash
docker build --no-cahe -t myImage:myTag
```
- Las imágenes deben ser autosuficientes.
    - En el ejemplo de docker compose build, en la base de datos no se hace ninguna configuración extra además de la definición del usuario.
    - Entonces, es mejor constuir un servicio en particular, como lo sería el de la app.
    - Eso se hace especificando el servicio en el comando de dockjer compose -f
``` bash
docker compose -f docker-compose.prod.yml build app
```

### Diferencia entre docker compose build y docker build
https://stackoverflow.com/questions/50230399/what-is-the-difference-between-docker-compose-build-and-docker-build

docker-compose can be considered a wrapper around the docker CLI (in fact it is another implementation in python as said in the comments) in order to gain time and avoid 500 characters-long lines (and also start multiple containers at the same time). It uses a file called docker-compose.yml in order to retrieve parameters.

You can find the reference for the docker-compose file format here.

So basically docker-compose build will read your docker-compose.yml, look for all services containing the build: statement and run a docker build for each one.

Each build can specify a Dockerfile, a context and args to pass to docker.

To conclude with an example docker-compose.yml file:

``` yml
version: '3.2'

services:
  database:
    image: mariadb
    restart: always
    volumes:
      - ./.data/sql:/var/lib/mysql

  web:
    build:
      dockerfile: Dockerfile-alpine
      context: ./web
    ports:
      - 8099:80
    depends_on:
```

When calling docker-compose build, only the web target will need an image to be built. The docker build command would look like:

``` bash
docker build -t web_myproject -f Dockerfile-alpine ./web
```