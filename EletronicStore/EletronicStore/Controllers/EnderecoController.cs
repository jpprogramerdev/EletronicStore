using Microsoft.AspNetCore.Mvc;

namespace EletronicStore.Controllers {
    public class EnderecoController : Controller {
        public IActionResult CadastrarEndereco() {
            return View();
        }

        public IActionResult EditarEndereco() {
            return View();
        }

        public void ExcluirEndereco() {
            //montar código
        }
    }
}
