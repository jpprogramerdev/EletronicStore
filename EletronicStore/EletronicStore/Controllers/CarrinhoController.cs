using EletronicStore.Facade;
using EletronicStore.Facade.Interface;
using EletronicStore.Models;
using EletronicStore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using EletronicStore.Exceptions;

namespace EletronicStore.Controllers {
    public class CarrinhoController : Controller {
        public IActionResult ExibirCarrinho(int id) {
            IFacadeGeneric facadeCarrinho = new FacadeCarrinho();
            IFacadeGeneric facadeUsuario = new FacadeCliente();

            Carrinho Carrinho = facadeCarrinho.Selecionar().Cast<Carrinho>().ToList().Where(c => c.Usuario.Id == 1).FirstOrDefault();

            if(Carrinho == null) {
                Carrinho = new();
            }

            Carrinho.Usuario = facadeUsuario.Selecionar().Cast<Usuario>().ToList().Where(u => u.Id == 1).FirstOrDefault();

            return View(Carrinho);
        }

        public IActionResult AdicionarItemNoCarrinho(ItemPedido ItemCarrinho) {
            IFacadeGeneric facadeCarrinho = new FacadeCarrinho();
            IFacadeGeneric facadeEstoque = new FacadeEstoque();
            IFacadeGeneric facadeProduto = new FacadeProduto();

            ItemCarrinho.Produto = facadeProduto.Selecionar().Cast<Produto>().Where(p => p.Id == ItemCarrinho.Produto.Id).FirstOrDefault();

            ItemCarrinho.Produto.QuantidadeEstoque = ItemCarrinho.Produto.QuantidadeEstoque - ItemCarrinho.Quantidade;

            try {
                facadeCarrinho.Inserir(ItemCarrinho);
                facadeEstoque.Atualizar(ItemCarrinho.Produto);
            }catch(QuantidadeEstoquueException Qex) {
                TempData["FalhaAddCarrinho"] = Qex.Message;
                return RedirectToAction("ExibirProduto", "Produto", new { id = ItemCarrinho.Produto.Id });
            } catch(Exception ex) {
                TempData["FalhaAddCarrinho"] = "Falha ao adicionar o item ao carrinho";
                return RedirectToAction("ExibirProduto", "Produto", new { id = ItemCarrinho.Produto.Id });
            }

            return RedirectToAction("ExibirTodosProdutos", "Produto");
        }

        public IActionResult RemoverItemCarrinho(int itemid, int usuarioId) {
            IFacadeGeneric facadeCarrinho = new FacadeCarrinho();

            Carrinho Carrinho = new Carrinho();
            Carrinho.ItensCarrinho.Add(new ItemPedido { Id = itemid, ExclusaoCarrinho = true });
            Carrinho.Usuario = new Usuario { Id = usuarioId };

            facadeCarrinho.Delete(Carrinho);

            return RedirectToAction("ExibirCarrinho", "Carrinho", new { id = usuarioId});
        }

        [HttpGet]
        public IActionResult VerificarCupom(string codigo) {
            IFacadeGeneric facadeCupom = new FacadeCupom();

            var cupom = facadeCupom.Selecionar().Cast<Cupom>().FirstOrDefault(c => c.Nome.ToUpper() == codigo.ToUpper());

            if(cupom == null) {
                return Json(new { valido = false, mensagem = "Cupom inválido" });
            }

            return Json(new { valido = true, desconto = cupom.Desconto, nome = cupom.Nome, id = cupom.Id,mensagem = "Cupom aplicado com sucesso!" });
        }
    }
}
