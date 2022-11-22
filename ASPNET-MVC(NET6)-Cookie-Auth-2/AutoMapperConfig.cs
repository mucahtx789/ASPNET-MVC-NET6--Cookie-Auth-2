using ASPNET_MVC_NET6__Cookie_Auth_2.Entities;
using ASPNET_MVC_NET6__Cookie_Auth_2.Models;
using AutoMapper;

namespace ASPNET_MVC_NET6__Cookie_Auth_2
{
    public class AutoMapperConfig:Profile
{
		public AutoMapperConfig()
		{
			CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, CreateUserModel>().ReverseMap();
            CreateMap<User, EditViewModel>().ReverseMap();
            CreateMap<uretim, DepartmanViewModel>().ReverseMap();
            CreateMap<bilgiislem, DepartmanViewModel>().ReverseMap();
            CreateMap<insankaynaklari, DepartmanViewModel>().ReverseMap();
            CreateMap<User, DepartmanViewModel>().ReverseMap();

        }
}
}
