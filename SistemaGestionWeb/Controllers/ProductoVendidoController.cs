using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaGestionWeb.Models;
using System.Text;

namespace SistemaGestionWeb.Controllers
{
    public class ProductoVendidoController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductoVendidoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7002/api");
        }

        // Index
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/ProductoVendido/lista");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var productosVendidos = JsonConvert.DeserializeObject<IEnumerable<ProductoVendidoViewModel>>(content);
                return View("Index", productosVendidos);
            }

            return View(new List<ProductoVendidoViewModel>());
        }

        // Para "Crear" un nuevo registro, primero llamamos a una nueva vista (Create) y
        // luego declaramos el controller para la insercion del registro en la bbdd
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductoVendidoViewModel productoVendido)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(productoVendido);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/ProductoVendido/crear", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear Producto");
                }
            }

            return View(productoVendido);
        }

        // Edit, creamos 2 métodos: GET y POST
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/ProductoVendido/ver?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var productoVendido = JsonConvert.DeserializeObject<ProductoVendidoViewModel>(content);

                return View(productoVendido);
            }
            else
            {
                return RedirectToAction("Details");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductoVendidoViewModel productoVendido)
        {
            if (id != productoVendido.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(productoVendido);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/ProductoVendido/editar?id={id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { id });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al editar ProductoVendido");
                }
            }

            // Si hay error de validación, mostrar el formulario de edición con el/los errores.
            return View(productoVendido);
        }

        // Detalle
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"/api/ProductoVendido/ver?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var productoVendido = JsonConvert.DeserializeObject<ProductoVendidoViewModel>(content);

                return View(productoVendido);
            }
            else
            {
                return RedirectToAction("Details");
            }
        }

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/ProductoVendido/eliminar?id={id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar ProductoVendido";
                return RedirectToAction("Index");
            }
        }



    }
}
