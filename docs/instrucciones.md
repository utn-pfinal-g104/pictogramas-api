## Instrucciones para instalar por primera vez

1) Descargas
	- SQL Server Management Studio
	- IDE (para la web y la api)
	- Microsoft Azure Storage Emulator (verificar si ya lo tenes porque puede venir con Windows)
	- Microsoft Azure Storage Explorer (es una interfaz gráfica para ver el storage)
	- npm

2) Crear tablas en Base de datos. Ejecutar el script

3) Clonar [ApiPictogar](https://github.com/utn-pfinal-g104/pictogramas-api.git)  y [WebPictogar](https://github.com/utn-pfinal-g104/pictogramas-web.git).

4) Correr Storage Emulator. Abrir Storage Explorer y crear la carpeta pictogramas para que sea ahí donde la api vaya guardando los recursos allí.

5) Levantar la Api, pero antes:
	- Comentar la excepción del archivo ActualizacionStorageJob, en el método ActualizarPictogramas.
	
	- Cambiar ConnectionString de development para que apunte a su base de datos local


6) Abrir proyecto web y ejecutar npm install y npm start. Crear una cuenta y verificar que se guarde en la base de datos sql y en la indexDb del browser.