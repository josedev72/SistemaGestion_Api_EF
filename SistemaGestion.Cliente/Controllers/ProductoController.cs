using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaGestion.Cliente.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace SistemaGestion.Cliente.Controllers
{
    public class ProductoController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7002/api");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Producto/lista");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var productos =JsonConvert.DeserializeObject<IEnumerable<ProductoViewModel>>(content);
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


    }
}
