using Microsoft.AspNetCore.Mvc;
using RaceDay.Mvc.Models;
using RaceDay.Mvc.Services;

namespace RaceDay.Mvc.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly RaceDayApiService _apiService;

        public ParticipantController(RaceDayApiService apiService)
        {
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("JWTToken")))
                return RedirectToAction("Login", "Account");

            if (HttpContext.Session.GetString("UserRole") != "Participant")
                return RedirectToAction("Login", "Account");

            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");

            return View();
        }

        public async Task<IActionResult> Events()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("JWTToken")))
                return RedirectToAction("Login", "Account");

            try
            {
                var events = await _apiService.GetAsync<List<EventViewModel>>("api/Events");

                return View(events ?? new List<EventViewModel>());
            }
            catch (HttpRequestException)
            {
                ViewBag.ErrorMessage =
                    "Unable to load events. Please check that the RaceDay API is running.";

                return View(new List<EventViewModel>());
            }
        }

        public IActionResult MyEnrolments()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("JWTToken")))
                return RedirectToAction("Login", "Account");

            return View();
        }

        public IActionResult Results()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("JWTToken")))
                return RedirectToAction("Login", "Account");

            return View();
        }

        public IActionResult Profile()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("JWTToken")))
                return RedirectToAction("Login", "Account");

            return View();
        }
    }
}