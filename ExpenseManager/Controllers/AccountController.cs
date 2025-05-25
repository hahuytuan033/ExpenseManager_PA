using Microsoft.AspNetCore.Mvc;
using ExpenseManager.Models;
using System.Linq;

public class AccountController : Controller
{
    private readonly ExpenseDBContext _context;
    public AccountController(ExpenseDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public IActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }
        return View(user);
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // Kiểm tra tài khoản cứng
        if (username == "Admin123" && password == "HUMG1234")
        {
            HttpContext.Session.SetString("Username", username);
            return RedirectToAction("Index", "Expense");
        }
        ViewBag.Error = "Sai tài khoản hoặc mật khẩu";
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}