using NHibernate;

namespace TagKid.Lib.NHibernate
{
    interface INHUnitOfWork
    {
        ISession Session { get; }
    }
}
