using ASPNET_MVC_NET6__Cookie_Auth_2.Entities;
using ASPNET_MVC_NET6__Cookie_Auth_2.Helpers;
using ASPNET_MVC_NET6__Cookie_Auth_2.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NETCore.Encrypt.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Claims;

namespace ASPNET_MVC_NET6__Cookie_Auth_2.Controllers
{
    [Authorize]//tüm actionlarda oturum açık mı kontrol
    public class AccountController : Controller
    {
        private readonly DatabaseContext _databaseContext;//Database bağlantısı için program.cs
        private readonly IConfiguration _configuration;//MD5Salt bilgisi almak için program.cs
        private readonly IHasher _Hasher;
        public AccountController(DatabaseContext databaseContext, IConfiguration configuration, IHasher hasher)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
            _Hasher = hasher;
        }
        [AllowAnonymous]//bu hariç (oturum açık değilsede action)
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]//bu hariç (oturum açık değilsede action)
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword =_Hasher.DoMD5HashedString(model.Password);

                User user = _databaseContext.Users.SingleOrDefault(x => x.Username.ToLower() == model.Username.ToLower()
                && x.Password == hashedPassword);

                if (user != null)
                {
                    if (user.Locked)
                    {
                        ModelState.AddModelError(nameof(model.Username), "User is locked.");
                        return View(model);
                    }
                    //Cookie list
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, user.FullName ?? String.Empty));//boş olabilir
                    claims.Add(new Claim(ClaimTypes.Role, user.Role));
                    claims.Add(new Claim("Username", user.Username));

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect.");
                }
            }
            return View(model);
        }

        private string DoMD5HashedString(string s)
        {
            //helpers'a taşındı member,user controllerda kullanılacak.
            string md5Salt = _configuration.GetValue<String>("AppSettings:MD5Salt");
            string salted = s + md5Salt;
            string hashed = salted.MD5();
            return hashed;
        }

        [AllowAnonymous]//bu hariç (oturum açık değilsede action)
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]//bu hariç (oturum açık değilsede action)
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_databaseContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exist.");
                    return View(model);//veritabanında aynı kullanıcı var mı
                }



                string hashedPassword = _Hasher.DoMD5HashedString(model.Password);
                User user = new User()
                {
                    Username = model.Username,
                    Password = hashedPassword
                };
                _databaseContext.Users.Add(user);
                int affectedRowCount = _databaseContext.SaveChanges();//kayıt sayısı
                if (affectedRowCount == 0)
                {
                    //ilk tırnağa propety yazıp username,pass altına yazıdırılabilir
                    ModelState.AddModelError("", "User can not be added.");//boşsa genel hata
                }
                else
                {
                    return RedirectToAction(nameof(Login));//logine geç
                }
            }
            return View(model);
        }

        public IActionResult Profile()
        {
            ProfileInfoLoader();
            return View();
        }

        private void ProfileInfoLoader()
        {
            Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = _databaseContext.Users.SingleOrDefault(x => x.Id == userid);
            ViewData["Fullname"] = user.FullName;
            ViewData["ProfileImage"] = user.ProfileImageFileNames;
        }

        [HttpPost]
        public IActionResult ProfileChangeFullName([Required][StringLength(50)] string? fullname)
        {
            if (ModelState.IsValid)
            {
                Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _databaseContext.Users.SingleOrDefault(x => x.Id == userid);
                user.FullName = fullname;
                _databaseContext.SaveChanges();
                return RedirectToAction(nameof(Profile));
            }
            ProfileInfoLoader();//isim kayıtdan sonra varsa hata göstersin isim değişmediğini göstersin
            return View("Profile");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        public IActionResult ProfileChangePassword([Required][MinLength(6)][MaxLength(16)] string password)
        {
            if (ModelState.IsValid)
            {
                Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _databaseContext.Users.SingleOrDefault(x => x.Id == userid);

                string hashedpassword = _Hasher.DoMD5HashedString(password);
                user.Password = hashedpassword;
                _databaseContext.SaveChanges();
                ViewData["Result"] = "Password changed";

            }
            ProfileInfoLoader();//isim kayıtdan sonra varsa hata göstersin isim değişmediğini göstersin
            return View("Profile");
        }

        [HttpPost]
        public IActionResult ProfileChangeImage([Required]IFormFile file)
        {
            if (ModelState.IsValid)
            {
                Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _databaseContext.Users.SingleOrDefault(x => x.Id == userid);
                string filename = $"p_{userid}.jpg";
                
                Stream stream =new FileStream($"wwwroot/uploads/{filename}",FileMode.OpenOrCreate);
                file.CopyTo(stream);
                stream.Close();
                stream.Dispose();
                user.ProfileImageFileNames = filename;
                _databaseContext.SaveChanges();
               
                return RedirectToAction(nameof(Profile));
            }
            ProfileInfoLoader();//isim kayıtdan sonra varsa hata göstersin isim değişmediğini göstersin
            return View("Profile");
        }
    }
}
