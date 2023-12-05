# tekton-challenge
Prueba tecnica de Tekton 2023-11-30

## Patrones Utilizados

- Arquitectura Limpia, el cual estructura la aplicacion en 4 capas: Infraestructura, Dominio, Aplicacion y WebApi.
- Mediator, el cual se implementa en la capa de WebApi para realizar llamadas a los commands y queries mediante MediatR. 
- Patron Repositorio, el cual sirve para crear una capa de abstraccion entre el acceso a datos (Infraestructura) y la logica (Aplicacion).
- CQRS, el cual sirve para ordenar las llamadas en la logica (Aplicacion) mediante Commands y Queries.

## Requisitos Previos

- .NET 5.0 SDK o una versión posterior.
- SQLite.

## Ejecucion del Proyecto

- Abre una terminal o linea de comandos.
- Navega al directorio del proyecto (donde se encuentra el .csproj).
- Restaurar las dependencias del proyecto mediante el comando: `dotnet restore`.
- Construir el proyecto mediante el comando: `dotnet build`.
- Ejecutar el proyecto mediante el comando: `dotnet run`.
- Ejecutar las pruebas unitarias mediante el comando: `dotnet test`.
