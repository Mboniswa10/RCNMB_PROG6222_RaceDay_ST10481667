using RaceDay.Mvc.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace RaceDay.Mvc.Controllers
{
    public class OrganiserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public OrganiserController(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            // Check whether the user is logged in.
            if (string.IsNullOrEmpty(
                HttpContext.Session.GetString("JWTToken")))
            {
                return RedirectToAction("Login", "Account");
            }

            // Only organisers may access this dashboard.
            var role = HttpContext.Session.GetString("UserRole");

            if (!string.Equals(
                role,
                "Organiser",
                StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.UserEmail =
                HttpContext.Session.GetString("UserEmail");

            return View();
        }

        public IActionResult Events()
        {
            return View();
        }

        public IActionResult CreateEvent()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEvent(CreateEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var token = HttpContext.Session.GetString("JWTToken");
            var role = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(token) ||
                !string.Equals(role, "Organiser",
                    StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                var client = _httpClientFactory.CreateClient();

                var baseUrl = _configuration["ApiSettings:BaseUrl"];

                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    ModelState.AddModelError(
                        string.Empty,
                        "The RaceDay API address is not configured.");

                    return View(model);
                }

                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var response = await client.PostAsJsonAsync(
                    "api/Events",
                    model);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] =
                        "Your event has been created successfully!";

                    return RedirectToAction("Events");
                }

                var error = await response.Content.ReadAsStringAsync();

                ModelState.AddModelError(
                    string.Empty,
                    $"Event creation failed. API response: {error}");

                return View(model);
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Cannot connect to the RaceDay API. Please try again.");

                return View(model);
            }
        }

        public IActionResult Categories()
        {
            return View();
        }

        public IActionResult Enrolments()
        {
            return View();
        }

        public IActionResult Results()
        {
            return View();
        }
    }
}
