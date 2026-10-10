using GameVault.Api.Data;
using GameVault.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Api.Controllers;

public class VideojuegosController : ODataController
{
    private readonly GameVaultContext _contexto;

    public VideojuegosController(GameVaultContext contexto)
    {
        _contexto = contexto;
    }

    // GET /odata/Videojuegos
    // Acepta $expand=Resenas, $filter=Plataforma eq 'PC', $orderby, $select y $count.
    [EnableQuery(MaxExpansionDepth = 2, PageSize = 100)]
    public IQueryable<Videojuego> Get() => _contexto.Videojuegos.AsNoTracking();

    // GET /odata/Videojuegos(3)?$expand=Resenas
    [EnableQuery(MaxExpansionDepth = 2)]
    public SingleResult<Videojuego> Get([FromRoute] int key) =>
        SingleResult.Create(_contexto.Videojuegos.AsNoTracking().Where(juego => juego.Id == key));

    // GET /odata/Videojuegos(3)/Resenas
    [EnableQuery]
    public IQueryable<Resena> GetResenas([FromRoute] int key) =>
        _contexto.Resenas.AsNoTracking().Where(resena => resena.VideojuegoId == key);

    // POST /odata/Videojuegos
    public async Task<IActionResult> Post([FromBody] Videojuego juego)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        juego.Id = 0;
        _contexto.Videojuegos.Add(juego);
        await _contexto.SaveChangesAsync();

        return Created(juego);
    }

    // PATCH /odata/Videojuegos(3)
    public async Task<IActionResult> Patch([FromRoute] int key, [FromBody] Delta<Videojuego> cambios)
    {
        var juego = await _contexto.Videojuegos.FirstOrDefaultAsync(j => j.Id == key);

        if (juego is null)
        {
            return NotFound();
        }

        cambios.Patch(juego);

        if (!TryValidateModel(juego))
        {
            return BadRequest(ModelState);
        }

        await _contexto.SaveChangesAsync();
        return Updated(juego);
    }

    // DELETE /odata/Videojuegos(3)
    public async Task<IActionResult> Delete([FromRoute] int key)
    {
        var juego = await _contexto.Videojuegos.FirstOrDefaultAsync(j => j.Id == key);

        if (juego is null)
        {
            return NotFound();
        }

        _contexto.Videojuegos.Remove(juego);
        await _contexto.SaveChangesAsync();

        return NoContent();
    }
}
