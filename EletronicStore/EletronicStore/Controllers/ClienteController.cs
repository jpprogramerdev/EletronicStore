using Microsoft.AspNetCore.Mvc;

namespace EletronicStore.Controllers {
    public class ClienteController : Controller {
        public IActionResult Cadastro() {
            return View();
        }
    }
}
