using EletronicStore.Models;

namespace EletronicStore.ViewModel {
    public class ProdutoTroca {

        public Produto Produto { get; set; }
        public string Motivo { get; set; }
        public int QuantidadePedido { get; set; }
        public int QuantidadeTroca { get; set; }
        public bool SoliciarTrocar { get; set; }
    }
}
