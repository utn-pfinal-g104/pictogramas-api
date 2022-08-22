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

5) Levantar la Api, pero antes:
	- Comentar la excepción del archivo ActualizacionStorageJob, en el método ActualizarPictogramas.
	- Cambiar ConnectionString de development para que apunte a su base de datos local.
	![image](https://user-images.githubusercontent.com/26606912/186019466-171c22b9-7ed0-4b75-8a8e-f578d281b27c.png)

	- Cambiar ConnectionString del storage para que apunte al emulador.
	![image](https://user-images.githubusercontent.com/26606912/186019642-1268fd69-799c-4bd2-a90d-654a8b8d8b17.png)


Cuando comiencen a descargar los pictogramas, se puede ver el siguiente log en la consola de VisualStudio:
![image](https://user-images.githubusercontent.com/26606912/186018838-8f303a9a-db71-4d51-afa3-dd4486dc83c8.png)



6) Abrir proyecto web y ejecutar npm install y npm start. Crear una cuenta y verificar que se guarde en la base de datos sql y en la indexDb del browser.
