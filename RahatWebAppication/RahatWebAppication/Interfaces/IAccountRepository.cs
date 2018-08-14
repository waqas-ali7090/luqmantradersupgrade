using RahatWebAppication.Models;

namespace RahatWebAppication.Interfaces
{
    public interface IAccountRepository
    {
        bool LoginUser(User user);
    }
}
