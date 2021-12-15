using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TraSuaWeb.Areas.Admin.Models;
using TraSuaWeb.Models;

namespace TraSuaWeb.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles ="Admin")]//xac thuc tk
   [Authorize(AuthenticationSchemes = "AdministratorAuth")]
    public class AccountsController : Controller
    {
        private readonly DBtrasuaContext _context;

        public AccountsController(DBtrasuaContext context)
        {
            _context = context;
        }

        // GET: Admin/Accounts
        public async Task<IActionResult> Index()
        {
            var dBtrasuaContext = _context.Accounts.Include(a => a.Role);
            return View(await dBtrasuaContext.ToListAsync());
        }
        [HttpGet]
        [AllowAnonymous]
       [Route("Adminlogin.html", Name ="login")]
        public IActionResult Login( string returnUrl = null)
        {
           if (User.Identity.IsAuthenticated)
            return Redirect("/admin");
            var taikhoanID = HttpContext.Session.GetString("AccountId");//kiểm tra dữ liệu 
            if (taikhoanID != null) return RedirectToAction("Index", "Home", new { Area = "Admin" });
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost, AllowAnonymous]
       [Route("Adminlogin.html", Name = "login")]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Account add = _context.Accounts
                        .Include(p => p.Role)
                       .SingleOrDefault(p => p.Email.ToLower() == model.Email.ToLower().Trim());
                    if (add == null)
                    {
                        ViewBag.Error = "thong tin dang nhap chua chinh xac";
                        return View(model);
                    }
                    String pass = model.Password.Trim();
                    if (add.Password.Trim() != pass)
                    {
                        ViewBag.Error = "thong tin dang nhap chua chinh xac";
                        return View(model);
                    }                   
                    var taikhoanID = HttpContext.Session.GetString("AccountId");

                     HttpContext.Session.SetString("AccountId", add.AccountId.ToString());

                    var userClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,add.FullName),
                        new Claim(ClaimTypes.Email,add.Email),
                        new Claim("AccountId",add.AccountId.ToString()),
                        new Claim("RoleId",add.RoleId.ToString()),
                        new Claim(ClaimTypes.Role, add.Role.RoleName)
                    };
                    var grandmaIdentity = new ClaimsIdentity(userClaims, "AdministratorAuth");
                    var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                    //var status = HttpContext.SignInAsync("AdministratorAuth", new ClaimsPrincipal(userPrincipal), properties)IsCompleted;
                    await HttpContext.SignInAsync(userPrincipal);
                    HttpContext.Session.SetString("Ad", add.FullName);

                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                }
            }
            catch
            {
                return RedirectToAction("Login", "Accounts", new { Area = "Admin" });
            }
            return RedirectToAction("Login", "Accounts", new { Area = "Admin" });
        }

        [Route("Adminlogout.html", Name = "logout")]
        public IActionResult Logout()
        {
            try
            {
                HttpContext.SignOutAsync();
                HttpContext.Session.Remove("AccountId");
                return RedirectToAction("index", "home", new { Area = "Admin" });
            }
            catch
            {
                return RedirectToAction("index", "home", new { Area = "Admin" });
            }
        }
        // GET: Admin/Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Admin/Accounts/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            return View();
        }

        // POST: Admin/Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,FullName,Email,Phone,Password,Active,RoleId")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", account.RoleId);
            return View(account);
        }

        // GET: Admin/Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", account.RoleId);
            return View(account);
        }

        // POST: Admin/Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,FullName,Email,Phone,Password,Active,RoleId")] Account account)
        {
            if (id != account.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", account.RoleId);
            return View(account);
        }

        // GET: Admin/Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Admin/Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }
    }
}
