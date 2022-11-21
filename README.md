"# pictogramas-api" 

# Endpoints API REST (propios)

**GET** / - *monitorea el estado de la API REST (health check).*

**GET** /actualizarPictogramas - *Actualiza los pictogramas en base a la información provista por ARASAAC, ejecuta el job de actualización a demanda.*

**GET** /categorias/ - *Obtención de categorías.*
**GET** /categorias/{idCategoria}/ - *Obtiene la imagen de una categoría en base64.*
**GET** /categorias/{idCategoria}/pictogramas - *Obtiene los pictogramas asociados a una categoría.*

**GET** /estadisticas/{idUsuario}/recientes/ - *Obtiene pictogramas utilizados recientemente por usuario.*
**GET** /estadisticas/{idUsuario}/ - *Obtiene las estadísticas completas de un  usuario.*
**POST** /estadisticas/ - *Agrega registro de estadísticas.*

**GET** /imagenes/pictogramas - *Obtiene el nombre de todos los archivos subidos al storage de pictogramas.*
**GET** /imagenes/pictogramas/{idPictograma} - *Obtiene la imagen de un pictograma con posibilidad de descargar en base64.*
**POST** /imagenes/pictogramas - *Carga una imagen para un pictograma en el storage.*
**DELETE** /imagenes/pictogramas/{idPictograma} - *Borra una imagen de pictograma del storage.*

**GET** /interpretacion - *Genera una interpretación natural para una frase.*

**GET** /keywords/ - *Obtiene palabras claves.*

**POST** /login/ - *Autentica un usuario.*

**GET** /pictogramas/ - *Obtiene información de pictogramas. Opcionalmente, se puede filtrar por nombre de pictograma o nombre de categoría.*
**DELETE** /pictogramas/{idPictograma} - *Borra un pictograma personalizado por el usuario.*

**GET** /pizarras/ - *Obtiene las pizarras. Opcionalmente, se puede filtrar por id de usuario.*
**POST** /pizarras/ - *Crea una pizarra.*
**PUT** /pizarras/ - *Actualiza una pizarra.*
**DELETE** /pizarras/{idPizarra} - *Elimina una pizarra.*

**GET** /usuarios/ - *Obtiene los usuarios.*
**GET** /usuarios/{id:int} - *Obtiene un usuario determinado.*
**GET** /usuarios/{idUsuario}/categorias/ - *Obtiene categorías por usuario.*
**POST** /usuarios/{idUsuario}/categorias/{idCategoria} - *Crea una relación entre categoría y usuario.*
**GET** /usuarios/{idUsuario}/favoritos/ - *Obtiene los pictogramas favoritos del usuario.*
**POST** /usuarios/ - *Crea un usuario.*
**POST** /usuarios/{idUsuario}/pictogramas - *crea un pictograma personalizado por el usuario.*
**POST** /usuarios/{idUsuario}/favoritos/ - *Agrega a favoritos del usuario un determinado pictograma.*
**PUT** /usuarios/ - *Actualiza configuración de un usuario.*
**PATCH** /usuarios/ - *Actualiza contraseña de un usuario.*
**DELETE** /usuarios/{idUsuario}/categorias/{idCategoria} - *Elimina una relación de categoría y usuario.*
**DELETE** /usuarios/{idUsuario}/favoritos/{idPictograma} - *Quita de favoritos del usuario a un determinado pictograma.*

# APIs externas
[APIs ARASAAC](https://arasaac.org/developers/api)
