﻿using System.Runtime.Serialization;

namespace TagKid.Lib.Models.DTO.Messages
{
    [DataContract]
    public abstract class Request
    {
        public RequestContext Context { get; set; }
    }
}
