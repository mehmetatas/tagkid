using System;
using TagKid.Framework.Repository;

namespace TagKid.Core.Models.Database
{
    public class Login : IEntity
    {
        public virtual long Id { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Username { get; set; }
        public virtual string Email { get; set; }
        public virtual string FacebookId { get; set; }
        public virtual LoginType Type { get; set; }
        public virtual LoginResult Result { get; set; }
    }
}