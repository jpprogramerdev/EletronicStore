using Microsoft.AspNetCore.Mvc;

namespace EletronicStore.Controllers {
    public class CartaoController : Controller {
        public IActionResult CadastrarCartao() {
            return View();
        }

        public IActionResult EditarCartao() {
            return View();
        }

        public void ExcluirCartao() {
            //montar código de exclusão
        }
    }
}
