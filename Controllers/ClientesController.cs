using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using clienteRepository;
using clientes;
using iClientesRepository;

using tl2_tp6_2024_aleLobo17.Models;

namespace clientesController{
    public class ClientesController : Controller
{
    private readonly ILogger<ClientesController> _logger;
    private readonly IClientesRepository _clientesRepository;

    public ClientesController(ILogger<ClientesController> logger, IClientesRepository clientesRepository)
    {
        _logger = logger;
        _clientesRepository = clientesRepository;
    }

    [HttpGet]
    public IActionResult ListarClientes()
    {

            var clientes = _clientesRepository.ObtenerClientes();
            return View(clientes);

    }

    [HttpGet]
    public IActionResult CrearCliente()
    {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User"))) return RedirectToAction("Index", "Login");
            if (HttpContext.Session.GetString("Rol") != "Admin")
            {
                TempData["ErrorMessage"] = "No tienes permisos para realizar esta acción.";
                return RedirectToAction("ListarClientes");
            }
            return View();
        

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CrearCliente(Clientes cliente)
    {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User"))) return RedirectToAction("Index", "Login");
            if (HttpContext.Session.GetString("Rol") != "Admin")
            {
                TempData["ErrorMessage"] = "No tienes permisos para realizar esta acción.";
                return RedirectToAction("ListarClientes");
            }

            if (ModelState.IsValid)
            {
                _clientesRepository.CrearCliente(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);

    }

    [HttpGet]
    public IActionResult ModificarCliente(int id)
    {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User"))) return RedirectToAction("Index", "Login");
            if (HttpContext.Session.GetString("Rol") != "Admin")
            {
                TempData["ErrorMessage"] = "No tienes permisos para realizar esta acción.";
                return RedirectToAction("ListarClientes");
            }
            var cliente = _clientesRepository.ObtenerCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ModificarCliente(int id, Clientes cliente)
    {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User"))) return RedirectToAction("Index", "Login");
            if (HttpContext.Session.GetString("Rol") != "Admin")
            {
                TempData["ErrorMessage"] = "No tienes permisos para realizar esta acción.";
                return RedirectToAction("ListarClientes");
            }

            if (ModelState.IsValid)
            {
                _clientesRepository.ModificarCliente(id, cliente);
                return RedirectToAction(nameof(ListarClientes));
            }
            return View(cliente);

    }

    [HttpGet]
    public IActionResult EliminarCliente(int id)
    {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User"))) return RedirectToAction("Index", "Login");
            if (HttpContext.Session.GetString("Rol") != "Admin")
            {
                TempData["ErrorMessage"] = "No tienes permisos para realizar esta acción.";
                return RedirectToAction("ListarClientes");
            }
            var cliente = _clientesRepository.ObtenerCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        
    }

    [HttpPost]
    [ValidateAntiForgeryToken] //Es una buena práctica proteger las acciones POST con tokens antifalsificación para prevenir ataques Cross-Site Request Forgery (CSRF).
    public IActionResult EliminarClienteConfirmado(int id)
    {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User"))) return RedirectToAction("Index", "Login");
            if (HttpContext.Session.GetString("Rol") != "Admin")
            {
                TempData["ErrorMessage"] = "No tienes permisos para realizar esta acción.";
                return RedirectToAction("ListarClientes");
            }
            
            _clientesRepository.EliminarCliente(id);
            return RedirectToAction(nameof(ListarClientes));

    }

    public IActionResult Index() //??
    {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User"))) return RedirectToAction("Index", "Login");
            ViewData["Admin"] = HttpContext.Session.GetString("Rol") == "Admin";
            return View(_clientesRepository.ObtenerClientes());
        

    }
}
}