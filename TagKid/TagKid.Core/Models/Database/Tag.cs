﻿using System;

namespace TagKid.Core.Models.Database
{
    public class Tag
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual long Count { get; set; }
        public virtual string Hint { get; set; }
        public virtual string Description { get; set; }
        public virtual TagStatus Status { get; set; }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Hint.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("{0} ({1}): {2}", Name, Hint, Description);
        }
    }
}