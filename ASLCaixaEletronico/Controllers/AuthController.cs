using ASLCaixaEletronico.Data;
using ASLCaixaEletronico.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASLCaixaEletronico.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        public AuthController(AppDbContext context)
        {
            _context = context;
        }
        #region Login

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(string username, string senha)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Username == username && u.Senha == senha);
            if (usuario != null)
            {
                HttpContext.Session.SetString("usuarioLogado", usuario.Username);
                return RedirectToAction("Index", "Saque");
            }
            else
            {
                ViewBag.ErrorMessage = "Usuário ou senha inválidos.";
                return View();
            }
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("usuarioLogado");
            return RedirectToAction("Login");
        }

        #endregion

        #region Register

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // POST: processa o cadastro
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(Usuario model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_context.Usuarios.Any(u => u.Username == model.Username))
            {
                ViewBag.ErrorMessage = "Username já utilizado.";
                return View(model);
            }
            _context.Usuarios.Add(model);
            _context.SaveChanges();

            HttpContext.Session.SetString("usuarioLogado", model.Username);

            return RedirectToAction("Index", "Saque");
        }
        #endregion
    }
}
