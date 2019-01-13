
namespace fmCommon
{
    public interface IEncryptable
    {
        string Encrypt(string str);
        string Decrypt(string str);
    }
}
