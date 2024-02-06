# Docker
- Se ven los comandos disponibles con:

``` bash
docker container --help
docker image --help
```

# Volúmenes y Redes
- Se va a crear una red para unir una DB y PHP MyAdm.
## Tipos de volúmenes
### Named Volumes
- Se especifica el nombre que se le dará al Volume.

``` bash
docker volume create todo-db
```

- Se enlistan con ls

``` Bash
docker volume ls
```

- Se inseccionan con inspect

``` Bash
docker volume inspect todo-db
```

- Se remueven los volumenes no usado con prune

``` bash
docker volume prune
```

- Se usa un volumen al correr un contenedor

``` bash
docker run -v todo-db:/etc/todos getting-started
```

### Bind Volumes
- Trabaja con paths absolutos.
- Son útiles cuando se desea ligar un files system del equipo a un file system del contenedor.

``` bash
docker run -dp 3000:3000 \
-w /app -v "$(pwd):/app" \
node:18-alpine \
sh -c "yarn install && yarn run dev"
```

#### Ejercicio Bind Volumes
- A partir de un proyecto Nest, se crea un bind volume de la máquina local al contenedor.
- Se tiene la carpeta del archivo (nest-graphql), la cual se correrá en un contenedor que tiene una versión de node.
    - Esto puede ser útil cuando se desea correr la aplicación con una versión de Node específica y en un entorno de Linux.
- Para el ejemplo, se toma la imagen de Node 16-alpine3.16.
- Desde la termina, estando en la ruta de la carpeteta del archivo nest-graphq, se corre el comando:

``` bash
docker container run \
--name nest-app \
-w /app \
-p 80:3000 \
-v "$(pwd)":/app \
-d \
node:16-alpine3.16 \
sh -c "yarn install && yarn start:dev"
```

- Este comando levanta el contenedor, en donde el proyecto en la máquina local ahora corre en el contenedor, lo que provoca que cualquier cambio que se haga afecta al otro.
- Notas: 
    - Working Directory equivale a hacer cd dentro del contenedor.
    - El puerto 80 es el que por defecto escucha localhost.
    - A modo de que el contenedor no se cierre se ejecuta el comando sh -c "yarn install && yarn start:dev", ya que por defecto las imágenes de Node se monta, se ejecutan y si ya no hay algún comando se cierran.

#### Terminal interactiva -it
- Teniendo el contenedor corriendo, se usa el comando:

``` bash
docker exec -it <contenedorID> <ejecutable>
```

- Entonces, se va a tener el comando:

``` bash
docker exec -it 978 /bin/sh
```

- El comando sh permite abrir la terminal en el contenedor.
    - Este comando se encuentra en la carpeta bin.
- Se aprecia que se entra al contenedor a la ruta de /app, ya que es el working directory que se especificó.
    - Si se ejecuta cd .. se accede al filesystem del contenedor.
- Se pueden editar archivos en el contenedor, lo cual se verá reflejado en la máquina local.
    - Normalmente se pueden editar archivos desde laterminal usando vi <archivo>.
        - Al usar la versión Alpine, la cual es una versión ligera, se debe presionar la letra i para poder empezar a modificar el archivo.
    - Al terminar la edición se presionar ESC para poder escribir :wq, lo cual significa ESCACPE, WRITE, QUIT


### Anonymous Volumes
- Docker se encarga de asignar el nombre

## Hacer base de datos de MariaDB en docker persistente e implementar redes.
- Se crea el volumen.

``` bash
docker volume create world-db
```
- El volumen es una carpeta que se creó en la máquina local.
- Se corre el contenedor, en donde por medio de -v o --volume se liga el volumen creado con la dirección en el contenedor en donde se está guardando la data.
    - En la documentación de la imagen de MariaDB en Docker Hub se encuentra esa información.
        - Se puede filtrar la información de la página por medio de las palabras /var/lib

``` bash
docker container run \
--name world-db \
--env MARIADB_USER=example-user \
--env MARIADB_PASSWORD=user-password \
--env MARIADB_ROOT_PASSWORD=root-secret-password \
--env MARIADB_DATABASE=world-db \
--volume world-db:/var/lib/mysql \
mariadb:jammy
```

