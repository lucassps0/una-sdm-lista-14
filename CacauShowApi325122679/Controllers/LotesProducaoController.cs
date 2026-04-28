using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CacauShowApi325122679.Data;
using CacauShowApi325122679.Models;

namespace CacauShowApi325122679.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesProducaoController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoteProducao>>> GetLotesProducao()
        {
            return await _context.LotesProducao.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LoteProducao>> GetLoteProducao(int id)
        {
            var loteProducao = await _context.LotesProducao.FindAsync(id);

            if (loteProducao == null)
            {
                return NotFound();
            }

            return loteProducao;
        }

        [HttpPost]
        public async Task<ActionResult<LoteProducao>> CreateLoteProducao(LoteProducao loteProducao)
        {
            var produto = await _context.Produtos.FindAsync(loteProducao.ProdutoId);
            if (produto == null)
            {
                return BadRequest("Produto não encontrado");
            }

            if (loteProducao.DataFabricacao > DateTime.Now)
            {
                return BadRequest("Data de fabricação não pode ser futura");
            }

            _context.LotesProducao.Add(loteProducao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLoteProducao), new { id = loteProducao.Id }, loteProducao);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLoteProducao(int id, LoteProducao loteProducao)
        {
            if (id != loteProducao.Id)
            {
                return BadRequest();
            }

            var existingLote = await _context.LotesProducao.FindAsync(id);
            if (existingLote == null)
            {
                return NotFound();
            }

            if (existingLote.status == "descartado" && loteProducao.status != "descartado")
            {
                return BadRequest("Lote descartado não pode ser atualizado para outro status");
            }

            _context.Entry(loteProducao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoteProducaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        private bool LoteProducaoExists(int id)
        {
            return _context.LotesProducao.Any(e => e.Id == id);
        }
    }
}