using EletronicStore.Exceptions;
using EletronicStore.Facade;
using EletronicStore.Facade.Interface;
using EletronicStore.Models;
using EletronicStore.Strategy.Interface;

namespace EletronicStore.Strategy {
    public class VerificarCupom : IStrategy {
        public void Executar(EntidadeDominio entidade) {
            IFacadeGeneric facadeCupom = new FacadeCupom();

            Pedido Pedido = (Pedido)entidade;

            List<Cupom> ListCupom = facadeCupom.Selecionar().Cast<Cupom>().ToList();

            foreach (Cupom Cupom in ListCupom) {
                if (Cupom.Nome == "CUPOMTROCA" && Cupom.Usuario.Id != Pedido.Usuario.Id) {
                    throw new CupomTrocaInvalidoException();
                } else if (Cupom.Nome == "CUPOMTROCA" && Cupom.Usuario.Id == Pedido.Usuario.Id) {
                    facadeCupom.Delete(Cupom);
                }
            }
        }
    }
}
