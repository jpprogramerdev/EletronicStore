using EletronicStore.Models;

namespace EletronicStore.ViewModel {
    public class FormInativacaoProduto : EntidadeDominio{
        public Produto Produto { get; set; }
        public string Motivo { get; set; }

    }
}
