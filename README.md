# KataDotNetPossumus
Challenge Wallet Kata

# WalletService

Este es un microservicio de wallet construido con .NET Core 6.0 que permite a los usuarios realizar depósitos, retiros, intercambios y consultar el balance de su wallet. El servicio utiliza autenticación y autorización JWT para asegurar que solo los usuarios autenticados puedan acceder y modificar sus propias wallets.

## Características

- **Deposit**: Permite depositar diferentes tipos de monedas (ARS, USD, EUR, etc.) en la wallet.
- **Withdraw**: Permite retirar diferentes tipos de monedas de la wallet.
- **Exchange**: Permite intercambiar una moneda por otra utilizando una tasa de cambio referencial.
- **GetBalance**: Permite obtener el balance de la wallet con la cantidad de dinero de cada moneda.

## Tecnologías Utilizadas

- .NET Core 6.0
- SQL como base de datos
- Entity Framework para acceso a la base de datos
- JWT (JSON Web Tokens) para autenticación y autorización
- Xunit para pruebas unitarias
- Swagger para documentación de la API

## Configuración del Proyecto

### Requisitos Previos

- .NET 6.0 SDK
- Visual Studio 2022 o Visual Studio Code
- SQL Server Management Studio 19 

### Clonar el Repositorio

El codigo fuente se puede encontrar en la siguiente url: https://github.com/morenoEmmanuel/KataDotNetPossumus

### Base de datos

Dentro de la carpeta "Data Base" se encuentra un script para la creacion de la base de datos que se debe utilizar.

## Documentación con Swagger

Swagger está configurado para proporcionar documentación interactiva de la API. 
Cuando ejecutas el proyecto, puedes acceder a Swagger en https://localhost:7069/swagger/index.html para ver y probar los endpoints de la API.
