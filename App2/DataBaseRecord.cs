/* Цей файл — частина MoneyCon.

   Moneycon - вільна програма: ви можете повторно її розповсюджувати та/або
   змінювати її на умовах Стандартної суспільної ліцензії GNU в тому вигляді,
   в якому вона була опублікована Фондом вільного програмного забезпечення;
   або третьої версії ліцензії, або (зігдно з вашим вибором) будь-якої наступної
   версії.

   Moneycon розповсюджується з надією, що вона буде корисною,
   але БЕЗ БУДЬ-ЯКИХ ГАРАНТІЙ; навіть без неявної гарантії ТОВАРНОГО ВИГЛЯДУ
   або ПРИДАТНОСТІ ДЛЯ КОНКРЕТНИХ ЦІЛЕЙ. Детальніше див. в Стандартній
   суспільній ліцензії GNU.

   Ви повинні були отримати копію Стандартної суспільної ліцензії GNU
   разом з цією програмою. Якщо це не так, див.
   <http://www.gnu.org/licenses/>.*/

﻿using System;
using System.Security.Cryptography;
using System.IO;


namespace App2
{
    internal class Record
    {
        string str1;
        string str2;
        string str3;
        public string Date
        {
            get
            {
                return str1;
            }
        }
        public string Price
        {
            get
            {
                return str2;
            }
        }
        public string Desc
        {
            get
            {
                return str3;
            }
        }
        byte[] mas;
        public void Init(string str1, string str2, string str3)
        {
            this.str1 = str1;
            this.str2 = str2;
            this.str3 = str3;
        }
        public void EncryptAll()
        {
            using (Aes mycrypt = Aes.Create())
            {
                mycrypt.Key = DataBase.GetPass();
                mycrypt.IV = DataBase.GetIV();
                mas = Encrypt(str1, mycrypt.Key, mycrypt.IV);
                str1 = Conventer.BytesToString(mas);
                mas = Encrypt(str2, mycrypt.Key, mycrypt.IV);
                str2 = Conventer.BytesToString(mas);
                mas = Encrypt(str3, mycrypt.Key, mycrypt.IV);
                str3 = Conventer.BytesToString(mas);
            }
        }
        public void DecryptAll()
        {
            using (Aes mycrypt = Aes.Create())
            {
                mycrypt.Key = DataBase.GetPass();
                mycrypt.IV = DataBase.GetIV();
                if (str1 != "-1")
                    str1 = Decrypt(Conventer.BackToBytes(str1), mycrypt.Key, mycrypt.IV);
                if (str2 != "-1")
                    str2 = Decrypt(Conventer.BackToBytes(str2), mycrypt.Key, mycrypt.IV);
                if (str3 != "-1")
                    str3 = Decrypt(Conventer.BackToBytes(str3), mycrypt.Key, mycrypt.IV);
            }
        }
        static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = String.Empty;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            try
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                            catch (CryptographicException e)
                            {
                                throw e;
                            }
                        }
                    }
                }
            }
            return plaintext;
        }
        static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }
    }
}
