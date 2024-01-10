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

## Hacer base de datos de MariaDB en docker persistente
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

