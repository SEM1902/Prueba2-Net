# Prueba .NET - Prueba Técnica Backend

Este proyecto es una aplicación web ASP.NET Core MVC diseñada para gestionar productos y consultar tipos de cambio en tiempo real.

## 🚀 Características
- **CRUD de Productos**: Gestión completa de productos (Crear, Leer, Actualizar, Eliminar).
- **Procedimientos Almacenados**: Todas las operaciones de datos se realizan mediante Stored Procedures en SQL Server.
- **Consumo de API Externa**: Integración con Frankfurter API para mostrar tipos de cambio USD.
- **Diseño Premium**: Interfaz moderna con Bootstrap 5, glassmorphism, tipografía "Outfit" y componentes visuales atractivos.
- **Conexión Resiliente**: Soporte para autenticación SQL (`sa`) y fallback automático a Seguridad Integrada (Windows/Mac Auth).

## 🛠️ Stack Tecnológico
- **Backend**: .NET 10.0 (C#)
- **Base de Datos**: SQL Server
- **Acceso a Datos**: ADO.NET (Microsoft.Data.SqlClient)
- **Frontend**: ASP.NET Core MVC (Razor Views), Bootstrap 5

## 📋 Configuración y Ejecución

### 1. Base de Datos
1. Abra su cliente SQL (DBeaver, SSMS, etc.).
2. Cree una base de datos llamada `ProductDB`.
3. Ejecute el script contenido en [DatabaseSetup.sql](DatabaseSetup.sql) para crear la tabla y los procedimientos almacenados.

### 2. Configuración de la Aplicación
1. Abra el archivo `appsettings.json` en la raíz del proyecto.
2. Actualice la cadena de conexión `DefaultConnection` con sus credenciales de SQL Server:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ProductDB;User Id=sa;Password=DevPass123!;TrustServerCertificate=True;",
  "LocalAuthConnection": "Server=localhost;Database=ProductDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 3. Ejecutar el Proyecto
Desde la terminal en la carpeta raíz del proyecto (`Prueba-Net`):
```bash
dotnet run
```
La aplicación estará disponible en `http://localhost:5212` (o el puerto que indique la terminal).

## 🌐 API Consumida
- **URL**: `https://api.frankfurter.app/latest?from=USD`
- **Uso**: Se utiliza para mostrar la conversión de 1 USD a diversas monedas globales en la sección "Global Analytics".
