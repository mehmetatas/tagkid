using Taga.Core.IoC;
using Taga.Core.Mapping;
using TagKid.Lib.Models.DTO;
using TagKid.Lib.Models.DTO.Messages;
using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Utils
{
    public static class MappingExtensions
    {
        #region Mapper

        private static readonly IMapper Mapper = ServiceProvider.Provider.GetOrCreate<IMapper>();

        private static T To<T>(object source) where T : class
        {
            return Mapper.Map<T>(source);
        }

        #endregion

        #region User

        public static PublicUserModel ToPublicUserModel(this User entity)
        {
            return To<PublicUserModel>(entity);
        }

        public static User ToUser(this SignUpUserModel model)
        {
            return To<User>(model);
        }

        #endregion

        #region Tag

        public static TagModel ToModel(this Tag entity)
        {
            return To<TagModel>(entity);
        }

        #endregion
    }
}