using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_aleLobo17.Models;
using productoReposotory;
using productos;
using System.Net.Http.Headers;

namespace productosController
{
    public class ProductosController : Controller
    {
        private readonly ProductosRepository _productosRepository;

        public ProductosController()
        {
            _productosRepository = new ProductosRepository();
        }
        [HttpGet]
        public IActionResult ListarProductos()
        {
            return View(_productosRepository.ObtenerProductos());
        }
        [HttpGet]
        public IActionResult CrearProductos()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearProductos(Productos producto)
        {
            if (ModelState.IsValid) // Verifica si el modelo es válido
            {
                _productosRepository.CrearProductos(producto); // Guarda el producto en el repositorio
                return RedirectToAction(nameof(ListarProductos)); // Redirige a la lista de productos
            }
            return View(producto); // Si hay errores, vuelve a la vista del formulario con el modelo
        }

        [HttpGet]
        public IActionResult ModificarProductos(int id)
        {
            var producto = _productosRepository.ObtenerDetalleProducto(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost]
        public IActionResult ModificarProductos(int id, Productos producto)
        {
            if (ModelState.IsValid)
            {
                _productosRepository.ModificarProductos(id, producto);
                return RedirectToAction(nameof(ListarProductos));
            }
            return View(producto);
        }

        [HttpGet]
        public IActionResult EliminarProductos(int id)
        {
            var producto = _productosRepository.ObtenerDetalleProducto(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost]
        public IActionResult EliminarProductosConfirmarEliminacion(int id)
        {
            _productosRepository.EliminarProducto(id);
            return RedirectToAction(nameof(ListarProductos));
        }

    }



}