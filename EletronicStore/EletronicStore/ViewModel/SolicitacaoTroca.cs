using EletronicStore.Models;

namespace EletronicStore.ViewModel {
    public class SolicitacaoTroca : EntidadeDominio{

        public SolicitacaoTroca() {
            Produtos = new List<ProdutoTroca>();
        }
        public Usuario Usuario { get; set; }
        public int PedidoId { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public List<ProdutoTroca> Produtos{ get; set; }
    }
}
