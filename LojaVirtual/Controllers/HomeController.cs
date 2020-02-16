using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ContatoAcao()
        {
            try
            {
                Contato contato = new Contato();
                contato.Nome = HttpContext.Request.Form["nome"];
                contato.Email = HttpContext.Request.Form["email"];
                contato.Texto = HttpContext.Request.Form["texto"];

                var ListaMensagens = new List<ValidationResult>();
                var context = new ValidationContext(contato);
                bool isValid = Validator.TryValidateObject(contato, context, ListaMensagens, true);

                if (isValid)
                {
                    ContatoEmail.EnviarContatoPorEmail(contato);
                    ViewData["MSG_S"] = "Mensagem de contato enviado com sucesso!";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var texto in ListaMensagens)
                    {
                        sb.Append(texto.ErrorMessage + "<br/>");
                    }
                    ViewData["MSG_S"] = sb.ToString();
                    ViewData["CONTATO"] = contato;
                }
            }
            catch (Exception e)
            {
                ViewData["MSG_S"] = "Opps! Tivemos um erro, tente noveamente mais tarde!";
                //TODO - Implementar Log

            }

            return View("Contato");
        }

        public IActionResult Contato()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult CadastroCliente()
        {
            return View();
        }
        public IActionResult CarrinhoCompras()
        {
            return View();
        }
    }
}