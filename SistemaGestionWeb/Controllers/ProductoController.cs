using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaGestionWeb.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace SistemaGestionWeb.Controllers
{
    public class ProductoController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7002/api");
        }

        // Index
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Producto/lista");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var productos = JsonConvert.DeserializeObject<IEnumerable<ProductoViewModel>>(content);
                return View("Index", productos);
            }

            return View(new List<ProductoViewModel>());
        }

        // Para "Crear" un nuevo registro, primero llamamos a una nueva vista (Create) y
        // luego declaramos el controller para la insercion del registro en la bbdd
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductoViewModel producto)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Producto/crear", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear Producto");
                }
            }

            return View(producto);
        }

        // Edit, creamos 2 métodos: GET y POST
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/Producto/ver?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var producto = JsonConvert.DeserializeObject<ProductoViewModel>(content);

                return View(producto);
            }
            else
            {
                return RedirectToAction("Details");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductoViewModel producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Producto/editar?id={id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { id });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al editar Producto");
                }
            }

            // Si hay error de validación, mostrar el formulario de edición con el/los errores.
            return View(producto);
        }

        // Detalle
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Producto/ver?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var producto = JsonConvert.DeserializeObject<ProductoViewModel>(content);

                return View(producto);
            }
            else
            {
                return RedirectToAction("Details");
            }
        }

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Producto/eliminar?id={id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar Producto";
                return RedirectToAction("Index");
            }
        }


    }
}
