using Taga.Core.IoC;
using Taga.Core.Mapping;
using TagKid.Core.Models.DTO;
using TagKid.Core.Models.DTO.Messages;
using TagKid.Core.Models.Database;

namespace TagKid.Application.Bootstrapping.Bootstrappers
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