## Uso de phpMyAdmin para gestionar la base de datos
- Se crea un contenedor usando la imagen, en donde la versión de la imagen que se usa es: 5.2.0-apache
    - phpmyadmin:5.2.0-apache
- NOTA: En docker ya no se recomienda usar --link
    - Link permitía la comunicación entre contenedores, pero ya ha sido descartado y se deben usar redes.
- En la documentación de phpmyadmin se usa el ejemplo que dice: Usage with arbitrary server.

``` bash
docker container run \
--name phpmyadmin \
-d \
-e PMA_ARBBITRARY=1 \
-p 88080:80 \
phpmyadmin:5.2.0-apache
```
- Si se intenta conectar a la DB usando PHMADMIN no se puede hacer, ya que el servidor de la DB no está expuesto para que el servidor PHPADMIN en el otro contenedor pueda acceder.
- Dos contenedores o más pueden hablar entre sí si es que están en la misma red.
    - Se accede al puerto 8080 en el navegador y se acceden con las credenciales dadas en el contenedor de MariaDB:
        - MARIADB_USER=example-user
        - MARIADB_PASSWORD=user-password
        - server: world-db //En este caso es porque es el servidor en el cual está en la misma red que phpMyAdmin, en otras palabras, es el nombre del contenedor de la base de datos, el cual fue tomado como identificado en el DNS para la red del contenedor de la DB.

## Implementación de Red
- Así como se ha hecho con volúmenes, se usa la sintaxis:
``` bash
docker network create <NombreRed>
docker network create world-app
```

- Se conectan contenedores a la red por medio de connect:

``` bash
docker network connect <NombreRed> <IdentificadorContenedor>
docker network connect world-app deb //Conectar phpmyadmin a red
docker network connect world-app 86e //Conectar DB a red
```

- Se puede verificar que los contenedores estén en la misma red con:

``` bash
docker network inspect <NombreRed>
```

- Los contenedores conectados están en el apartado de Containers.
- Docker crea los identificadores mediante nombres o un DNS, lo cual permite identificar la base de datos y los contenedores van a poder comunicarse entre sí usando el mismo nombre.
    - En la parte de Containers dado por el comando inspect se puede ver esto con el apartado de Name dentro del contenedor que corresponde.

### Implementación de red desde inicialización
``` bash
docker container run \
--name world-db \
--env MARIADB_USER=example-user \
--env MARIADB_PASSWORD=user-password \
--env MARIADB_ROOT_PASSWORD=root-secret-password \
--env MARIADB_DATABASE=world-db \
--volume world-db:/var/lib/mysql \
--network world-app \
mariadb:jammy
```

``` bash
docker container run \
--name phpmyadmin \
-d \
-e PMA_ARBBITRARY=1 \
-p 88080:80 \
--network world-app \
phpmyadmin:5.2.0-apache
```

# Eliminar todos los contenedores o imágenes
``` bash
docker container rm -s $(docker container ls -aq)
```

- La bandera q permite traer solo el ID del listado.

# Ver Logs de un contenedor
``` bash
docker container logs <container id>
docker container logs --follow CONTAINER
```

# Eliminar contenedores
## Eliminar contenedores por medio de ID o nombre
- Basta con colocar los primeros dígitos de ID para referirse al contenedor.
- Se separan con espacios los IDs para indicar múltiples contenedores para eliminar.
``` bash
docker container rm 345 543 543
```
## Eliminar todos los contenedores detenidos
``` bash
docker container prune
```  

# Enlistar contenedores
## Enlistar contenedores que están corriendo
``` bash
docker container ls
```

## Enlistar todos lo contenedores, estén corriendo o no
``` bash
docker container ls -a
```

# Variables de entorno
- Son útiles al momento de levantar un contenedor.
- Pueden venir de diferentes formas:
    - Variables definidas por una imagen determinada.
``` bash
docker run --name some-postgres -e POSTGRES_PASSWORD=mysecretpassword -d postgres
```
- Se pueden consultar en la descripción de la imagen en Docker Hub.

# Descargar una imagen
- Si no se especifica el tag entonces toma el latest.
``` bash
docker pull <imageName>
```

# Multi-container Apps - Docker Compose
## Laboratorio de reforzamiento.
- Se montarán contenedores para correr Postgres y pgAdmin.

