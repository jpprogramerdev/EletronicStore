using EletronicStore.Models;

namespace EletronicStore.ViewModel {
    public class ItemPedido : EntidadeDominio {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public bool ExclusaoCarrinho { get; set; } //variavel usada para verificar se caso o item esteja no carrinho, ele acabe ficando um pedido
    }
}
