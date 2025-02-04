using ManageRevenue.Domain.Common;
using ManageRevenue.Domain.Models;

namespace ManageRevenue.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Response<UserViewModel>> GetAllUsers();
    }
}
