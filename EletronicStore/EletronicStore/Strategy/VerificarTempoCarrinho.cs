using EletronicStore.Facade;
using EletronicStore.Facade.Interface;
using EletronicStore.Models;
using EletronicStore.Strategy.Interface;
using EletronicStore.ViewModel;

namespace EletronicStore.Strategy {
    public class VerificarTempoCarrinho : IStrategy {
        public void Executar(EntidadeDominio entidade) {
            Carrinho Carrinho = (Carrinho)entidade;

            if((DateTime.Now - Carrinho.DataCriado).Days > 3) {
                IFacadeGeneric facadeCarrinho = new FacadeCarrinho();
                IFacadeGeneric facadeEstoque = new FacadeEstoque();

                foreach(ItemPedido Item in Carrinho.ItensCarrinho) {
                    Item.ExclusaoCarrinho = true;
                    Item.Produto.QuantidadeEstoque += Item.Quantidade;
                    facadeCarrinho.Delete(Carrinho);
                    facadeEstoque.Atualizar(Item.Produto);
                }
            }
        }
    }
}
