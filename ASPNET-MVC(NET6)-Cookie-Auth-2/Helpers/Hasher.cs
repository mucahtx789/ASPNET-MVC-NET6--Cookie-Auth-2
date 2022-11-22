using NETCore.Encrypt.Extensions;

namespace ASPNET_MVC_NET6__Cookie_Auth_2.Helpers
{
    public interface IHasher
    {
        string DoMD5HashedString(string s);
    }

    public class Hasher : IHasher
    {
        private readonly IConfiguration _configuration;

        public Hasher(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string DoMD5HashedString(string s)
        {
            string md5Salt = _configuration.GetValue<String>("AppSettings:MD5Salt");
            string salted = s + md5Salt;
            string hashed = salted.MD5();
            return hashed;
        }
    }
}
