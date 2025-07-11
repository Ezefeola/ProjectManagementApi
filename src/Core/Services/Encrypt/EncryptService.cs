﻿using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Core.Services.Encrypt;
public class EncryptService : IEncryptService
{
    private readonly byte[] _key;
    private readonly byte[] _iv = new byte[16];

    public EncryptService(IConfiguration configuration)
    {
        var encryptionKey = configuration["EncryptionSettings:Key"];

        if (string.IsNullOrEmpty(encryptionKey) || encryptionKey.Length < 32)
        {
            throw new ArgumentException("The password must have at leats 32 characters.");
        }

        _key = Encoding.UTF8.GetBytes(encryptionKey.Substring(0, 32));
    }

    public string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText)) return plainText;

        using var aesAlg = Aes.Create();
        aesAlg.Key = _key;
        aesAlg.IV = _iv;

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        byte[] encrypted;
        using (var msEncrypt = new MemoryStream())
        {
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }

            encrypted = msEncrypt.ToArray();
        }

        return Convert.ToBase64String(encrypted);
    }

    public string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText)) return cipherText;

        using var aesAlg = Aes.Create();
        aesAlg.Key = _key;
        aesAlg.IV = _iv;

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        string decrypted;
        using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        using (var srDecrypt = new StreamReader(csDecrypt))
        {
            decrypted = srDecrypt.ReadToEnd();
        }

        return decrypted;
    }
}