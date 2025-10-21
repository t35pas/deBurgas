using Microsoft.AspNetCore.Mvc;

// 1. Configuraci�n de la Aplicaci�n
var builder = WebApplication.CreateBuilder(args);

// 1. Agregar Servicios: Registro de los controladores y Swagger
builder.Services.AddControllers(); // <-- �Asegura que esto est� aqu�!
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// ... (Tus servicios MenuService, CORS, etc.)
builder.Services.AddSingleton<MenuService>();
builder.Services.AddSingleton<MetaNotificationService>();

// 2. MIDDLEWARE: Configuraci�n de la aplicaci�n
var app = builder.Build();

// Si est� en desarrollo, habilitar Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ... (Otros middlewares como CORS o HSTS)

// Middleware de ruteo
app.UseAuthorization();
app.MapControllers(); // <-- �Asegura que este mapeo est� aqu�!

app.Run();