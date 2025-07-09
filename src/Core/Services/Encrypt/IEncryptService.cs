namespace Core.Services.Encrypt
{
    public interface IEncryptService
    {
        string Decrypt(string cipherText);
        string Encrypt(string plainText);
    }
}