using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaGestionWeb.Models;
using System.Text;

namespace SistemaGestionWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly HttpClient _httpClient;

        public UsuarioController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7002/api");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Usuario/lista");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usuarios = JsonConvert.DeserializeObject<IEnumerable<UsuarioViewModel>>(content);
                return View("Index", usuarios);
            }

            return View(new List<UsuarioViewModel>());
        }

        // Para "Crear" un nuevo registro, primero llamamos a una nueva vista (Create) y
        // luego declaramos el controller para la insercion del registro en la bbdd
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Usuario/crear", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear Usuario");
                }
            }

            return View(usuario);
        }

        // Edit, creamos 2 métodos: GET y POST
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/Usuario/ver?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(content);

                return View(usuario);
            }
            else
            {
                return RedirectToAction("Details");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UsuarioViewModel usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Usuario/editar?id={id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { id });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al editar Usuario");
                }
            }

            // Si hay error de validación, mostrar el formulario de edición con el/los errores.
            return View(usuario);
        }

        // Detalle
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Usuario/ver?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(content);

                return View(usuario);
            }
            else
            {
                return RedirectToAction("Details");
            }
        }

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Usuario/eliminar?id={id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar Usuario";
                return RedirectToAction("Index");
            }
        }


    }
}