### Paso 1. Creación de volumen
``` bash
docker volume create postgres-db
```

### Paso 2. Montar imagen de Postgres.
- NO hace falta asignar un puerto, ya que con pgAdmin se administra la db.
- EL path a la base de datos para hacer el mapeo del volumen se encuentra en la documentación oficial de docker para postgres, en donde se filtra la información por medio de las palabras var/lib
``` bash
docker container run \
-d \
--name postgres-db \
-e POSTGRES_PASSWORD=123456 \
-v postgres-db:/var/lib/postgresql/data \
postgres:15.1
```

### Paso 3. Montar imagen pgAdmin
``` bash
docker container run \
--name pgAdmin \
-e PGADMIN_DEFAULT_PASSWORD=123456 \
-e PGADMIN_DEFAULT_EMAIL=superman@google.com \
-dp 8080:80 \
dpage/pgadmin4:6.17
```

### Paso 4. Crear red para comunicar ambos contenedores
``` bash
docker network create postgres-net
```

### Paso 5. Asignar ambos contenedores a la red
```bash
docker network connect postgres-net postgres-db
docker network conncet postgres-net pgAdmin
```

### Paso 6. Ingresar a pgAdmin y crear conexión a la base de datos
http://localhost:8080/

Click derecho en Servers
Click en Register > Server
Colocar el nombre de: "SuperHeroesDB" (el nombre no importa)
Ir a la pestaña de connection
Colocar el hostname "postgres-db" (el mismo nombre que le dimos al contenedor)
Username es "postgres" y el password: 123456
Probar la conexión

## Docker compose
- Es una herramiento se que desarrolló para ayudar a definir y compartir aplicaciones de varios contenedores.
- Se crea la carpeta postgres-pgadmin
- Se crea el archivo docker-compose.yml

### Paso 1. Definir versión.
- Siempre se inicia con la versión del docker compose.
- Esto especifica si se usa la versión legacy, la 2 o si se trabaja con la última.

### Paso 2. Definir servicios.
- Cada servicio en la lista es un contenedor.
- Normalmente se definen los campos de image, volumes, ports, environment, container name (si no se especifica le coloca el nombre del servicio).
- Para el caso de los volumenes se tienen dos casos: usar uno existente o que se cree uno nuevo.
    - Al final del archivo se debe enlistar con ayuda del apartado volumes.
        - Si solo se especifica el nombre del volumen entonces se indica a docker que lo cree.
            - En este caso, el nombre del volumen es el especificado, en donde inicia con el nombre de la carpeta del archivo:
                - postgres-pgadmin_postgres-db
    - Para poder usar un volumen externo (el creado anteriormente postgres-db) se debe especificar external.
- Se puede detener el contenedor con docker compose down.

``` yml
version: '3'

services:
  db:
    container_name: postgres_database
    image: postgres:15.1
    volumes:
      - postgres-db:/var/lib/posgresql/data
    environment:
      - POSTGRES_PASSWORD=123456

  pgAdmin:
    depends_on:
      - db
    image: dpage/pgadmin4:6.17
    ports:
      - 8080:80
    environment:
      - PGADMIN_DEFAULT_PASSWORD=123456
      - PGADMIN_DEFAULT_EMAIL=superman@google.com

volumes:
  postgres-db:
    external: true
```

## Bind Volumes - Docker Compose
- Se eliminan los volúmenes trabajados hasta el momento con docker volume prune
- Docker compose se encarga de crear las carpetas en caso de que no existan cuando se define el Bind Volume.
- En contenedores con pgAdmin se recomienda revisar la documentación de la versión usada.
    - Se revisa la imagen para encontrar el VOLUME que se usó en su construcción por parte de los creadores.
    - Se escoge el TAG con la versión que corresponde y se busca el Volume.

<img src='Docker\images\pgAdmin.png'></img>

- De esta forma, se tienen dos bind volumes.

