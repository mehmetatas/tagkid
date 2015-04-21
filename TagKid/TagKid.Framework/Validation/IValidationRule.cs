namespace TagKid.Framework.Validation
{
    public interface IValidationRule
    {
        bool Execute(object obj);
    }
}
