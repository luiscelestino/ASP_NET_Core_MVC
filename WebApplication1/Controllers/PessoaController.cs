using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PessoaController : Controller
    {
        public static List<Pessoa> Lista { get; set; }

        public PessoaController()
        {
            if (Lista == null)
            {
                Lista = new List<Pessoa>();
            }
        }

        public IActionResult Index()
        {
            ViewBag.Mensagem = "Nossa primeira view";

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Pessoa model)
        {
            ModelState.Remove("Codigo");

            if (ModelState.IsValid)
            {
                model.Codigo = Lista.Count + 1;
                Lista.Add(model);
            }
            else
            {
                return View(model);
            }

            return View("List", Lista);
        }

        public IActionResult List()
        {
            if (Lista != null)
            {
                return View(Lista);
            }
            return View(new List<Pessoa>());
        }

        public IActionResult Edit(int id)
        {
            if (Lista.Where(p => p.Codigo == id).Any())
            {
                var model = Lista.Where(p => p.Codigo == id).FirstOrDefault();

                return View(model);
            }

            return View(new Pessoa());
        }

        [HttpPost]
        public IActionResult Edit(Pessoa model)
        {
            if (Lista != null)
            {
                if (Lista.Where(p => p.Codigo == model.Codigo).Any())
                {
                    //var modelBase = Lista.Where(p => p.Codigo == model.Codigo).FirstOrDefault();

                    // Atualiza seu registro com o model enviado por parametro...
                    Lista[model.Codigo - 1] = model;
                }
            }

            return View("List", Lista);
        }

        public IActionResult Delete(int id)
        {
            if (Lista != null)
            {
                if (Lista.Where(p => p.Codigo == id).Any())
                {
                    var modelBase = Lista.Where(p => p.Codigo == id).FirstOrDefault();
                    Lista.Remove(modelBase);
                    return View("List", Lista);
                }
            }

            return View("List", new List<Pessoa>());
        }
        
    }
}
