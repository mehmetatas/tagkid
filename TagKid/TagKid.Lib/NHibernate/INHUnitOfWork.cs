using NHibernate;

namespace TagKid.Lib.NHibernate
{
    internal interface INHUnitOfWork
    {
        ISession Session { get; }
    }
}