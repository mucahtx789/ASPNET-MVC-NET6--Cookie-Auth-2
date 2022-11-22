using ASPNET_MVC_NET6__Cookie_Auth_2.Entities;
using ASPNET_MVC_NET6__Cookie_Auth_2.Helpers;
using ASPNET_MVC_NET6__Cookie_Auth_2.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Runtime.ConstrainedExecution;

namespace ASPNET_MVC_NET6__Cookie_Auth_2.Controllers
{
    [Authorize(Roles = "admin")]
    public class MemberController : Controller
{
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        private readonly IHasher _Hasher;
        public MemberController(DatabaseContext databaseContext, IMapper mapper, IHasher hasher)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
            _Hasher = hasher;
        }
        public IActionResult Index()
    {
        return View();
    }
        public IActionResult MemberListPartial()
        {
            List<User> users = _databaseContext.Users.ToList();
            List<UserModel> model = users.Select(x => _mapper.Map<UserModel>(x)).ToList();
            return PartialView("_MemberListPartial", model);
        }

        public IActionResult AddNewUserPartial()
        {
           
            return PartialView("_AddNewUserPartial",new CreateUserModel());
        }

        [HttpPost]
        public IActionResult AddNewUser(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {

                if (_databaseContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exist.");
                    return PartialView("_AddNewUserPartial", model);
                }

                User user = _mapper.Map<User>(model);
                user.Password = _Hasher.DoMD5HashedString(model.Password);
                _databaseContext.Users.Add(user);
                _databaseContext.SaveChanges();
                return PartialView("_AddNewUserPartial",new CreateUserModel {Done="user added." } );
            }

            return PartialView("_AddNewUserPartial", model);
        }
        public IActionResult EditUserPartial(Guid id)
        {
            User user = _databaseContext.Users.Find(id);
            EditViewModel model = _mapper.Map<EditViewModel>(user);
            

            return PartialView("_EditUserPartial", model);
        }
        [HttpPost]
        public IActionResult EditUser(Guid id, EditViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (_databaseContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exist.");
                    return PartialView("_EditUserPartial", model);
                }

                User user = _databaseContext.Users.Find(id);
                _mapper.Map(model, user);
                _databaseContext.SaveChanges();

                return PartialView("_EditUserPartial", new EditViewModel { Done = "user updated." });
            }

            return PartialView("_EditUserPartial", model);
        }

        public IActionResult DeleteUser(Guid id)
        {
            User user = _databaseContext.Users.Find(id);
            if (user != null)
            {
                _databaseContext.Users.Remove(user);
                _databaseContext.SaveChanges();
            }

            return MemberListPartial();
        }
    }
}