``` yml
version: '3'

services:
  db:
    container_name: postgres_database
    image: postgres:15.1
    volumes:
#      - postgres-db:/var/lib/posgresql/data
      - ./postgres:/var/lib/posgresql/data
    environment:
      - POSTGRES_PASSWORD=123456

  pgAdmin:
    depends_on:
      - db
    image: dpage/pgadmin4:6.17
    ports:
      - 8080:80
    volumes:
#      - postgres-db:/var/lib/posgresql/data
      - ./pgadmin:/var/lib/pgadmin
    environment:
      - PGADMIN_DEFAULT_PASSWORD=123456
      - PGADMIN_DEFAULT_EMAIL=superman@google.com

#volumes:
#  postgres-db:
#    external: true
```

- Es posible que para Linux aparezca un error, el cual se resuelve al hacer cambio de owner con:
    Warning: pgAdmin runs as the pgadmin user (UID: 5050) in the pgadmin group (GID: 5050) in the container. You must ensure that all files are readable, and where necessary (e.g. the working/session directory) writeable for this user on the host machine. For example:

    sudo chown -R 5050:5050 <host_directory>

    - On some filesystems that do not support extended attributes, it may not be possible to run pgAdmin without specifying a value for PGADMIN_LISTEN_PORT that is greater than 1024. In such cases, specify an alternate port when launching the container by adding the environment variable, for example:

    - -e 'PGADMIN_LISTEN_PORT=5050'
    - Don’t forget to adjust any host-container port mapping accordingly.

``` bash
sudo chown -R 5050:5050 pgadmin
```

- Si el contenedor corre en detached entonces se puede llevar registro de lo que sucede con:

``` bash
docker compose logs
docker compose logs -f // Muestra cualquier cambio o log que se vaya haciendo.
```

## Ejercicio 2. Multi-container app - Base de datos Mongo
- Se utilizan las siguiente imágenes:
    - Mongo
    - Mongo-express (permite conexión a base de datos).
    - Pokemon-nest-app
- Se crea la carpeta pokemon-app.
- Se crea el archivo .env
    - Docker compose lee por defecto los archivo .env que se encuentran al mismo nivel.
- Se crea el archivo de Docker Compose.
    - Al especificar que el volumen no es externo entonces Docker va a crear el volumen.
        - Por medio de este apartado se puede cambiar el volumen por ocupar en un futuro.
### Servicio MongoDB (DB)
- El servicio de DB (MongoDB) no se expone al exterior por medio de puerto, ya que solo se utiliza dentro de la red para otros contenedores.
- Se le coloca el campo de restart always, lo cual permite que el contenedor se reinicie automáticamente si es que se detiene.
#### Variables de entorno
- Las variables de entorno se definen con el campo environment.
    - Se tienen dos maneras de colocar las variables de entorno:
        - En un listado.
        - Como un campo.
``` yaml
    environment:
      - MONGO_INITDB_ROOT_USERNAME = mongoadmin

    environment:
      MONGO_INITDB_ROOT_USERNAME: mongoadmin
```

- Autenticación para consultas a base
    - La definición se puede encontrar en la documentación oficial.
    - A modo de poder ejecutar el comando se pueden usar el comando de docker compose (command: ['--auth']). Por otro lado, otra alternativa menos viable es:
        - Entrar al contenedor y ejecutar el comando con la terminal interactiva usando el bash.
    - En mongoDB se puede autenticar por medio del connection string:
        - mongodb://username:password@localhost:27017
``` yaml
command: ['--auth']
```
#### Uso de .env
- Se definen las variables en el archivo .env

``` env
MONGO_USERNAME=Quemso
MONGO_PASSWORD=123456
MONGO_DB_NAME=pokemon_db
```

- En el archivo de Docker Compose se usan las variables de entorno por medio ${MONGO_DB_NAME}

``` yml
version: '3'

services:
  db:
    container_name: ${MONGO_DB_NAME}
    image: mongo:6.0
    volumes:
      - poke-vol:/data/db
    restart: always
    ports:
      - 27017:27017
    environment:
      MONGO_INITDB_ROOT_USERNAME: ${MONGO_USERNAME}
      MONGO_INITDB_ROOT_PASSWORD: ${MONGO_PASSWORD}

volumes:
  poke-vol:
    external: false
```

### Visor de Base de datos (mongo-express)
- Entre las variables de entorno que se definen se encuentra el nombre del servidor a conectar, el cual es el de la base de datos.
    - Esto corresponde con la red del contenedor al que se desea conectar, la cual tiene el DNS con el nombre del contenedor, siendo en este caso el valor de la variable .env MONGO_DB_NAME.
    - En otras palabras, el nombre del contenedor a su vez es el nombre del servidor basado en el DNS asignado a la red.
