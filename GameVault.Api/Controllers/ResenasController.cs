using GameVault.Api.Data;
using GameVault.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Api.Controllers;

public class ResenasController : ODataController
{
    private readonly GameVaultContext _contexto;

    public ResenasController(GameVaultContext contexto)
    {
        _contexto = contexto;
    }

    // GET /odata/Resenas?$filter=VideojuegoId eq 3
    [EnableQuery(MaxExpansionDepth = 2, PageSize = 200)]
    public IQueryable<Resena> Get() => _contexto.Resenas.AsNoTracking();

    // GET /odata/Resenas(5)?$expand=Videojuego
    [EnableQuery(MaxExpansionDepth = 2)]
    public SingleResult<Resena> Get([FromRoute] int key) =>
        SingleResult.Create(_contexto.Resenas.AsNoTracking().Where(resena => resena.Id == key));

    // POST /odata/Resenas
    public async Task<IActionResult> Post([FromBody] Resena resena)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existeElJuego = await _contexto.Videojuegos.AnyAsync(juego => juego.Id == resena.VideojuegoId);

        if (!existeElJuego)
        {
            ModelState.AddModelError(nameof(Resena.VideojuegoId), "El videojuego de la resena no existe.");
            return BadRequest(ModelState);
        }

        resena.Id = 0;

        if (resena.Fecha == default)
        {
            resena.Fecha = DateTimeOffset.UtcNow;
        }

        _contexto.Resenas.Add(resena);
        await _contexto.SaveChangesAsync();

        return Created(resena);
    }

    // DELETE /odata/Resenas(5)
    public async Task<IActionResult> Delete([FromRoute] int key)
    {
        var resena = await _contexto.Resenas.FirstOrDefaultAsync(r => r.Id == key);

        if (resena is null)
        {
            return NotFound();
        }

        _contexto.Resenas.Remove(resena);
        await _contexto.SaveChangesAsync();

        return NoContent();
    }
}
