using EletronicStore.ViewModel;

namespace EletronicStore.Models {
    public class Pedido :EntidadeDominio {
        public Pedido() {
            Produtos = new List<ItemPedido>();
            Cupons = new List<Cupom>();
        }

        public Usuario  Usuario{ get; set; }
        public string Status { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
        //mudar para List de cartoes
        public Cartao Cartao { get; set; }
        public Endereco Endereco { get; set; }
        public List<Cupom> Cupons { get; set; }
        public List<ItemPedido> Produtos { get; set; }
    }
}