- Se quita el puerto de la base de datos para asegurar que el contenedor quede aislado, siendo accedido solo por mongo-express.

``` yml
version: '3'

services:
  db:
    container_name: ${MONGO_DB_NAME}
    image: mongo:6.0
    volumes:
      - poke-vol:/data/db
    restart: always
    ports:
      # - 27017:27017
    environment:
      MONGO_INITDB_ROOT_USERNAME: ${MONGO_USERNAME}
      MONGO_INITDB_ROOT_PASSWORD: ${MONGO_PASSWORD}
    command: ['--auth']

  mongo-express:
    depends_on:
      - db
    image: mongo-express:1.0.0-alpha.4
    environment:
      ME_CONFIG_MONGODB_ADMINUSERNAME: ${MONGO_USERNAME}
      ME_CONFIG_MONGODB_ADMINPASSWORD: ${MONGO_PASSWORD}
      ME_CONFIG_MONGODB_SERVER: ${MONGO_DB_NAME}
    ports:
      - 8080:8081
    restart: always

volumes:
  poke-vol:
    external: false
```

### Aplicación NEST

``` yml
version: '3'

services:
  db:
    container_name: ${MONGO_DB_NAME}
    image: mongo:6.0
    volumes:
      - poke-vol:/data/db
    restart: always
    #ports:
      # - 27017:27017
    environment:
      MONGO_INITDB_ROOT_USERNAME: ${MONGO_USERNAME}
      MONGO_INITDB_ROOT_PASSWORD: ${MONGO_PASSWORD}
    command: ['--auth']

  mongo-express:
    depends_on:
      - db
    image: mongo-express:1.0.0-alpha.4
    environment:
      ME_CONFIG_MONGODB_ADMINUSERNAME: ${MONGO_USERNAME}
      ME_CONFIG_MONGODB_ADMINPASSWORD: ${MONGO_PASSWORD}
      ME_CONFIG_MONGODB_SERVER: ${MONGO_DB_NAME}
    ports:
      - 8080:8081
    restart: always
  
  poke-app:
    depends_on:
      - db
      - mongo-express
    image: klerith/pokemon-nest-app:1.0.0
    ports:
      - 3000:3000
    environment:
      MONGODB: mongodb://${MONGO_USERNAME}:${MONGO_PASSWORD}@${MONGO_DB_NAME}:27017
      DB_NAME: ${MONGO_DB_NAME}
    restart: always

volumes:
  poke-vol:
    external: false
```

## Dockerfile
- Son instrucciones de cómo construir la imagen que va a ejecutar el código.
    - Se puede ver como el blueprint para la construcción de la imagen.
- Dockerizar es construir una imagen basado en el código deseado.
    - Proceso de tomar un código fuente y generar un imagen lista para montar y correrla en un contenedor.

### Ejemplo Cron-Ticker
- Se crea un nuevo proyecto usando npm init.
    - Se inicia el proyecto con la configuración inicial.
- Se instala node-cron:
    - npm i node-cron
- En el archivo app.js se coloca el çodigo de muestra para cron.

``` js
const cron = require('node-cron');

let times = 0;

cron.schedule('1-59/5 * * * * *', () => {
  console.log('Tick cada 5 segundos', times);
});

console.log('Inicio');
```

- Se crea el script start para ejecutar el comando node app.js

#### Dockerizacion
- La mayoría de las veces se inicia la imagen a partir de otras imágenes.
- Se inicia la imagen agregando Node.
    - Se aprecia que en la imagen de node, los tags traen el nombre de Alpine.
        - Esto indica que la imagen de Node ya provee de una versión de Linux lista para ser utilizada.
    - Por esta razón se inicia indicando el uso de Node.
    - Se especifica que se desea la tag 19.2-alpine.
    - La imagen de alpine ya trae una carpeta llamada /app que se usa para colocar la aplicación.
