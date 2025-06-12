# JuegoMarvel
En este repositorio se alojara lo que prenten ser el TFG de mi Grado Superior de DAM, es un juego de marvel multijugador 1 vs 1
# Por donde empezar con este proyecto:
Primero Empezemos con las Tecnologias necesarias:
* Primero tienes que tener windows 10 o 11.
*  Visual Studio community(si es el pro mejor) 2022 con los paquetes necesarios para hacer Aplicaciones en MAUI (se instalan en el instalador de Visual Studio)
*   Un disposito android en el que probarlo o poder ejecutar el Simulador de Android sin problemas(recomiendo un dispositivo movil, el simulador suele fallar)
*   Instalar DBrowser para poder ver la Base de datos del Movil
*   MySQL Workbench para poder crear la Base de datos necesaria para poder ejecutar estas sentencias.

Para que puedas utilizar este repositorio necesitas:
* Clonar los siguiente repositorios:
  *  ServidorJuegoMarvel: Servidor Multihilo que gestiona los clientes en el Modulo login.
  *  ServidorJuegoMultijudadorMarvel: Servidor que solo acepta dos clientes para poder interactuar entre ellos. 
  *  JuegoMarvel: La aplicación principal y cliente del ServidorJuegoMarvel.
  *  JuegoMarvelData: Son las clases que Mappean la Base de datos embebida del Cliente (Juego Marvel)
  *  MensajesServidor: Clases que sirven para Serializar en Json y por enviar los mensajes al Servidor.
Una vez tengas los repositorios clonados Necesitaras dos Scriptes de la base de datos para poder gestionarla:
* Script Base de datos de MySQL:
* Script Base de datos de SQLite(Opcional):
La base de datos de SQLite no necesitas tocarla y ni hacer nada con el JuegoMarvel

Una vez iniciado esto tienes que cambiar la dirección IP y el puerto(si lo consideras necesario) de los servidores, los dos tiene que tener la misma dirección IP pero distintos Puertos, para que funcione.
Tienes que poner en la aplicación en de JuegoMarvel en appsettings.json que esta en la raiz del proyecto, cambiar los valores del ip y los puertos por los que has puesto en los servidores, recuerda la clave PuertoServidor es el del Servidor Multicliente y la clave PuertoServidorJuego es el multijugador.
Una vez configurado todo 
  
