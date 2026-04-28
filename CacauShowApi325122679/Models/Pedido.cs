
namespace CacauShowApi325122679.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int UnidadeId { get; set; }
        public int ProdutoId { get; set; }
        public decimal ValorTotal { get; set; }
        public int Quantidade { get; set; }
    }
}
