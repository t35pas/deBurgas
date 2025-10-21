// src/Controllers/MenuController.cs
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult GetFullMenu()
    {
        // Devuelve la estructura ensamblada lista para ser consumida por React
        return Ok(_menuService.GetAssembledMenu());
    }
}