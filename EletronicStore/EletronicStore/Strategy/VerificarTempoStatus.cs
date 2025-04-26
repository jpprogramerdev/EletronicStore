using EletronicStore.Facade;
using EletronicStore.Facade.Interface;
using EletronicStore.Models;
using EletronicStore.Strategy.Interface;

namespace EletronicStore.Strategy {
    public class VerificarTempoStatus : IStrategy {
        public void Executar(EntidadeDominio entidade) {
            IFacadeGeneric facadePedido = new FacadePedido();
            
            Pedido Pedido = (Pedido)entidade;

            if((DateTime.Now - Pedido.UltimaAtualizacao).Days > 7) {
                if(Pedido.Status == "EM PROCESSAMENTO") {
                    Pedido.Status = "REPROVADO";
                }else if(Pedido.Status == "TROCA SOLICITADA") {
                    Pedido.Status = "TROCA RECUSADA";
                }else if(Pedido.Status == "DEVOLUÇÃO SOLICITADA") {
                    Pedido.Status = "DEVOLUÇÃO RECUSADA";
                } else if(Pedido.Status == "ENTREGUE") {
                    Pedido.Status = "PEDIDO CONCLUIDO";
                }
                facadePedido.Atualizar(Pedido);
            }
        }
    }
}
