
using Microsoft.AspNetCore.Mvc;
using RaceDay.Mvc.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace RaceDay.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AccountController(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var client = _httpClientFactory.CreateClient();
                var baseUrl = _configuration["ApiSettings:BaseUrl"];

                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    ModelState.AddModelError(
                        string.Empty,
                        "The API address is not configured.");
                    return View(model);
                }

                client.BaseAddress = new Uri(baseUrl);

                var response = await client.PostAsJsonAsync(
                    "api/Auth/login",
                    new
                    {
                        Email = model.Email.Trim(),
                        model.Password
                    });

                if (!response.IsSuccessStatusCode)
                {
                    var apiError = await response.Content.ReadAsStringAsync();

                    ModelState.AddModelError(
                        string.Empty,
                        $"API Error: HTTP {(int)response.StatusCode} - {apiError}");

                    return View(model);
                }

                using var json = await JsonDocument.ParseAsync(
                    await response.Content.ReadAsStreamAsync());

                var root = json.RootElement;
                var token = root.GetProperty("token").GetString();

                if (string.IsNullOrWhiteSpace(token))
                {
                    ModelState.AddModelError(
                        string.Empty,
                        "The API did not return an authentication token.");
                    return View(model);
                }

                var user = root.GetProperty("user");

                var role = user.GetProperty("role").GetString();
                var userId = user.GetProperty("userId").GetInt32();
                var fullName = user.GetProperty("fullName").GetString();
                var email = user.GetProperty("email").GetString();

                HttpContext.Session.SetString("JWTToken", token);
                HttpContext.Session.SetString("UserRole", role ?? "Participant");
                HttpContext.Session.SetString("UserId", userId.ToString());
                HttpContext.Session.SetString("UserFullName", fullName ?? "");
                HttpContext.Session.SetString("UserEmail", email ?? "");

                if (string.Equals(
                    role, "Organiser",
                    StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("Index", "Organiser");
                }

                return RedirectToAction("Index", "Participant");
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    $"API connection error: {ex.Message} {ex.InnerException?.Message}");

                return View(model);
            }
            catch (JsonException)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "The API returned an invalid login response.");
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    $"Login error: {ex.Message}");

                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var client = _httpClientFactory.CreateClient();
                var baseUrl = _configuration["ApiSettings:BaseUrl"];

                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    ModelState.AddModelError(
                        string.Empty,
                        "The API address is not configured.");
                    return View(model);
                }

                client.BaseAddress = new Uri(baseUrl);

                var response = await client.PostAsJsonAsync(
                    "api/Auth/register",
                    new
                    {
                        model.FullName,
                        model.Email,
                        model.Password
                    });

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] =
                        "Registration successful. Please log in.";

                    return RedirectToAction("Login");
                }

                ModelState.AddModelError(
                    string.Empty,
                    "Registration failed. This email may already be registered.");

                return View(model);
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    $"API connection error: {ex.Message} {ex.InnerException?.Message}");

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}