using Taga.Core.IoC;
using Taga.Core.Mapping;
using TagKid.Lib.Models.DTO;
using TagKid.Lib.Models.DTO.Messages;
using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Bootstrapping.Bootstrappers
{
    public class MappingBootstrapper : IBootstrapper
    {
        public void Bootstrap(IServiceProvider prov)
        {
            var mapper = prov.GetOrCreate<IMapper>();

            mapper.Register<User, PublicUserModel>();
            mapper.Register<SignUpUserModel, User>();

            mapper.Register<Tag, TagModel>();
        }
    }
}