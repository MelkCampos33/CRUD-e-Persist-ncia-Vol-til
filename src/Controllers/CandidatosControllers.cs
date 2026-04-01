using Microsoft.AspNetCore.Mvc;
using EleicaoApi.Models;
using EleicaoApi.Data;

[Route("api/[controller]")]
[ApiController]
public class CandidatosController : ControllerBase
{
    private readonly AppDbContext _context;

    public CandidatosController(AppDbContext context) => _context = context;

    [HttpGet]
    public IActionResult Get() => Ok(_context.Candidatos.ToList());

    [HttpGet("partido/{nomeDoPartido}")]
    public IActionResult GetByPartido(string nomeDoPartido)
    {
        var filtro = _context.Candidatos
            .Where(c => c.Partido.ToLower() == nomeDoPartido.ToLower()).ToList();
        return Ok(filtro);
    }

    [HttpPost]
    public IActionResult Post(Candidato novo)
    {
        if (_context.Candidatos.Any(c => c.Numero == novo.Numero))
            return BadRequest("Este número de candidato já está em uso.");

        _context.Candidatos.Add(novo);
        _context.SaveChanges();
        return Ok(novo);
    }



    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Candidato candidatoAtualizado)
    {
        var candidatoExistente = _context.Candidatos.Find(id);

        if (candidatoExistente == null)
        {
            return NotFound($"Candidato com ID {id} não encontrado.");
        }

        // Atualiza os campos (Parte 2, item 3 do seu exercício)
        candidatoExistente.Nome = candidatoAtualizado.Nome;
        candidatoExistente.Partido = candidatoAtualizado.Partido;
        candidatoExistente.Numero = candidatoAtualizado.Numero;
        candidatoExistente.ViceNome = candidatoAtualizado.ViceNome; // Campo novo!

        _context.SaveChanges();

        // Retornamos 204 No Content para indicar que deu certo, mas não há corpo na resposta
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var candidato = _context.Candidatos.Find(id);

        if (candidato == null)
        {
            return NotFound();
        }

        _context.Candidatos.Remove(candidato);
        _context.SaveChanges();

        return NoContent();
    }

}