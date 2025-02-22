using Microsoft.AspNetCore.Mvc;

namespace EletronicStore.Controllers {
    public class ClienteController : Controller {
        public IActionResult CadastrarCliente() {
            return View();
        }

        public IActionResult ExibirClientes() {
            return View();
        }

        public IActionResult ExibirDadosCliente() {
            return View();
        }

        public IActionResult AtualizarCliente() {
            return View();
        }

        public void ExcluirCliente() {
            //montar método
        }
    }
}
