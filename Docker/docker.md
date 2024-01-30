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
    - Docker compose lee por defecot los archivo .env que se encuentran al mismo nivel.