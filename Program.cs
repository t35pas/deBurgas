using Microsoft.AspNetCore.Mvc;

// 1. Configuración de la Aplicación
var builder = WebApplication.CreateBuilder(args);

// 1. Agregar Servicios: Registro de los controladores y Swagger
builder.Services.AddControllers(); // <-- ¡Asegura que esto esté aquí!
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// ... (Tus servicios MenuService, CORS, etc.)
builder.Services.AddSingleton<MenuService>();
builder.Services.AddSingleton<MetaNotificationService>();

// 2. MIDDLEWARE: Configuración de la aplicación
var app = builder.Build();

// Si está en desarrollo, habilitar Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ... (Otros middlewares como CORS o HSTS)

// Middleware de ruteo
app.UseAuthorization();
app.MapControllers(); // <-- ¡Asegura que este mapeo esté aquí!

app.Run();