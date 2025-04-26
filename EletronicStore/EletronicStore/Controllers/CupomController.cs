using EletronicStore.Facade;
using EletronicStore.Facade.Interface;
using EletronicStore.Models;
using EletronicStore.Strategy;
using Microsoft.AspNetCore.Mvc;

namespace EletronicStore.Controllers {
    public class CupomController : Controller {
        public IActionResult CriarCupomTroca(Usuario Usuario) {
            Cupom Cupom = new Cupom() { Usuario = Usuario };
            return View(Cupom);
        }

        public IActionResult SalvarCupomTroca(Cupom Cupom) {
            IFacadeGeneric facadeCupom = new FacadeCupom();
            
            Cupom.Nome = "CUPOMTROCA";
            facadeCupom.Inserir(Cupom);
            TempData["CupomTrocaCriado"] = $"Cupom de troca gerado para o pedido.<br/>Informações sobre o cupom:<br/>Nome: {Cupom.Nome}<br/>Desconto: R$ {Cupom.Desconto.ToString ("F2")}";

            return RedirectToAction("ExibirTodosPedidos", "Pedido", new { Id = Cupom.Usuario.Id });
        }
    }
}
