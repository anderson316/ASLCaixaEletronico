using System.Diagnostics;
using ASLCaixaEletronico.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASLCaixaEletronico.Controllers
{
    public class SaqueController : Controller
    {
        private readonly Service.SaqueService _saqueService = new();

        [HttpGet]
        public IActionResult Index()
        {
            return View(new SaqueViewModel());
        }
        [HttpPost]
        public IActionResult Index(SaqueViewModel model)
        {
            try
            {
                model.CombinacoesDetalhadas = _saqueService.CombinacoesNotas(model.Valor);
            }
            catch (ArgumentException ex)
            {
                model.MensagemErro = ex.Message;
            }
            return View(model);
        }
    }
}
