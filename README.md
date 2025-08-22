ServicioWebApiUser

Una API RESTful moderna desarrollada en ASP.NET Core Web API para la gestión de usuarios, con autenticación JWT, autorización por roles, y operaciones CRUD completas.
  Características

    ASP.NET Core 7.0 – Framework de alto rendimiento.

    Entity Framework Core – ORM para acceso a datos con Code-First.

    SQL Server – Base de datos principal.

    Autenticación JWT – Seguridad con tokens Bearer.

    Autorización por Roles – Control de acceso (Admin, User, etc.).

    Swagger (OpenAPI) – Documentación interactiva.

    Patrón Repository – Separación de preocupaciones.

    DTOs (Data Transfer Objects) – Modelos de transferencia optimizados.

    AutoMapper – Mapeo entre entidades y DTOs.

    Logging – Registro de eventos con ILogger.

    Migraciones – Control de versiones de base de datos.

  Estructura del Proyecto
text

ServicioWebApiUser/
├── Controllers/           # Controladores de la API
├── Models/               # Entidades de EF Core
├── Data/                 # Contexto de base de datos y configuración
├── DTOs/                 # Objetos de transferencia de datos
├── Repository/           # Implementación del patrón Repository
├── Services/             # Lógica de negocio y servicios
├── Migrations/           # Migraciones de base de datos
├── appsettings.json      # Configuración de la aplicación
└── Program.cs            # Punto de entrada de la aplicación

  Requisitos Previos

    .NET 7.0 SDK

    SQL Server (LocalDB, Express o superior)

    Visual Studio 2022, Rider o VS Code

  Configuración e Instalación
1. Clonar el repositorio
bash

git clone https://github.com/kure-doc/ServicioWebApiUser.git
cd ServicioWebApiUser

2. Configurar la base de datos

Modifica el archivo appsettings.json con tu cadena de conexión:
json

"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR;Database=WebApiUserDB;Trusted_Connection=True;TrustServerCertificate=True;"
}

3. Aplicar migraciones

Ejecuta desde la consola de paquetes NuGet o CLI:
bash

dotnet ef database update

4. Ejecutar la aplicación
bash

dotnet run

O ejecuta desde Visual Studio con F5 o Ctrl + F5.
  Endpoints de la API
Autenticación

    POST /api/auth/login
    Iniciar sesión y obtener token JWT.

    POST /api/auth/register
    Registrar nuevo usuario.

Usuarios (requiere autenticación)

    GET /api/users
    Obtener todos los usuarios (solo Admin).

    GET /api/users/{id}
    Obtener usuario por ID.

    PUT /api/users/{id}
    Actualizar usuario.

    DELETE /api/users/{id}
    Eliminar usuario (solo Admin).

  Autenticación JWT

Incluye el token en las solicitudes mediante el header:
text

Authorization: Bearer {token}

Puedes obtener un token usando el endpoint /api/auth/login con un usuario existente.
  Ejemplos de Uso con curl
Login
bash

curl -X POST "https://localhost:7000/api/auth/login" \
-H "Content-Type: application/json" \
-d '{
  "email": "admin@example.com",
  "password": "Admin123!"
}'

Obtener todos los usuarios (como Admin)
bash

curl -X GET "https://localhost:7000/api/users" \
-H "Authorization: Bearer {token}"

Roles Disponibles

    Admin: Acceso total a todos los endpoints.

    User: Acceso limitado a su propia información.

Migraciones

Para agregar una nueva migración:
bash

dotnet ef migrations add NombreMigración
dotnet ef database update

Testing

Ejecuta las pruebas con:
bash

dotnet test

Dependencias Principales

    Microsoft.EntityFrameworkCore.SqlServer

    Microsoft.AspNetCore.Authentication.JwtBearer

    AutoMapper.Extensions.Microsoft.DependencyInjection

    Swashbuckle.AspNetCore

    Microsoft.EntityFrameworkCore.Tools

Solución de Problemas

    Error de conexión a BD: Verifica la cadena de conexión en appsettings.json.

    Error de migraciones: Ejecuta dotnet ef migrations add Init y luego dotnet ef database update.

    Token inválido: Revisa la configuración JWT en appsettings.json.

Contribuir

¡Las contribuciones son bienvenidas!

    Haz un fork del proyecto.

    Crea una rama: git checkout -b feature/nueva-funcionalidad.

    Realiza tus cambios y commitea: git commit -m 'Agrega nueva funcionalidad'.

    Haz push a la rama: git push origin feature/nueva-funcionalidad.

    Abre un Pull Request.

Licencia

Este proyecto está bajo la Licencia MIT. Consulta el archivo LICENSE para más detalles.
Autor

Desarrollado por kure-doc.
Contacto

Si tienes preguntas o sugerencias, no dudes en abrir un issue o enviar un pull request.

¡Si te gusta el proyecto, dale una ⭐ en GitHub!
