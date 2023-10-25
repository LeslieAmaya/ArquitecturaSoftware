using Microsoft.AspNetCore.Mvc;
using ContactoDB.Datos;
using ContactoDB.Models;
using System.Security.Permissions;

namespace ContactoDB.Controllers
{
    public class ContactoController : Controller
    {
        ContactoDatos contactoDatos = new ContactoDatos();
        public IActionResult Listar()
        {
            var lista = contactoDatos.ListarContacto();
            return View(lista);
        }
        [HttpGet] //Para que acrgue el formulario
        public IActionResult Guardar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Guardar(ContactoModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var respuesta = contactoDatos.GuardarContacto(model);
            if (respuesta) 
                return RedirectToAction("Listar"); //vista a la que queremos reenviar
            else
            {
                return View();
            }

        }
        [HttpGet]
        public IActionResult Editar(int IdContacto)
        {
            ContactoModel contacto = contactoDatos.ObtenerContacto(IdContacto);
            return View(contacto);
        }
        [HttpPost]
        public IActionResult Editar(ContactoModel model)
        {
            var resultado = contactoDatos.EditarContacto(model);
            if(resultado)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
		public IActionResult Eliminar(int id)
		{
			var contacto = contactoDatos.ObtenerContacto(id);
            return View(contacto);
		}
        [HttpPost]
        public IActionResult Eliminar(ContactoModel model)
        {
            var respuesta = contactoDatos.EliminarContacto(model);
            if(respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }

        }
	}
}
