﻿using System;

namespace TagKid.Lib.Models.Entities
{
    public class Login
    {
        public virtual long Id { get; set; }

        public virtual long UserId { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual string Username { get; set; }

        public virtual string Email { get; set; }

        public virtual string FacebookId { get; set; }

        public virtual LoginType Type { get; set; }

        public virtual LoginResult Result { get; set; }
    }
}
