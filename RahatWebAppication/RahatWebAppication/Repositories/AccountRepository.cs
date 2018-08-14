using System.Linq;
using RahatWebAppication.Interfaces;
using RahatWebAppication.Models;

namespace RahatWebAppication.Repositories
{
    public class AccountRepository:IAccountRepository
    {
        private readonly RahatDBEntities db = new RahatDBEntities();
        public bool LoginUser(User user)
        {
            if (db.Users.First(x => x.UserName.Equals(user.UserName) && x.Password.Equals(user.Password)) != null)
            {
                return true;
            }
            return false;
        }
    }
}