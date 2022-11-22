using ASPNET_MVC_NET6__Cookie_Auth_2.Entities;
using ASPNET_MVC_NET6__Cookie_Auth_2.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.Arm;
using System.Xml.Linq;

namespace ASPNET_MVC_NET6__Cookie_Auth_2.Controllers
{
    [Authorize(Roles = "admin")]
    public class Departman : Controller
    {

       
        public DbSet<uretim> Uretims { get; set; }
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
       
        public string connectionString = "Server=HERONDALE\\SQLEXPRESS;Database=WebApplication22DB;Trusted_Connection=true";
        public Departman(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
            
           
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UretimListPartial()
        {
            string sqlquery = "SELECT departman_uretim.Id,Position,Task,Username,FullName FROM departman_uretim INNER JOIN Users ON departman_uretim.Id=Users.Id; ";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sqlquery, connection);
            connection.Open();
            SqlDataReader a = command.ExecuteReader();
            List<DepartmanViewModel> model2 = new List<DepartmanViewModel>();
            while (a.Read())
            {
                var details = new DepartmanViewModel();
                details.FullName = a["FullName"].ToString();
                details.Username = a["Username"].ToString();
                details.Position = a["Position"].ToString();
                details.Task = a["Task"].ToString();
                
                model2.Add(details);
            }
            
            
            //  var a=   _databaseContext.Uretims.FromSqlRaw(sqlquery).ToList<uretim>;
            //  List<User> users = _databaseContext.Users.ToList();


            //    _databaseContext.Database.ExecuteSqlRaw(sqlquery);

            // List<uretim> uretimt = _databaseContext.Uretims.ToList();

            //  List<uretim> uretimt = _databaseContext.Uretims.FromSqlRaw(sqlquery).ToList();

            //  List<DepartmanViewModel> model = uretimt.Select(x => _mapper.Map<DepartmanViewModel>(x)).ToList();

            return PartialView("_DepartmanListPartial", model2);
                
        }
        public IActionResult BilgiislemListPartial()
        {
            string sqlquery = "SELECT departman_bilgisislem.Id,Position,Task,Username,FullName FROM departman_bilgisislem INNER JOIN Users ON departman_bilgisislem.Id=Users.Id; ";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sqlquery, connection);
            connection.Open();
            SqlDataReader a = command.ExecuteReader();
            List<DepartmanViewModel> model2 = new List<DepartmanViewModel>();
            while (a.Read())
            {
                var details = new DepartmanViewModel();
                details.FullName = a["FullName"].ToString();
                details.Username = a["Username"].ToString();
                details.Position = a["Position"].ToString();
                details.Task = a["Task"].ToString();

                model2.Add(details);
            }
            //List<User> users = _databaseContext.Users.ToList();

            //List<bilgiislem> bilgiislemt = _databaseContext.Bilgiislems.ToList();
            //List<DepartmanViewModel> model = users.Select(x => _mapper.Map<DepartmanViewModel>(x)).ToList();
            return PartialView("_DepartmanListPartial", model2);
        }
        public IActionResult InsankaynaklariListPartial()
        {
            string sqlquery = "SELECT departman_insankaynaklari.Id,Position,Task,Username,FullName FROM departman_insankaynaklari INNER JOIN Users ON departman_insankaynaklari.Id=Users.Id; ";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sqlquery, connection);
            connection.Open();
            SqlDataReader a = command.ExecuteReader();
            List<DepartmanViewModel> model2 = new List<DepartmanViewModel>();
            while (a.Read())
            {
                var details = new DepartmanViewModel();
                details.FullName = a["FullName"].ToString();
                details.Username = a["Username"].ToString();
                details.Position = a["Position"].ToString();
                details.Task = a["Task"].ToString();

                model2.Add(details);
            }

            // List<User> users = _databaseContext.Users.ToList();
            //List<insankaynaklari> insankaynaklarit = _databaseContext.Insankaynaklaris.ToList();

            //List<DepartmanViewModel> model = insankaynaklarit.Select(x => _mapper.Map<DepartmanViewModel>(x)).ToList();
            // List<DepartmanViewModel> model2 = users.Select(x => _mapper.Map<DepartmanViewModel>(x)).ToList();
            // model.AddRange(model2);
            return PartialView("_DepartmanListPartial", model2);



        }
    }
}