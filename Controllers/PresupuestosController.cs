using Microsoft.AspNetCore.Mvc;
using presupuestoRepository;
using presupuestos;
using clienteRepository;
using productoReposotory;
using iPresupuestosRepository;
using iClientesRepository;
using iProductosRepository;

namespace presupuestoController
{
    public class PresupuestosController : Controller
    {
        private readonly ILogger<PresupuestosController> _logger;
        private readonly IPresupuestosRepository _presupuestoRepository;
        private readonly IClientesRepository _clientesRepository;
        private readonly IProductosRepository _productosRepository;

        public PresupuestosController(ILogger<PresupuestosController> logger, IPresupuestosRepository presupuestosRepository, IProductosRepository productosRepository, IClientesRepository clientesRepository)
        {
            _logger = logger;
            _presupuestoRepository = presupuestosRepository;
            _clientesRepository = clientesRepository; // Inicializamos el repositorio de 
            _productosRepository = productosRepository;
        }

        [HttpGet]
        public IActionResult ListarPresupuestos()
        {
            return View(_presupuestoRepository.ObtenerPresupuestos());
        }

        [HttpGet]
        public IActionResult ListarDetallesPresupuestos(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User"))) return RedirectToAction("Index", "Login");
            var listaDetalle = _presupuestoRepository.ObtenerDetalle(id);
            return View(listaDetalle);
        }

        [HttpGet]
        public IActionResult CrearPresupuestos()
        {
            // Cargar la lista de clientes
            ViewBag.Clientes = _clientesRepository.ObtenerClientes();
            ViewBag.Productos = _productosRepository.ObtenerProductos();
            return View();
        }

        [HttpPost]
        public IActionResult CrearPresupuestos(Presupuestos presupuesto)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User"))) return RedirectToAction("Index", "Login");
            if (HttpContext.Session.GetString("Rol") != "Admin")
            {
                TempData["ErrorMessage"] = "No tienes permisos para realizar esta acción.";
                return RedirectToAction("ListarPresupuestos");
            }

            // Si hay errores, recarga la lista de clientes
            ViewBag.Clientes = _clientesRepository.ObtenerClientes();
            ViewBag.Productos = _productosRepository.ObtenerProductos();
            return View(presupuesto);
        }

        [HttpGet]
        public IActionResult ModificarPresupuestos(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User"))) return RedirectToAction ("Index", "Login");
            if (HttpContext.Session.GetString("Rol") != "Admin")
            {
                TempData["ErrorMessage"] = "No tienes permisos para realizar esta acción.";
                return RedirectToAction("ListarPresupuestos");
            }
            var presupuesto = _presupuestoRepository.ObtenerPresupuestoPorId(id);
            if (presupuesto == null)
            {
                return NotFound();
            }

            // Cargar la lista de clientes
            ViewBag.Clientes = _clientesRepository.ObtenerClientes();
            ViewBag.Productos = _productosRepository.ObtenerProductos();
            return View(presupuesto);
        }

        [HttpPost]
        public IActionResult ModificarPresupuestos(Presupuestos presupuesto)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User"))) return RedirectToAction ("Index", "Login");
            if (HttpContext.Session.GetString("Rol") != "Admin")
            {
                TempData["ErrorMessage"] = "No tienes permisos para realizar esta acción.";
                return RedirectToAction("ListarPresupuestos");
            }
            if (ModelState.IsValid)
            {
                _presupuestoRepository.ModificarPresupuestoQ(presupuesto);
                return RedirectToAction(nameof(ListarPresupuestos));
            }

            // Si hay errores, recarga la lista de clientes
            ViewBag.Clientes = _clientesRepository.ObtenerClientes();
            return View(presupuesto);
        }

        [HttpGet]
        public IActionResult EliminarPresupuestos(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User"))) return RedirectToAction ("Index", "Login");
            if (HttpContext.Session.GetString("Rol") != "Admin")
            {
                TempData["ErrorMessage"] = "No tienes permisos para realizar esta acción.";
                return RedirectToAction("ListarPresupuestos");
            }
            var presupuesto = _presupuestoRepository.ObtenerPresupuestoConDetalles(id);
            if (presupuesto == null)
            {
                return NotFound();
            }
            return View(presupuesto);
        }

        [HttpPost]
        public IActionResult EliminarPresupuestosConfirmarEliminacion(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User"))) return RedirectToAction ("Index", "Login");
            if (HttpContext.Session.GetString("Rol") != "Admin")
            {
                TempData["ErrorMessage"] = "No tienes permisos para realizar esta acción.";
                return RedirectToAction("ListarPresupuestos");
            }
            _presupuestoRepository.EliminarPresupuestoPorId(id);
            return RedirectToAction(nameof(ListarPresupuestos));
        }
    }
}
