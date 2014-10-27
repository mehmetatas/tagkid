using System.Linq;
using Taga.Core.Mapping;
using Taga.Core.Repository;
using Taga.UserApp.Core.Model.Business;
using Taga.UserApp.Core.Model.Database;
using Taga.UserApp.Core.Repository;

namespace Taga.UserApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public UserRepository(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual void Save(UserModel model)
        {
            var entity = _mapper.Map<User>(model);
            _repository.Save(entity);
            model.Id = entity.Id;
        }

        public virtual UserModel Get(long id, bool selectRoles = false, bool selectCategories = false)
        {
            var entity = _repository.Select<User>()
                .FirstOrDefault(u => u.Id == id);

            var model = _mapper.Map<UserModel>(entity);

            if (selectRoles)
            {
                var roles =
                    from userRole in _repository.Select<UserRole>()
                    join role in _repository.Select<Role>()
                    on userRole.RoleId equals role.Id
                    where userRole.UserId == id
                    select _mapper.Map<RoleModel>(role);

                model.Roles = roles.ToList();
            }

            if (selectCategories)
            {
                var categories =
                    from category in _repository.Select<Category>()
                    where category.UserId == id
                    select _mapper.Map<CategoryModel>(category);

                model.Categories = categories.ToList();
            }

            return model;
        }

        public virtual void Delete(UserModel model)
        {
            var entity = _mapper.Map<User>(model);
            _repository.Delete(entity);
        }
    }
}
