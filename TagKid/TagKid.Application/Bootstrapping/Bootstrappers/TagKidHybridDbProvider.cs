using System;
using System.Data;
using Taga.Core.Repository.Hybrid;

namespace TagKid.Application.Bootstrapping.Bootstrappers
{
    public class TagKidHybridDbProvider : IHybridDbProvider
    {
        public char ParameterPrefix
        {
            get { return '@'; }
        }

        public object Insert(Type type, IDbCommand cmd, bool selectId)
        {
            if (selectId)
            {
                cmd.CommandText += "; SELECT SCOPE_IDENTITY();";
            }
            return cmd.ExecuteScalar();
        }
    }
}