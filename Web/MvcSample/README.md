# MvcSample - Aplicación de Gestión de Granjas

Una aplicación web ASP.NET Core moderna construida con Razor Pages y .NET 10, diseñada para gestionar granjas y sus operaciones. La aplicación implementa una arquitectura de capas siguiendo principios de diseño limpio y sostenible.

## 📋 Descripción General

MvcSample es una aplicación empresarial que demuestra las mejores prácticas en desarrollo de aplicaciones ASP.NET Core. Proporciona una plataforma para gestionar granjas con funcionalidades de autenticación, autorización y administración de datos.

## ✨ Características Principales

- **Autenticación y Autorización**: Sistema seguro de registro e inicio de sesión mediante ASP.NET Core Identity
- **Gestión de Granjas**: CRUD completo para la administración de granjas
- **Interfaz de Usuario Responsiva**: Interfaz moderna y amigable para usuarios
- **Validación de Datos**: Validación robusta en cliente y servidor
- **Arquitectura en Capas**: Separación clara de responsabilidades entre capas de presentación, servicios, dominio e infraestructura

## 🏗️ Estructura del Proyecto

```
MVCSampleFinal/
├── Web/
│   └── MvcSample/                  # Proyecto principal - Razor Pages ASP.NET Core
│       ├── Areas/
│       │   └── Identity/           # Páginas de autenticación (Login, Register, etc.)
│       ├── Controllers/
│       │   ├── FarmController.cs   # Controlador para gestión de granjas
│       │   └── HomeController.cs   # Controlador de inicio
│       ├── Pages/                  # Páginas Razor
│       ├── Program.cs              # Configuración de la aplicación
│       └── MvcSample.csproj
│
├── Domain/                         # Capa de Dominio - Modelos y entidades
│   └── Domain.csproj
│
├── Services/                       # Capa de Servicios - Lógica de negocio
│   └── Services.csproj
│
├── Infrastructure/                # Capa de Infraestructura - Acceso a datos
│   └── Infrastructure.csproj
│
└── Test/                          # Pruebas Unitarias
    ├── DomainTest/
    ├── ServicesTest/
    └── ...
```

## 🛠️ Tecnologías Utilizadas

- **Framework**: ASP.NET Core con .NET 10
- **Patrón de Presentación**: Razor Pages
- **Autenticación**: ASP.NET Core Identity
- **Base de Datos**: SQL Server (tipicamente)
- **Testing**: Pruebas unitarias en Domain y Services
- **Arquitectura**: Arquitectura en Capas (Layered Architecture)

## 📦 Requisitos Previos

- .NET 10 SDK o superior
- Visual Studio 2026 (Community Edition o superior)
- SQL Server 2019 o superior (u otra base de datos configurada)
- Git

## 🚀 Instalación y Configuración

### 1. Clonar el Repositorio

```bash
git clone https://github.com/camchaca/MVCSampleFinalCore9.git
cd MVCSampleFinal
```

### 2. Restaurar Dependencias

Desde la raíz del proyecto:

```bash
dotnet restore
```

### 3. Configurar la Base de Datos

Actualiza la cadena de conexión en `appsettings.json` según tu entorno:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=MvcSampleDb;Trusted_Connection=true;"
  }
}
```

### 4. Ejecutar Migraciones

```bash
dotnet ef database update
```

### 5. Ejecutar la Aplicación

```bash
cd Web/MvcSample
dotnet run
```

La aplicación estará disponible en `https://localhost:5001`

## 📚 Guía de Uso

### Autenticación
1. Navega a la página de registro
2. Crea una nueva cuenta con correo electrónico y contraseña
3. Inicia sesión con tus credenciales

### Gestión de Granjas
1. Una vez autenticado, accede al módulo de granjas
2. Visualiza, crea, edita o elimina granjas según sea necesario

## 🧪 Pruebas

Para ejecutar las pruebas unitarias:

```bash
dotnet test
```

O desde Visual Studio, usa el Explorador de Pruebas.

## 📁 Archivos Principales

- `Program.cs` - Configuración de la aplicación y inyección de dependencias
- `Controllers/FarmController.cs` - Lógica de controlador para granjas
- `Controllers/HomeController.cs` - Controlador de página de inicio
- `Areas/Identity/` - Páginas de autenticación

## 🔒 Seguridad

- La aplicación utiliza ASP.NET Core Identity para autenticación
- Las contraseñas se almacenan hasheadas
- Se implementan validaciones en servidor y cliente
- HTTPS está habilitado en producción

## 🤝 Contribuir

Las contribuciones son bienvenidas. Por favor:

1. Crea un fork del proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📝 Licencia

Este proyecto está bajo licencia MIT. Consulta el archivo `LICENSE` para más detalles.

## 👨‍💻 Autor

**Camilo Charry** - [@camchaca](https://github.com/camchaca)

## 📧 Contacto

Para preguntas o sugerencias, contacta al autor o abre un issue en el repositorio.

---

**Última actualización**: 2024
