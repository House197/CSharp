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

