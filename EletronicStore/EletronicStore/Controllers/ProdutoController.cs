using EletronicStore.Facade;
using EletronicStore.Facade.Interface;
using EletronicStore.Models;
using EletronicStore.Strategy;
using EletronicStore.Strategy.Interface;
using EletronicStore.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EletronicStore.Controllers {
    public class ProdutoController : Controller {
        public IActionResult CadastrarNovoProduto() {
            return View();
        }

        [HttpGet]
        public IActionResult ExibirTodosProdutos(string? filterStatusProduto, string? filterNomeProduto, string? filterTipoProduto, string? filterMarcaProduto) {
            IFacadeGeneric facadeProduto = new FacadeProduto();
            IStrategy VerificarTempoCarrinho = new VerificarTempoCarrinho();
            IFacadeGeneric facadeCarrinho = new FacadeCarrinho();
            IFacadeGeneric facadeUsuario = new FacadeCliente();

            Carrinho Carrinho = facadeCarrinho.Selecionar().Cast<Carrinho>().ToList().Where(c => c.Usuario.Id == 1).FirstOrDefault();

            if (Carrinho == null) {
                Carrinho = new();
            }

            Carrinho.Usuario = facadeUsuario.Selecionar().Cast<Usuario>().ToList().Where(u => u.Id == 1).FirstOrDefault();

            List<Produto> ListProduto = facadeProduto.Selecionar().Cast<Produto>().ToList();

            if (!string.IsNullOrEmpty(filterStatusProduto)) {
                bool statusProduto = filterStatusProduto == "1" ? true : false;

                ListProduto = ListProduto.Where(p => p.Status == statusProduto).ToList();
            }
            if (string.IsNullOrEmpty(filterStatusProduto)) {
                ListProduto = ListProduto.Where(p => p.Status == true).ToList();
            }
            if (!string.IsNullOrEmpty(filterNomeProduto)) {
                ListProduto = ListProduto.Where(p => p.Nome.Contains(filterNomeProduto, StringComparison.OrdinalIgnoreCase)).ToList();
            } 
            if (!string.IsNullOrEmpty(filterTipoProduto)) {
                int IdTipoProduto = int.Parse(filterTipoProduto);

                ListProduto = ListProduto.Where(p => p.TipoProduto.Id == IdTipoProduto).ToList();
            }
            if (!string.IsNullOrEmpty(filterMarcaProduto)) {
                ListProduto = ListProduto.Where(p => p.Marca.Nome.Contains(filterMarcaProduto, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (Carrinho.ItensCarrinho.Count != 0) {
                VerificarTempoCarrinho.Executar(Carrinho);
            }

            return View(ListProduto);
        }

        public IActionResult ExibirProduto(int Id) {
            IFacadeGeneric facadeProduto = new FacadeProduto();

            ItemPedido ItemCarrinho = new ItemPedido { Produto = facadeProduto.Selecionar().Cast<Produto>().Where(p => p.Id == Id).FirstOrDefault()};

            return View(ItemCarrinho);
        }

        public IActionResult EditarProduto(int Id) {
            IFacadeGeneric facadeProduto = new FacadeProduto();

            Produto Produto = facadeProduto.Selecionar().Cast<Produto>().Where(p => p.Id == Id).FirstOrDefault();

            return View(Produto);
        }

        public IActionResult InativarProduto(int Id){
            IFacadeGeneric facadeProduto = new FacadeProduto();
            IFacadeGeneric facadeProdutoDesativado = new FacadeProdutoInativado();

            Produto Produto = facadeProduto.Selecionar().Cast<Produto>().Where(p => p.Id == Id).FirstOrDefault();

            if (!Produto.Status) {
                facadeProdutoDesativado.Delete(Produto);

                TempData["SucessoAtivarProduto"] = "Sucesso ao ativar produto";

                return RedirectToAction("ExibirTodosProdutos", "Produto");
            }

            FormInativacaoProduto FormInativacaoProduto = new();
            FormInativacaoProduto.Produto = facadeProduto.Selecionar().Cast<Produto>().Where(p => p.Id == Id).FirstOrDefault();

            return View(FormInativacaoProduto);
        }

        [HttpPost]
        public IActionResult SalvarNovoProduto(Produto Produto) {
            IFacadeGeneric facadeMarca = new FacadeMarca();
            IFacadeGeneric facadeFornecedor = new FacadeFornecedor();
            IFacadeGeneric facadeProduto = new FacadeProduto();
            IFacadeGeneric facadeEstoque = new FacadeEstoque();
            IStrategy GerarCaminhoImagem = new GerarCaminhoImagem();

            try {
                facadeMarca.Inserir(Produto.Marca);
                facadeFornecedor.Inserir(Produto.Fornecedor);

                GerarCaminhoImagem.Executar(Produto);

                facadeProduto.Inserir(Produto);
                facadeEstoque.Inserir(Produto);

                TempData["SucessoCadastroProduto"] = "Sucesso ao cadastrar o produto";
            } catch(Exception ex) {
                TempData["FalhaCadastroProduto"] = "Falha ao cadastrar o produto: " + ex.Message;
            }

            return View("CadastrarNovoProduto");
        }

        [HttpPost]
        public IActionResult AtualizarProduto(Produto Produto) {
            IFacadeGeneric facadeMarca = new FacadeMarca();
            IFacadeGeneric facadeFornecedor = new FacadeFornecedor();
            IFacadeGeneric facadeProduto = new FacadeProduto();
            IFacadeGeneric facadeEstoque = new FacadeEstoque();
            IStrategy GerarCaminhoImagem = new GerarCaminhoImagem();

            Produto.Status = true;

            if (Produto.QuantidadeEstoque == 0) {
                Produto.Status = false;

                FormInativacaoProduto FormInativacaoProduto = new();
                FormInativacaoProduto.Produto = Produto;
                FormInativacaoProduto.Motivo = "Fora de mercado";

                IFacadeGeneric facadeProdutoInativado = new FacadeProdutoInativado();
                facadeProdutoInativado.Inserir(FormInativacaoProduto);
            }

            try {
                facadeMarca.Inserir(Produto.Marca);
                facadeFornecedor.Inserir(Produto.Fornecedor);

                GerarCaminhoImagem.Executar(Produto);

                facadeProduto.Atualizar(Produto);
                facadeEstoque.Atualizar(Produto);

                TempData["SucessoAtualizarProduto"] = "Sucesso ao atualizar produto";
            } catch (NullReferenceException NullEx) {

                facadeProduto.Atualizar(Produto);
                facadeEstoque.Atualizar(Produto);

                TempData["SucessoAtualizarProduto"] = "Sucesso ao atualizar produto";
            } catch (Exception ex) {
                TempData["FalhaAtualizarProrduto"] = "Falha ao atualizar produto";
            }

            return RedirectToAction("EditarProduto","Produto", new { id = Produto.Id});
        }

        [HttpPost]
        public IActionResult AlterarStatusProduto(FormInativacaoProduto FormInativacaoProduto) {
            IFacadeGeneric facadeProdutoInativado = new FacadeProdutoInativado();

            try {
                facadeProdutoInativado.Inserir(FormInativacaoProduto);

                TempData["SucessoInativarProduto"] = "Sucesso ao inativar produto";
            } catch (Exception ex){
                TempData["FalhaInativarProduto"] = "Falha ao inativar produto";
            }

            return RedirectToAction("ExibirTodosProdutos", "Produto");
        }
    }
}
