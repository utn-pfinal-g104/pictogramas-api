## Instrucciones para instalar por primera vez

1) Descargas
	- SQL Server Management Studio
	- IDE (para la web y la api)
	- Microsoft Azure Storage Emulator (verificar si ya lo tenes porque puede venir con Windows)
	- Microsoft Azure Storage Explorer (es una interfaz gráfica para ver el storage)
	- npm

2) Crear tablas en Base de datos. Ejecutar el script

3) Clonar [ApiPictogar](https://github.com/utn-pfinal-g104/pictogramas-api.git) y [WebPictogar](https://github.com/utn-pfinal-g104/pictogramas-web.git).

4) Correr Storage Emulator. Abrir Storage Explorer y crear la carpeta pictogramas para que sea ahí donde la api vaya guardando los recursos allí.

5) Levantar la Api, pero antes completar los siguientes 3 pasos:
	a - Comentar la excepción del archivo ActualizacionStorageJob, en el método ActualizarPictogramas.
	b - Cambiar ConnectionString de appsettings.development.json para que apunte a su base de datos local.
	![image](https://user-images.githubusercontent.com/26606912/186019466-171c22b9-7ed0-4b75-8a8e-f578d281b27c.png)

	c - Cambiar ConnectionString del storage en appsettings.development.json para que apunte al emulador.
	![image](https://user-images.githubusercontent.com/26606912/186019642-1268fd69-799c-4bd2-a90d-654a8b8d8b17.png)

6) Una vez que levanten la Api, ejecutar GET sobre https://localhost:5001/pictogramas/guardar, que comenzará con el proceso de completar la base de datos y la descarga de pictogramas. Es un proceso que demora bastante porque realiza también los insert en el storaga, así que dejenlo corriendo y vayan a tomar un café ☕

Cuando comiencen a descargar los pictogramas, se puede ver el siguiente log en la consola de VisualStudio:
![image](https://user-images.githubusercontent.com/26606912/186018838-8f303a9a-db71-4d51-afa3-dd4486dc83c8.png)


7) Abrir proyecto web y ejecutar npm install y npm start. Crear una cuenta y click en el botón "descargar pictogramas". Esto lo que hace es insertar en la indexDB del browser. Este proceso también tarda porque hay un proceso que tiene que convertir las imágenes de base64 a otro formato antes de insertarlas directamente.
