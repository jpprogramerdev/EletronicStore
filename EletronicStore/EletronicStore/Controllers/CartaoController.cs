using EletronicStore.Facade.Interface;
using EletronicStore.Facade;
using EletronicStore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using EletronicStore.Models;

namespace EletronicStore.Controllers {
    public class CartaoController : Controller {
        public IActionResult CadastrarCartao(int id) {
            IFacadeGeneric facadeCliente = new FacadeCliente();
            FormCadCliente FormCadCliente = new();
            FormCadCliente.Cliente = facadeCliente.Selecionar().Cast<Usuario>().ToList().Where(u => u.Id == id).FirstOrDefault();
            return View(FormCadCliente);
        }

        public IActionResult EditarCartao(int idCartao, int idCliente) {
            IFacadeGeneric facadeCartao = new FacadeCartao();
            IFacadeGeneric facadeCliente = new FacadeCliente();

            FormCadCliente FormCadCliente = new FormCadCliente();

            FormCadCliente.Cartao = facadeCartao.Selecionar().Cast<Cartao>().ToList().Where(c => c.Id == idCartao).FirstOrDefault();
            FormCadCliente.Cliente = facadeCliente.Selecionar().Cast<Usuario>().ToList().Where(c => c.Id == idCliente).FirstOrDefault();

            return View(FormCadCliente);
        }

        public IActionResult ExcluirCartao(int idCliente, int idCartao) {
            IFacadeGeneric facadeCliente = new FacadeCliente();

            Usuario Cliente = facadeCliente.Selecionar().Cast<Usuario>().ToList().Where(u => u.Id == idCliente).FirstOrDefault();
            Cartao Cartao = Cliente.Cartoes.Where(c => c.Id == idCartao).FirstOrDefault();

            Cliente.Cartoes.Clear();
            Cliente.Cartoes.Add(Cartao);

            return View(Cliente);
        }

        [HttpPost]
        public IActionResult SalvarNovoCartao(FormCadCliente formCadCliente) {
            IFacadeGeneric facadeCartao = new FacadeCartao();
            IFacadeGeneric facadeCartaoCliente = new FacadeCartaoCliente();

            try {
                facadeCartao.Inserir(formCadCliente.Cartao);
                facadeCartaoCliente.Inserir(formCadCliente);
                TempData["SucessoCadastroNovoCartao"] = "Sucesso ao cadastrar o Cartao";

            } catch (Exception ex) {
                TempData["FalhaCadastroNovoCartao"] = "Falha ao cadastrar o cartão";
            }
            return RedirectToAction("ExibirDadosCliente", "Cliente", new { id = formCadCliente.Cliente.Id });
        }

        [HttpPost]
        public IActionResult AtualizarCartao(FormCadCliente formCadCliente) {
            IFacadeGeneric facadeCartao = new FacadeCartao();
            IFacadeGeneric facadeCartaoCliente = new FacadeCartaoCliente();

            try {
                facadeCartaoCliente.Delete(formCadCliente);
                facadeCartao.Inserir(formCadCliente.Cartao);
                facadeCartaoCliente.Inserir(formCadCliente);
                TempData["SucessoCadastroNovoCartao"] = "Sucesso ao atualizar o cartão";
            } catch (Exception ex){
                TempData["FalhaCadastroNovoCartao"] = "Falha ao atualizar o cartão";
            }
            return RedirectToAction("ExibirDadosCliente", "Cliente", new { id = formCadCliente.Cliente.Id });
        }

        public IActionResult DeletarCartao(int idCliente, int idCartao) {
            IFacadeGeneric facadeCartao = new FacadeCartao();
            IFacadeGeneric facadeCartaoCliente = new FacadeCartaoCliente();

            if (facadeCartaoCliente.Delete(new FormCadCliente { Cartao = new Cartao { Id = idCartao }, Cliente = new Usuario { Id = idCliente } })) {
                TempData["SucessoExclusaoCartao"] = "Cartão excluido com sucesso";
            } else {
                TempData["FalhaExclusaoCartao"] = "Falha ao excluir o cartão";
            }
            return RedirectToAction("ExibirDadosCliente", "Cliente", new { id = idCliente });
        }
    }
}
