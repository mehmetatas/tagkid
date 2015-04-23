﻿using System;

namespace TagKid.Framework.Models.Database
{
    public class User : IEntity
    {
        public virtual long Id { get; set; }
        public virtual string Fullname { get; set; }
        public virtual string Email { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime JoinDate { get; set; }
        public virtual string FacebookId { get; set; }
        public virtual UserType Type { get; set; }
        public virtual UserStatus Status { get; set; }
    }
}