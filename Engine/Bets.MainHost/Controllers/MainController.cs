using System.Diagnostics;
using Bets.MainHost.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bets.MainHost.Controllers;
public class MainController : Controller {
    private readonly ILogger<MainController> _logger;

    public MainController(ILogger<MainController> logger) {
        _logger = logger;
    }

    public IActionResult Index() {
        return View();
    }

    public IActionResult Privacy() {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Information() {
        return View();
    }

    [HttpPost]
    public IActionResult SubmitContact(string name, string email, string message) {

        //TODO: Implement SubmitContact Request
        ViewBag.Message = "������� �� ���� ���������! �� �������� � ���� � ��������� �����.";

        return View("Information");
    }

    // Handle the "������ �������" route
    public IActionResult Profile() {
        // Simulating fetching user profile (replace with actual user fetching logic)
        // TODO: Remove user profile simulation!
        User user = new() {
            Name = "���� ������",
            Email = "ivan@example.com",
            PhoneNumber = "+7 999 123 4567",
            DateOfBirth = "12 ������ 1990 ����"
        };

        return View(user);
    }

}