- CMD le indica a DOCKER qué hacer cuando se empiece a correr el contenedor de la imagen, por lo que se definen ahí los comandos para iniciar la aplicación.
- Se construye la imagen usando el siguiente comando.
  - --tag permite nombrar a la imagen.
  - Colocar : después del nombre dado a la imagen perite colocarle un tag.
  - El . es el path relativo de donde se encuentra el dockerfile.
  - Al construir la imagen, la capa CMD no se toma en cuenta ya que entra en juego cuando se corre un contenedor.
``` bash
docker build --tag cron-ticker .
```

``` dockerfile
FROM node:19.2-alpine3.16
# /app

# cd app
WORKDIR /app

# Dest /app
COPY app.js package.json ./

# Se instalan las dependencias
RUN npm install

# Se ejecuta el comando (script definido en package.json) para correr app
# CMD se usa para la última instrucción/es a ejecutar.
CMD [ "node", "app.js" ]

```
- Como buena práctica se tiene colocar los cmoando que menos cambian hasta arriba para que se guarden en caché a la hora de construir nuevas imágenes o descargarlas.

#### Reconstruir imágenes
- Se lleva a cabo cuando se hace actualización en el código fuente o para corregir errores en la imagen.
- Al momento de construir la imagen, si se espera que un archivo tenga cambios frecuentes en el futuro entonces debe ir en las capas inferiores:
  - El comando COPY se separa para poder colocar el archivo app.js al final, ya que este archivo puede cambiar más veces en lugar de package.json.
- Si se reconstruye la imagen colocando el mismo nombre y sin tag, entonces se irán almacenando las imágenes anteriores sin un repository ni tag, haciendolas dificil de identificar.

#### Colocar tag a una imagen
``` bash
docker image tag SOURCE[:TAG] TARGET_IMAGE[:TAG]
```
- Entonces, en SOURCE se coloca el nombre que aparece en REPOSITORY junto a su tag al enlistar las imágenes. Luego, se coloca el nuevo nombre y el tag.

``` bash
docker image tag cron-ticker:1.0.0 cron-ticker:bunny
```

- AL hacer esto se tienen dos registros en la lista de image ls con el nombre de cron-ticker pero con una Tag diferentes.
  - Estas dos imágenes apuntan a la misma imagen, lo cual se ve en IMAGE ID. 
  - Si esta es la última versión de la imagen, entonces el registro que dice LATEST en la tag también apunta a la misma imagen.
- De esta forma, al hacer rebuild a la imagen se tiene un historial de las imágenes anteriores que se pueden identificar por medio de su tag.

### Subir imagen a Docker Hub.
- A los repositorios en Docker Hub se les conoce como registros.
- Se crea una cuenta en Docker Hub y se crea un repositorio público.
- Se sube la imagen por medio del comando dado por la página:

``` bash
docker push username/imageName:tagname
```

- Entonces, se tiene:

``` bash
docker push houser97/cron-ticker:tagname
```

- Para poder subir la imagen se debe cambiar su nombre de REPOSITORY para que concuerde con houser97/cron-ticker

``` bash
 docker image tag cron-ticker:bunny houser97/cron-ticker
```

- Luego, se debe autenticar desde la terminar con los comandos:

``` bash
 docker login
```

- Se debe escribir el username en minúscula, tal como aparece en el comando para hacer push a una imagen.
- Se sube la imagen.

``` bash
docker push houser97/cron-ticker
```

- Si no se puso tag entonces toma el latest.
- Se puede hacer push de las otras impagenes de forma de no perder la referencia de las imágenes subidas al Docker Hub.
  - Se debe cambiar el nombre de REPOSITORY de la imagen que se desea subir con la diferencia de que ahora se especifica su tag.

``` bash
 docker image tag houser97/cron-ticker:latest houser97/cron-ticker:1.0.0
```

``` bash
docker push houser97/cron-ticker:1.0.0
```

- Solo las imágenes oficiales respetan que el latest sea el primero que aparece.

- NOTA: ya que la iamgen se contruyó desade la máquina virtual de linux la arquitectura de la imagen es esa, por lo que puede haber incompatibilidad si se quiere usar la imagen en otra arquitectura. Se resuelve en siguientes capítulos.

### Consumibr imágenes propias de Docker Hub
- Basta con crear un contenedor: 

``` bash
docker container run houser97/cron-ticker:1.0.0
```

