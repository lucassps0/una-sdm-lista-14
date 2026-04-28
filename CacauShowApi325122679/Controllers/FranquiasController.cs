using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CacauShowApi325122679.Data;
using CacauShowApi325122679.Models;


namespace CacauShowApi325122679.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FranquiasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FranquiasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Franquia>>> GetFranquias()
        {
            return await _context.Franquias.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Franquia>> GetFranquia(int id)
        {
            var franquia = await _context.Franquias.FindAsync(id);

            if (franquia == null)
            {
                return NotFound();
            }

            return franquia;
        }

        [HttpPost]
        public async Task<ActionResult<Franquia>> CreateFranquia(Franquia franquia)
        {
            _context.Franquias.Add(franquia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFranquia), new { id = franquia.Id }, franquia);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutFranquia(int id, Franquia franquia)
        {
            if (id != franquia.Id)
            {
                return BadRequest();
            }

            _context.Entry(franquia).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFranquia(int id)
        {
            var franquia = await _context.Franquias.FindAsync(id);
            if (franquia == null)
            {
                return NotFound();
            }

            _context.Franquias.Remove(franquia);
            await _context.SaveChangesAsync();

            return NoContent();
        }



    }
}