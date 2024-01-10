# Docker
- Se ven los comandos disponibles con:

``` bash
docker container --help
docker image --help
```

# Volúmenes y Redes
- Se va a crear una red para unir una DB y PHP MyAdm.

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

