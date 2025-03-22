using EletronicStore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using EletronicStore.Exceptions;
using EletronicStore.Facade.Interface;
using EletronicStore.Facade;
using EletronicStore.Models;
using EletronicStore.Strategy.Interface;
using EletronicStore.Strategy;

namespace EletronicStore.Controllers {
    public class ClienteController : Controller {
        public IActionResult CadastrarCliente() {
            return View();
        }

        public IActionResult ExibirClientes(string? filterNome, string? filterEmail, string filterStatus) {
            IFacadeGeneric facadeCliente = new FacadeCliente();
            List<Usuario> ListClient = facadeCliente.Selecionar().Cast<Usuario>().ToList();

            if (!string.IsNullOrEmpty(filterNome)) {
                ListClient = ListClient.Where(c => c.Nome.Contains(filterNome, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(filterEmail)) {
                ListClient = ListClient.Where(c => c.Email.Contains(filterEmail, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(filterStatus)) {
                bool statusFiltro = filterStatus == "1" ? true : false;

                ListClient = ListClient.Where(c => c.Status == statusFiltro).ToList();
            }


            return View(ListClient);
        }

        public IActionResult ExibirDadosCliente(int id) {
            IFacadeGeneric facadeCliente = new FacadeCliente();
            Usuario Cliente = facadeCliente.Selecionar().Cast<Usuario>().ToList().Where(c => c.Id == id).FirstOrDefault();
            return View(Cliente);
        }

        public IActionResult AtualizarCliente(int id) {
            IFacadeGeneric facadeCliente = new FacadeCliente();
            Usuario Cliente = facadeCliente.Selecionar().Cast<Usuario>().ToList().Where(c => c.Id == id).FirstOrDefault();
            return View(Cliente);
        }

        public IActionResult AlterarStatusCliente(int id) {
            IFacadeGeneric facadeCliente = new FacadeCliente();
            facadeCliente.Atualizar(facadeCliente.Selecionar().Cast<Usuario>().ToList().Where(c => c.Id == id).FirstOrDefault());
            return RedirectToAction("ExibirClientes", "Cliente");
        }

        [HttpPost]
        public IActionResult SalvarCliente(FormCadCliente FormCadCliente) {
            try {
                IFacadeGeneric facadeCliente = new FacadeCliente();
                IFacadeGeneric facadeCartao = new FacadeCartao();
                IFacadeGeneric facadePais = new FacadePais();
                IFacadeGeneric facadeEstado = new FacadeEstado();
                IFacadeGeneric facadeCidade = new FacadeMunicipio();
                IFacadeGeneric facadeEndereco = new FacadeEndereco();
                IFacadeGeneric facadeTelefone = new FacadeTelefone();
                IFacadeGeneric facadeCartaoCliente = new FacadeCartaoCliente();
                IFacadeGeneric facadeEnderecoCliente = new FacadeEnderecoCliente();

                IStrategy ValidarSenha = new ValidarSenha();
                IStrategy GerarLogUsuario = new GerarLogUsuario();
                IStrategy CriptgrafarSenha = new CriptografarSenha();

                ValidarSenha.Executar(FormCadCliente.Cliente);
                CriptgrafarSenha.Executar(FormCadCliente.Cliente);
                facadeTelefone.Inserir(FormCadCliente.Cliente.Telefone);

                facadeCartao.Inserir(FormCadCliente.Cartao);

                facadePais.Inserir(FormCadCliente.Endereco.Municipio.Estado.Pais);
                facadeEstado.Inserir(FormCadCliente.Endereco.Municipio.Estado);
                facadeCidade.Inserir(FormCadCliente.Endereco.Municipio);
                facadeEndereco.Inserir(FormCadCliente.Endereco);

                facadeCliente.Inserir(FormCadCliente.Cliente);

                //Inserção nas tabaleas de relação
                facadeCartaoCliente.Inserir(FormCadCliente);
                facadeEnderecoCliente.Inserir(FormCadCliente);


                Usuario Cliente = facadeCliente.Selecionar().Cast<Usuario>().ToList().Where(c => c.Id == FormCadCliente.Cliente.Id).FirstOrDefault();

                GerarLogUsuario.Executar(Cliente);

                TempData["SucessoCadastro"] = "Sucesso ao cadastrar";
            } catch (UniqueCPFException CPFEx) {
                TempData["CPFJaCadastrado"] = CPFEx.Message;
            } catch (PadraoSenhaException PSenhaEX) {
                TempData["SenhaForaPadrao"] = PSenhaEX.Message;
            } catch (Exception ex){
                TempData["FahaCadastro"] = "Falha ao cadastrar Cliente. Preencha todos os campos";
            }
            return View("CadastrarCliente");
        }

        [HttpPost]
        public IActionResult AtualizarDadosCliente(Usuario Usuario) {
            IFacadeGeneric facadeCliente = new FacadeCliente();
            IFacadeGeneric facadeTelefone = new FacadeTelefone();
            IStrategy GerarLogUsuario = new GerarLogUsuario();


            try {
                facadeTelefone.Inserir(Usuario.Telefone);
                facadeCliente.Atualizar(Usuario);

                Usuario = facadeCliente.Selecionar().Cast<Usuario>().ToList().Where(c => c.Id == Usuario.Id).FirstOrDefault();

                GerarLogUsuario.Executar(Usuario);

                TempData["SucessoAtualizarCliente"] = "Sucesso ao atualizar o cliente";
            } catch (Exception ex) {
                TempData["FalhaAtualizarCliente"] = "Falha ao atualizar o cliente";
            }
            return RedirectToAction("AtualizarCliente", "Cliente", new { id = Usuario.Id });
        }
    }
}
