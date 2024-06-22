using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaGestionWeb.Models;
using System.Text;

namespace SistemaGestionWeb.Controllers
{
    public class VentaController : Controller
    {
        private readonly HttpClient _httpClient;

        public VentaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7002/api");
        }

        // Index
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Venta/lista");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var ventas = JsonConvert.DeserializeObject<IEnumerable<VentaViewModel>>(content);
                return View("Index", ventas);
            }

            return View(new List<VentaViewModel>());
        }

        // Para "Crear" un nuevo registro, primero llamamos a una nueva vista (Create) y
        // luego declaramos el controller para la insercion del registro en la bbdd
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VentaViewModel venta)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(venta);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Venta/crear", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear Venta");
                }
            }

            return View(venta);
        }

        // Edit, creamos 2 métodos: GET y POST
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/Venta/ver?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var venta = JsonConvert.DeserializeObject<VentaViewModel>(content);

                return View(venta);
            }
            else
            {
                return RedirectToAction("Details");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, VentaViewModel venta)
        {
            if (id != venta.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(venta);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //var response = await _httpClient.PutAsync($"/api/Productos/editarProducto?id={id}", content);
                var response = await _httpClient.PutAsync($"/api/Venta/editar?id={id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { id });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al editar Venta");
                }
            }

            // Si hay error de validación, mostrar el formulario de edición con el/los errores.
            return View(venta);
        }

        // Detalle
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Venta/ver?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var venta = JsonConvert.DeserializeObject<VentaViewModel>(content);

                return View(venta);
            }
            else
            {
                return RedirectToAction("Details");
            }
        }

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Venta/eliminar?id={id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar Venta";
                return RedirectToAction("Index");
            }
        }



    }
}
