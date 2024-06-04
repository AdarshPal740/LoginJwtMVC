using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JwtAuthMvcApp.Models;
using JwtAuthMvcApp.Services;
using login.Models;

namespace JwtAuthMvcApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly LoginContext _context;
        private readonly TokenService _tokenService;
        private IConfiguration _configuration;
        public AccountController(LoginContext context, TokenService tokenService, IConfiguration configuration)
        {
            _context = context;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(loginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Logintbs.SingleOrDefaultAsync(u => u.Email == model.Email && u.Password==model.Password);
                if (user != null)
                {
                    var token = _tokenService.GenerateToken(user);

                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        
                        Expires = DateTime.Now.AddMinutes(30)
                    };

                    Response.Cookies.Append("jwtToken", token, cookieOptions);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }
    }
}
