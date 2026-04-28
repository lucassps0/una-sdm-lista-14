using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CacauShowApi325122679.Data;
using CacauShowApi325122679.Models;


namespace CacauShowApi325122679.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PedidosController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _context.Pedidos.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }
        [HttpPost]
        public async Task<ActionResult<Pedido>> CreatePedido(Pedido pedido)
        {
            var produto = await _context.Produtos.FindAsync(pedido.ProdutoId);
            if (produto == null)
            {
                return BadRequest("Produto não encontrado");
            }

            var franquia = await _context.Franquias.FindAsync(pedido.UnidadeId);
            if (franquia == null)
            {
                return BadRequest("Franquia não encontrada");
            }

            var totalPedidos = await _context.Pedidos
                .Where(p => p.UnidadeId == pedido.UnidadeId)
                .SumAsync(p => p.Quantidade);

            if (totalPedidos + pedido.Quantidade > franquia.CapacidadeEstoque)
            {
                return BadRequest("Capacidade de estoque da franquia excedida");
            }

            if (produto.tipo == "sazonal")
            {
                pedido.ValorTotal += 15;
            }

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, pedido);
        }
    }
}