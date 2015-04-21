namespace TagKid.Framework.IoC
{
    public enum DependencyScope
    {
        Transient,
        PerThread,
        PerWebRequest,
        Singleton
    }
}