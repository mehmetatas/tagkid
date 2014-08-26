using Taga.Core.DynamicProxy;
using TagKid.Lib.Entities;
using TagKid.Lib.Repository;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Service
{
    [Intercept]
    public class TagKidService
    {
        public virtual void RegisterUser(User user)
        {
            Check(user);

            user.Status = UserStatus.Passive;

            using (var db = new Db(false))
            {
                db.Save(user);
            }
        }

        private static void Check(User tagger)
        {
            Validate.StringLength("Username", tagger.Username, 30, 3);
            Validate.StringLength("Fullname", tagger.FullName, 30, 3);
        }
    }
}
