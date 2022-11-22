using ASPNET_MVC_NET6__Cookie_Auth_2.Entities;
using ASPNET_MVC_NET6__Cookie_Auth_2.Helpers;
using ASPNET_MVC_NET6__Cookie_Auth_2.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_MVC_NET6__Cookie_Auth_2.Controllers
{
    public class UserController : Controller
{
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        private readonly IHasher _Hasher;
        public UserController(DatabaseContext databaseContext, IMapper mapper, IHasher hasher)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
            _Hasher = hasher;
        }

        public IActionResult Index()
    {
            /*  List<User> users = _databaseContext.Users.ToList();
            List<UserModel> model=new List<UserModel>();
            foreach (User user in users)
              {
                  model.Add(new UserModel
                  {
                      FullName = user.FullName,
                      Id = user.Id
                  });
              }1.yöntem*/
            /*_databaseContext.Users.Select(x => new UserModel { Id = x.Id, FullName = x.FullName }).ToList(); 2.yöntem*/

             List<User> users = _databaseContext.Users.ToList();
              List<UserModel> model = users.Select(x => _mapper.Map<UserModel>(x)).ToList();
           // List<UserModel> users = _databaseContext.Users.ToList().Select(x => _mapper.Map<UserModel>(x)).ToList();
            return View(model);
    }
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {

                if (_databaseContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exist.");
                    return View(model);//veritabanında aynı kullanıcı var mı
                }

                User user = _mapper.Map<User>(model);
                user.Password = _Hasher.DoMD5HashedString(model.Password);
                _databaseContext.Users.Add(user);
                _databaseContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
        public IActionResult Edit(Guid id)
        {
            User user = _databaseContext.Users.Find(id);
            EditViewModel model = _mapper.Map<EditViewModel>(user);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Guid id, EditViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (_databaseContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exist.");
                    return View(model);//veritabanında aynı kullanıcı var mı
                }

                User user = _databaseContext.Users.Find(id);
                _mapper.Map(model,user);
                _databaseContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Delete(Guid id)
        {
           

         User user = _databaseContext.Users.Find(id);
            if (user!=null)
            {
                _databaseContext.Users.Remove(user);
                _databaseContext.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
