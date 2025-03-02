using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using clienteRepository;
using clientes;

using tl2_tp6_2024_aleLobo17.Models;

namespace clientesController{
    public class ClientesController : Controller
{
    private readonly ILogger<ClientesController> _logger;
    private readonly ClientesRepository _clientesRepository;

    public ClientesController(ILogger<ClientesController> logger)
    {
        _logger = logger;
        _clientesRepository = new ClientesRepository();
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
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CrearCliente(Clientes cliente)
    {
        if (ModelState.IsValid) // se utiliza para verificar si los datos enviados en un formulario cumplen con todas las reglas de validación definidas en el modelo de datos.
        {
            _clientesRepository.CrearCliente(cliente);
            return RedirectToAction(nameof(ListarClientes));
        }
        return View(cliente);
    }

    [HttpGet]
    public IActionResult ModificarCliente(int id)
    {
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
        //En este caso no es necesario el ModelState.IsValid porque solo recibo un dato simple(id)
        _clientesRepository.EliminarCliente(id);
        return RedirectToAction(nameof(ListarClientes));
    }

    public IActionResult Index()
    {
        return View(_clientesRepository.ObtenerClientes());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
}