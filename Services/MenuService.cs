using System;
using System.Collections.Generic;

// src/Services/MenuService.cs
public class MenuService
{
    private readonly List<CatalogItem> _catalogItems;
    private readonly List<Modifier> _modifiers;
    private readonly List<Option> _options;

    public MenuService()
    {
        // --- 1. Definición de Opciones ---
        var optCheddar = new Option(Guid.NewGuid(), "Cheddar", 0.50m);
        var optMozzarella = new Option(Guid.NewGuid(), "Mozzarella", 0.75m);
        var optBbq = new Option(Guid.NewGuid(), "Salsa BBQ", 0.00m);
        var optPapas = new Option(Guid.NewGuid(), "Papas Grandes", 2.50m);

        _options = new List<Option> { optCheddar, optMozzarella, optBbq, optPapas };

        // --- 2. Definición de Modificadores ---
        var modQueso = new Modifier(
            Guid.NewGuid(),
            "Tipo de Queso",
            "single", // Solo se puede elegir uno
            new List<Guid> { optCheddar.OptionId, optMozzarella.OptionId }
        );

        var modSalsas = new Modifier(
            Guid.NewGuid(),
            "Salsas Extras",
            "multiple", // Permite varias selecciones
            new List<Guid> { optBbq.OptionId }
        );

        _modifiers = new List<Modifier> { modQueso, modSalsas };

        // --- 3. Definición del Producto (Hamburguesa) ---
        var burgerId = Guid.NewGuid();
        var classicBurger = new CatalogItem(
            burgerId,
            "Classic Burger",
            "BURGER",
            10.00m,
            "Nuestra hamburguesa clásica con lechuga y tomate.",
            "https://example.com/images/classic.jpg",
            new List<Guid> { modQueso.ModifierId, modSalsas.ModifierId } // Asociamos los modificadores
        );

        _catalogItems = new List<CatalogItem> { classicBurger };
    }

    // Método para obtener el catálogo completo (incluyendo reglas de configuración)
    public List<CatalogItem> GetFullCatalog() => _catalogItems;
    public List<Modifier> GetModifiers() => _modifiers;
    public List<Option> GetOptions() => _options;

    // Método para obtener el menú completo ensamblado para el frontend
    public object GetAssembledMenu()
    {
        // Esto simula la lógica de unir el Product con sus Modifiers y Options
        var menu = _catalogItems.Select(item => new
        {
            item.ProductId,
            item.Name,
            item.BasePrice,
            item.ProductType,
            // Ensamblamos los modificadores
            Modifiers = item.ModifierIds
                .Select(id => _modifiers.Find(m => m.ModifierId == id))
                .Where(m => m != null)
                .Select(mod => new
                {
                    mod.ModifierId,
                    mod.Name,
                    mod.SelectionType,
                    Options = mod.OptionIds
                        .Select(optId => _options.Find(o => o.OptionId == optId))
                        .Where(o => o != null)
                })
        }).ToList();

        return menu;
    }
}