### Pruebas automáticas al código
- Se usa para asegurar que el código cumpla con los objetivos antes de pasarlo a producción.
- Es una capa de seguridad de que la aplicación se comporta como se desea.
- Se instala la dependencia de jest en la aplicación.
``` bash
 npm i jest --save-dev
```

- El archivo de testing es:

``` js
const { syncDB } = require("../../tasks/sync-db");


describe('Pruebas en syncDB', () => {
    test('Debe ejecutar el proceso 2 veces', () => {
        syncDB();
        const times = syncDB();
        expect(times).toBe(2);
    })
})
```

- Se hace la prueba de que al retornar times en el código retorne el valor esperado.
- En el archivo package.json se debe escribir jest en el apartado de test.


#### Incorporación testing en la construcción
- Se colocarán los módulos de jest temporalmente en la imagen para que se puedan correr los tests.
- Una vez que los tests han pasado se eliminan los módulos de jest, ya que solo son necesarios durante el desarrollo y no en producción.
  - Se crea el .dockerignore para no copiar las carpetas de node_modules, ya que están optimizadas al sistema operativo en donde se instalaron.
- Este enfoque no es el mejor, ya que cada layer de la imagen incremental el peso de la imagen. En la sección de MultiStage se muestra la solución.

``` bash
FROM node:19.2-alpine3.16
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
```

### Forzar una plataforma en la construcción
- Se puede especificar una plataforma en el dockerfile usando:
``` dockerfile
FROM --platform=linux/amd64 node:18-alpine
```

### Buildx
- Es un constructor de imagen.
- Con docker buildx ls se enlistan los builders que se tienen por defecto.

``` docker
docker buildx create --name mybuilder --bootstrap --use
```

- Con el comando anterior se descarga una imagen y crea un contenedor.
  - Se puede utilizar para definir la construcción y que cree las imágenes con todas las versiones en una sola línea.
- Al enlistar las plataformas con buildx ls se aprecia que se tiene por defecto una seleccionada.
- Se cambia por medio de:

``` docker
docker buildx use mybuilder
```

- Se pueden ver las características del builder por medio de inspect.

``` docker
docker buildx inspect
```

- Dockerfile provee de dos variables de entorno:
  - BUILDPLATFORM
  - TARGETPLATFORM

- Se pueden usar de la siguiente manera:

 ``` dockerfile
FROM --platform=$BUILDPLATFORM node:19.2-alpine3.16
```
- De esta forma, esta imagen va a depenedner de todas las plataformas que el Builder maneja.
  - La variable recibe el valor directamente desde el comando de builx build.
- Por otro lado, con el siguiente comando se especifican las plataformas:

 ``` bash
docker buildx build --platform linux/amd64,linux/arm64,linux/arm/v7 -t <username>/<image>:latest --push .
```

- Se debe verificar que las plataformas estén disponibles en el builder para poder hacerlo.
 ``` bash
docker buildx build --platform linux/amd64,linux/amd64/v2,linux/386 \
> -t houser97/cron-ticker --push .
```

- Por otro lado, si en el dockerfile no se usa la variable de entorno y se deja como el Dockerfile original, entonces el comando buildx build va a crear varias imágenes para cada arquitectura.

### Eliminar buildx
- Se debe cambiar de buildx antes de poder borrarlo:
 ``` bash
docker buildx use default
```

- Luego, se elimina:

 ``` bash
docker buildx rm mybuilder
```

## Notas
- Al levantar la imagen por primera vez, la base de datos ya se habrá configurado por medio de las variable de entorno, por lo que un cambio en estas variable en el Docker Compose no serán tomadas debido a que el volumen persiste la data de los primero valores dados a las variables de entorno.
- Cada Layer en la imagen agrega peso a la imagen final.

## Buenas prácticas
### Push
- Después de hacerle push a una imagen con su tag se debería hacer un segundo push sin la tag para que en Docker Hub latest y la última imagen subida coincidan.
### Dockerfile
- Las capas que no cambian se colocan hasta arriba del dockerfile para que se guarden en caché.
- No copiar todo el systemfile al contenedor, ya que se van carpetas como node_modules, carpetas invisibles como git. Se puede copiar todo si se usa dockerignore.
### Dockerignore
- EL archivo no se ignora a sí mismo, por lo que se debe especificar también.