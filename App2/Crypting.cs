using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

//залишається тільки додати методи, які інкапсулюватимуть шифрування та розшифровку

internal static class PasswordSafe
{
    //поля необхідні для шифрування
    private static string securityCode; //зберігання паролю, який прийде в програму при відриванні й за допомогою якого буде виконано шифрування після завершення
    private static string inpFile; //шлях до вхідного файлу
    private static string outpFile; //шлях до вихідного файлу
    enum TransformType { ENCRYPT = 0, DECRYPT = 1 }; //можливі перетворення

    //вектори ініціалізації необхідні для шифрування
    private static byte[] IndInit;
    private static byte[] Key;

    //зміна паролю
    public static string Code
    {
        set
        {
            securityCode = value;
            PerformVectors();
        }
    }

    //підготовка векторів ініціалізації
    private static void PerformVectors()
    {
        //ініціалізація векторів
        IndInit = new byte[24];
        Key = new byte[16];

        //формування унікального ідентифікатора через Hash-функції
        byte[] transformedStr = Encoding.ASCII.GetBytes(securityCode);
        SHA384Managed sha384 = new SHA384Managed();
        sha384.ComputeHash(transformedStr);
        byte[] rez = sha384.Hash;

        //заповнення векторів
        for (int i = 0; i < 24; i++)
        {
            IndInit[i] = rez[i];
        }
        for (int i = 24; i < 40; i++)
        {
            Key[i] = rez[i];
        }
    }
    private static void Transform(byte[] input, TransformType T)
    {
        CryptoStream cryptStream = null;//потік для шифрування
        RijndaelManaged rijObj = null;//річ, яка забезпечує Rijndael
        ICryptoTransform rijTrans = null;//об'єкт, який буде зашифровано
        FileStream fsIn = null;//потік для вхідного файлу
        FileStream fsOut = null;//потік для вихідного файлу
        MemoryStream memStream = null;//буферний потік

        try
        {
            //створення об'єктів шифрування
            rijObj = new RijndaelManaged();
            rijObj.IV = IndInit;
            rijObj.Key = Key;
            if (T == TransformType.ENCRYPT)
            {
                rijTrans = rijObj.CreateEncryptor();
            }
            else
            {
                rijTrans = rijObj.CreateDecryptor();
            }
            if (inpFile.Length > 0 && outpFile.Length > 0)
            {
                fsIn = new FileStream(inpFile, FileMode.Open, FileAccess.Read);
                fsOut = new FileStream(outpFile, FileMode.OpenOrCreate, FileAccess.Write);

                cryptStream = new CryptoStream(fsOut, rijTrans, CryptoStreamMode.Write);

                int buff = 8192;
                byte[] buffer = new byte[buff];
                int bytesToRead;
                do
                {
                    bytesToRead = fsIn.Read(buffer, 0, buff);
                    cryptStream.Write(buffer, 0, bytesToRead);
                }
                while (bytesToRead != 0);
                cryptStream.FlushFinalBlock();
            }
        }
        catch (CryptographicException)
        {
            throw new CryptographicException(
                "Пароль неправильний, спробуйте ще");
        }
        finally
        {
            if (rijObj != null) rijObj.Clear();
            if (rijTrans != null) rijTrans.Dispose();
            if (cryptStream != null) cryptStream.Close();
            if (memStream != null) memStream.Close();
            if (fsOut != null) fsOut.Close();
            if (fsIn != null) fsIn.Close();
        }
    } 
}