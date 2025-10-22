// src/Controllers/MenuController.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly MenuService _menuService;

    public MenuController(MenuService menuService)
    {
        _menuService = menuService;
    }

    [HttpGet("full")]
    public async Task<IActionResult> GetFullMenu()
    {
        // 1. Usar 'var' para que C# infiera el tipo anónimo.
        var menu = await _menuService.GetAssembledMenuAsync();

        // 2. Comprobar si es null, y si es una colección enumerable no vacía.
        //    Usamos 'as' para intentar la conversión a la interfaz no genérica.
        var menuList = menu as IEnumerable;

        if (menuList == null || !menuList.GetEnumerator().MoveNext())
        {
            // La lista está vacía o es null.
            return NotFound("El menú no se pudo cargar o está vacío.");
        }

        // 3. Devolvemos el resultado directamente. El serializador JSON (System.Text.Json)
        //    es lo suficientemente inteligente como para manejar los tipos anónimos.
        return Ok(menu);
    }
}