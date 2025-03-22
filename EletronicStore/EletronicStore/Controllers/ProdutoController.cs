using Microsoft.AspNetCore.Mvc;

namespace EletronicStore.Controllers {
    public class ProdutoController : Controller {
        public IActionResult CadastrarNovoProduto() {
            return View();
        }
    }
}
