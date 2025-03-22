using EletronicStore.Facade;
using EletronicStore.Facade.Interface;
using EletronicStore.Models;
using EletronicStore.Strategy.Interface;
using EletronicStore.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EletronicStore.Controllers {
    public class EnderecoController : Controller {
        public IActionResult CadastrarEndereco(int id) {
            IFacadeGeneric facadeCliente = new FacadeCliente();
            FormCadCliente FormCadCliente = new();
            FormCadCliente.Cliente = facadeCliente.Selecionar().Cast<Usuario>().ToList().Where(u => u.Id == id).FirstOrDefault();
            return View(FormCadCliente);
        }

        public IActionResult EditarEndereco(int idCliente, int idEndereco) {
            IFacadeGeneric facadeEndereco = new FacadeEndereco();
            IFacadeGeneric facadeCliente = new FacadeCliente();

            FormCadCliente FormCadCliente = new FormCadCliente();
            FormCadCliente.Cliente = facadeCliente.Selecionar().Cast<Usuario>().ToList().Where(c => c.Id == idCliente).FirstOrDefault();
            FormCadCliente.Endereco = FormCadCliente.Cliente.Enderecos.Where(e => e.Id == idEndereco).FirstOrDefault();
            return View(FormCadCliente);
        }

        public IActionResult ExcluirEndereco(int idCliente, int idEndereco) {
            IFacadeGeneric facadeCliente = new FacadeCliente();

            Usuario Cliente = facadeCliente.Selecionar().Cast<Usuario>().ToList().Where(u => u.Id == idCliente).FirstOrDefault();
            Endereco Endereco = Cliente.Enderecos.Where(e => e.Id == idEndereco).FirstOrDefault();

            Cliente.Enderecos.Clear();
            Cliente.Enderecos.Add(Endereco);

            return View(Cliente);
        }

        [HttpPost]
        public IActionResult SalvarNovoEndereco(FormCadCliente formCadCliente) {
            IFacadeGeneric facadeEndereco = new FacadeEndereco();
            IFacadeGeneric facadeMunicipio = new FacadeMunicipio();
            IFacadeGeneric facadeEstado = new FacadeEstado();
            IFacadeGeneric facadePais = new FacadePais();
            IFacadeGeneric facadeEnderecoCliente = new FacadeEnderecoCliente();

            try {
                facadePais.Inserir(formCadCliente.Endereco.Municipio.Estado.Pais);
                facadeEstado.Inserir(formCadCliente.Endereco.Municipio.Estado);
                facadeMunicipio.Inserir(formCadCliente.Endereco.Municipio);
                facadeEndereco.Inserir(formCadCliente.Endereco);
                facadeEnderecoCliente.Inserir(formCadCliente);
                TempData["SucessoCadastroNovoEndereco"] = "Sucesso ao cadastrar o endereço";

            } catch (Exception) {
                TempData["FalhaCadastroNovoEndereco"] = "Falha ao cadastrar o endereço";
            }
            return RedirectToAction("ExibirDadosCliente", "Cliente", new { id = formCadCliente.Cliente.Id });
        }

        public IActionResult DeletarEndereco(int idCliente, int idEndereco) {
            IFacadeGeneric facadeEnderecoCliente = new FacadeEnderecoCliente();
            IFacadeGeneric facadeEndereco = new FacadeEndereco();

            if (facadeEnderecoCliente.Delete(new FormCadCliente { Endereco = new Endereco { Id = idEndereco }, Cliente = new Usuario { Id = idCliente } })){


                TempData["SucessoExclusaoEndereco"] = "Endereço excluido com sucesso";
            } else {
                TempData["FalhaExclusaoEndereco"] = "Falha ao excluir o endereco";

            }
            return RedirectToAction("ExibirDadosCliente", "Cliente", new { id = idCliente });
        }

        [HttpPost]
        public IActionResult AtualizarEndereco(FormCadCliente formCadCliente) {
            IFacadeGeneric facadeEndereco = new FacadeEndereco();
            IFacadeGeneric facadeEnderecoCliente = new FacadeEnderecoCliente();
            IFacadeGeneric facadeMunicipio = new FacadeMunicipio();
            IFacadeGeneric facadeEstado = new FacadeEstado();
            IFacadeGeneric facadePais = new FacadePais();

            try {
                facadeEnderecoCliente.Delete(formCadCliente);
                facadePais.Inserir(formCadCliente.Endereco.Municipio.Estado.Pais);
                facadeEstado.Inserir(formCadCliente.Endereco.Municipio.Estado);
                facadeMunicipio.Inserir(formCadCliente.Endereco.Municipio);
                facadeEndereco.Inserir(formCadCliente.Endereco);
                facadeEnderecoCliente.Inserir(formCadCliente);
                TempData["SucessoCadastroNovoEndereco"] = "Sucesso ao atualizar o endereço";
            } catch (Exception ex) {
                TempData["FalhaCadastroNovoEndereco"] = "Falha ao atualizar o endereço";
            }
            return RedirectToAction("ExibirDadosCliente", "Cliente", new { id = formCadCliente.Cliente.Id });
        }
    }
}
