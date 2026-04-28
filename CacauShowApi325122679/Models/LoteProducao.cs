

namespace CacauShowApi325122679.Models
{
    public class LoteProducao
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string status { get; set; } //  [Em Produção, Qualidade Aprovada, Distribuído,descartado]
        public DateTime DataFabricacao { get; set; }
        public string CodigoLote { get; set; } // Ex: "BATCH-2026-X"
    }
}