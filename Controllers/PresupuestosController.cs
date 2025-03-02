using Microsoft.AspNetCore.Mvc;
using presupuestoRepository;
using presupuestos;
using clienteRepository;
using productoReposotory;

namespace presupuestoController
{
    public class PresupuestosController : Controller
    {
        private readonly PresupuestosRepository _presupuestoRepository;
        private readonly ClientesRepository _clientesRepository;
        private readonly ProductosRepository _productosRepository;

        public PresupuestosController()
        {
            _presupuestoRepository = new PresupuestosRepository();
            _clientesRepository = new ClientesRepository(); // Inicializamos el repositorio de 
            _productosRepository = new ProductosRepository();
        }

        [HttpGet]
        public IActionResult ListarPresupuestos()
        {
            return View(_presupuestoRepository.ObtenerPresupuestos());
        }

        [HttpGet]
        public IActionResult ListarDetallesPresupuestos(int id)
        {
            return View(_presupuestoRepository.ObtenerDetalle(id));
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
            if (ModelState.IsValid) // Verifica si el modelo es válido
            {
                _presupuestoRepository.CrearPresupuesto(presupuesto); // Guarda el presupuesto en el repositorio
                return RedirectToAction(nameof(ListarPresupuestos)); // Redirige a la lista de presupuestos
            }

            // Si hay errores, recarga la lista de clientes
            ViewBag.Clientes = _clientesRepository.ObtenerClientes();
            ViewBag.Productos = _productosRepository.ObtenerProductos();
            return View(presupuesto);
        }

        [HttpGet]
        public IActionResult ModificarPresupuestos(int id)
        {
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
            _presupuestoRepository.EliminarPresupuestoPorId(id);
            return RedirectToAction(nameof(ListarPresupuestos));
        }
    }
}
