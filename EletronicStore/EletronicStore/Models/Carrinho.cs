using EletronicStore.ViewModel;

namespace EletronicStore.Models {
    public class Carrinho : EntidadeDominio{

        public Carrinho() {
            ItensCarrinho = new List<ItemPedido>();
            Cupons = new List<Cupom>();
        }
        public DateTime DataCriado { get; set; }
        public Endereco Endereco { get; set; }
        public Cartao Cartao { get; set; }
        public bool AdicionarEndereco { get; set; }
        public bool AdicionarCartao { get; set; }
        public Usuario Usuario { get; set; }
        public List<Cupom> Cupons { get; set; }
        public List<ItemPedido> ItensCarrinho { get; set; }
    }
}
