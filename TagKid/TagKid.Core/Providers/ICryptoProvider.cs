namespace TagKid.Core.Providers
{
    public interface ICryptoProvider
    {
        string ComputeHash(string utf8Text);
    }
}
