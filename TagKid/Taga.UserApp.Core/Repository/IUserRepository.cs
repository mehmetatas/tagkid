using Taga.UserApp.Core.Model.Business;

namespace Taga.UserApp.Core.Repository
{
    public interface IUserRepository : IUserAppRepository
    {
        void Save(UserModel model);

        UserModel Get(long id, bool selectRoles = false, bool selectCategories = false);
        
        void Delete(UserModel model);
    }
}
