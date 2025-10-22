// src/Services/MenuService.cs
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using deBurgas.Data; // Asume que AppDbContext está aquí
using deBurgas.Models.Entities; // Asume que tus entidades están aquí

// Nota: Reemplaza TuProyecto.Data y TuProyecto.Models.Entities por tus namespaces reales.

public class MenuService
{
    // Usamos IServiceScopeFactory para resolver el DbContext
    // Esto es NECESARIO si el servicio se registra como Singleton (que es lo ideal para el menú)
    private readonly IServiceScopeFactory _scopeFactory;

    public MenuService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    /// <summary>
    /// Método principal para obtener el menú completo ensamblado para el frontend.
    /// Carga los productos, sus modificadores y sus opciones con una sola consulta eficiente.
    /// </summary>
    public async Task<object> GetAssembledMenuAsync()
    {
        // Usamos un nuevo scope para DbContext porque MenuService es Singleton
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // 1. Cargar todos los datos necesarios en memoria con 'Eager Loading'
        // Esto es un patrón común para minimizar las consultas a la DB (N+1 problem).
        var catalogItems = await context.CatalogItems
            .Include(item => item.ItemModifiers) // Relación M:N
            .ThenInclude(im => im.Modifier)       // Incluye el Modificador
            .ThenInclude(m => m.Options)         // Incluye las Opciones del Modificador
            .ToListAsync();

        // 2. Ensamblar la estructura de respuesta para el Frontend
        var menu = catalogItems.Select(item => new
        {
            item.ProductId,
            item.Name,
            item.BasePrice,
            item.ProductType,
            item.Description,
            item.ImageUrl,
            // Ensamblamos los modificadores a partir de la relación M:N
            Modifiers = item.ItemModifiers
                .Select(im => im.Modifier)
                .Where(mod => mod != null)
                .Select(mod => new
                {
                    mod.ModifierId,
                    mod.Name,
                    mod.SelectionType,
                    // Las Opciones ya vienen cargadas dentro del Modificador
                    Options = mod.Options.Select(opt => new
                    {
                        opt.OptionId,
                        opt.Name,
                        opt.AdditionalPrice
                    })
                })
                .ToList()
        }).ToList();

        return menu;
    }
}