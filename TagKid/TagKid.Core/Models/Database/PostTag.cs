﻿namespace TagKid.Core.Models.Database
{
    public class PostTag
    {
        public virtual Post Post { get; set; }
        public virtual Tag Tag { get; set; }
    }
}