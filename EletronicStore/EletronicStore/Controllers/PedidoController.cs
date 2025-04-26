using EletronicStore.Exceptions;
using EletronicStore.Facade;
using EletronicStore.Facade.Interface;
using EletronicStore.Models;
using EletronicStore.Strategy;
using EletronicStore.Strategy.Interface;
using EletronicStore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EletronicStore.Controllers {
    public class PedidoController : Controller {
        public IActionResult ExibirTodosPedidos(int Id) {
            IFacadeGeneric facadePedido = new FacadePedido();
            IStrategy VerificarTempoStatus = new VerificarTempoStatus();

            

            Id = 1;

            List<Pedido> ListPedido = facadePedido.Selecionar().Cast<Pedido>().Where(p => p.Usuario.Id == Id).ToList();
            List<string> ordemStatus = new List<string>{
                    "EM PROCESSAMENTO",
                    "REPROVADO",
                    "APROVADO",  
                    "EM TRANSPORTE",
                    "ENTREGUE",
                    "TROCA SOLICITADA",
                    "TROCA ACEITA",
                    "TROCA CONCLUÍDA",
                    "TROCA RECUSADA",
                    "DEVOLUÇÃO SOLICITADA",
                    "DEVOLUÇÃO RECUSADA",
                    "DEVOLUÇÃO CONCLUÍDA",
                    "CANCELADO"
                };

            ListPedido = ListPedido.OrderBy( p => ordemStatus.IndexOf(p.Status)).ToList();

            foreach (Pedido Pedido in ListPedido) {
                VerificarTempoStatus.Executar(Pedido);
            }

            return View(ListPedido);
        }

        public IActionResult ExibirSolicitacao(int id) {
            IFacadeGeneric facadeSolicitacaoTroca = new FacadeSolicitacaoTroca();
            SolicitacaoTroca solicitacaoTroca = facadeSolicitacaoTroca.Selecionar().Cast<SolicitacaoTroca>().Where(s => s.PedidoId == id).FirstOrDefault();

            return View(solicitacaoTroca);
        }

        public IActionResult ExibirPedido(int Id) {
            IFacadeGeneric facadePedido = new FacadePedido();

            Pedido Pedido = facadePedido.Selecionar().Cast<Pedido>().Where(p => p.Id == Id).FirstOrDefault();

            return View(Pedido);
        }

        public IActionResult SolicitarTroca(int id) {
            IFacadeGeneric facadePedido = new FacadePedido();

            Pedido Pedido = facadePedido.Selecionar().Cast<Pedido>().Where(p => p.Id == id).FirstOrDefault();

            SolicitacaoTroca SolicitacaoTroca = new();

            foreach(ItemPedido item in Pedido.Produtos) {
                ProdutoTroca ProdutoTroca = new();

                ProdutoTroca.Produto = item.Produto;
                ProdutoTroca.QuantidadePedido = item.Quantidade;

                SolicitacaoTroca.Produtos.Add(ProdutoTroca);
            }

            SolicitacaoTroca.PedidoId = Pedido.Id;

            return View(SolicitacaoTroca);
        }

        public IActionResult AlterarStatusPedido(int pedidoId, string status) {
            IFacadeGeneric facadePedido = new FacadePedido();
            IFacadeGeneric facadeSolicitacaoTroca = new FacadeSolicitacaoTroca();

            Pedido Pedido = facadePedido.Selecionar().Cast<Pedido>().Where(p => p.Id == pedidoId).FirstOrDefault();

            if (Pedido != null) {
                Pedido.Status = status;
                try {
                    if (status == "TROCA RECUSADA") {
                        facadeSolicitacaoTroca.Delete(Pedido);
                    } else if (status == "TROCA CONCLUÍDA" || status == "DEVOLUÇÃO CONCLUÍDA") {
                        facadePedido.Atualizar(Pedido);
                        Usuario Usuario = Pedido.Usuario;
                        return RedirectToAction("CriarCupomTroca", "Cupom", Usuario);
                    }

                    facadePedido.Atualizar(Pedido);
                }catch (Exception ex) {
                    TempData["FalhaAoGerarCupom"] = "Falha ao gerar cupom de troca";
                }
            } else {
                TempData["FalhaAtualizarStatus"] = "Falha ao atualizar o status do produto";
            }

            return RedirectToAction("ExibirTodosPedidos", "Pedido", new { Id = 1 });
        }

        [HttpPost]
        public IActionResult AdicionarPedido(Carrinho Carrinho) {
            IFacadeGeneric facadePedido = new FacadePedido();
            IFacadeGeneric facadeItemPedido = new FacadeItemPedido();
            IFacadeGeneric facadeCarrinho = new FacadeCarrinho();
            IFacadeGeneric facadeCartaoPedido = new FacadeCartaoPedido();
            IStrategy VerificarCupom = new VerificarCupom();

            Pedido Pedido = new Pedido { Usuario = Carrinho.Usuario };
            Pedido.Produtos = Carrinho.ItensCarrinho.Where(i => i.ExclusaoCarrinho).ToList();
            Pedido.Endereco = Carrinho.Endereco;
            Pedido.Cartao = Carrinho.Cartao;
            Pedido.Cupons = Carrinho.Cupons;

            try {
                VerificarCupom.Executar(Pedido);

                if (Pedido.Produtos.Count > 0) {
                    if (Pedido.Endereco.Id == 0) {

                        IFacadeGeneric facadeEnderecoCliente = new FacadeEnderecoCliente();
                        IFacadeGeneric facadePais = new FacadePais();
                        IFacadeGeneric facadeEstado = new FacadeEstado();
                        IFacadeGeneric facadeMunicipio = new FacadeMunicipio();
                        IFacadeGeneric facadeEndereco = new FacadeEndereco();

                        facadePais.Inserir(Pedido.Endereco.Municipio.Estado.Pais);
                        facadeEstado.Inserir(Pedido.Endereco.Municipio.Estado);
                        facadeMunicipio.Inserir(Pedido.Endereco.Municipio);
                        facadeEndereco.Inserir(Pedido.Endereco);
                        if (Carrinho.AdicionarEndereco) {
                            facadeEnderecoCliente.Inserir(new FormCadCliente { Cliente = Pedido.Usuario, Endereco = Pedido.Endereco });
                        }
                    }

                    if (Pedido.Cartao.Id == 0) {
                        IFacadeGeneric facadeCartao = new FacadeCartao();
                        IFacadeGeneric facadeCartaoCliente = new FacadeCartaoCliente();

                        facadeCartao.Inserir(Pedido.Cartao);
                        if (Carrinho.AdicionarCartao) {
                            facadeCartaoCliente.Inserir(new FormCadCliente { Cliente = Pedido.Usuario, Cartao = Pedido.Cartao });
                        }
                    }

                    facadePedido.Inserir(Pedido);
                    facadeCartaoPedido.Inserir(Pedido);

                    facadeItemPedido.Inserir(Pedido);
                    facadeCarrinho.Delete(Carrinho);


                    TempData["SucessoFinalizarPedido"] = "Pedido finalizado com sucesso. Aguarde autorização de um administrador";
                    return RedirectToAction("ExibirTodosPedidos", "Pedido", new { Id = Pedido.Usuario.Id });
                }

                TempData["FalhaFinalizarPedido"] = "Selecione pelo menos um item para finalizar a compra";
                return RedirectToAction("ExibirCarrinho", "Carrinho", new { id = Pedido.Usuario.Id });
            }catch(CupomTrocaInvalidoException ex) {
                TempData["FalhaFinalizarPedido"] = "Usuario sem permissão para utilizar esse cupom";
                return RedirectToAction("ExibirCarrinho", "Carrinho", new { id = Pedido.Usuario.Id });
            } catch (Exception ex) {
                TempData["FalhaFinalizarPedido"] = "Falha ao finalizar a compra";

                return RedirectToAction("ExibirCarrinho", "Carrinho", new { id = Pedido.Usuario.Id });
            }


        }

        [HttpPost]
        public IActionResult SalvarSolicitacaoTroca(SolicitacaoTroca SolicitacaoTroca) {

            try {
                if (SolicitacaoTroca.Produtos.Where(p => p.SoliciarTrocar).Count() > 0 && !SolicitacaoTroca.Produtos.Where(p => p.SoliciarTrocar).Any(p => string.IsNullOrWhiteSpace(p.Motivo))) {

                    IFacadeGeneric facadeSolicitacaoTroca = new FacadeSolicitacaoTroca();
                    facadeSolicitacaoTroca.Inserir(SolicitacaoTroca);

                    return RedirectToAction("AlterarStatusPedido", "Pedido", new { pedidoId = SolicitacaoTroca.PedidoId, status = "TROCA SOLICITADA" });
                }
                TempData["NenhumProdutoSelecionado"] = "Nenhum produto foi selecionado e/ou nenhuma justificativa foi informada para troca";
                return RedirectToAction("SolicitarTroca", "Pedido", new { id = SolicitacaoTroca.PedidoId});
            } catch (Exception ex){
                IFacadeGeneric facadePedido = new FacadePedido();
                Pedido Pedido = facadePedido.Selecionar().Cast<Pedido>().Where(p => p.Id == SolicitacaoTroca.PedidoId).FirstOrDefault();

                TempData["FalhaSolicitacaoTroca"] = "Falha ao solicitar a troca do pedido";

                return RedirectToAction("ExibirTodosPedidos", "Pedido", new { Id = Pedido.Usuario.Id});
            }
        }
    }
}
