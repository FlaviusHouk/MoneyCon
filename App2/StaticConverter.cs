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

namespace App2
{
    static class Conventer
    {
        public static byte[] GetNums(char[] arr)
        {
            byte[] toRet = new byte[arr.Length * sizeof(char)];
            for (int i = 0; i < toRet.Length; i = i + 2)
            {
                toRet[i] = Rest(arr[i / 2], out toRet[i + 1]);
            }
            return toRet;
        }
        public static byte Rest(char c, out byte coef)
        {
            int i = (int)c;
            for (int j = 1; ; j++)
            {
                if (i / j * byte.MaxValue < 1)
                {
                    coef = (byte)(j - 1);
                    return (byte)(i - (j - 1) * byte.MaxValue);
                }
            }
        }
        public static byte[] BackToBytes(string str)
        {
            string sub = String.Empty;
            System.Collections.Generic.List<byte> byteCollection = new System.Collections.Generic.List<byte>();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ' && sub != String.Empty)
                {
                    byteCollection.Add(byte.Parse(sub));
                    sub = String.Empty;
                }
                else
                {
                    sub += str[i];
                }
            }
            return byteCollection.ToArray();
        }
        public static string BytesToString(byte[] arr)
        {
            string toRet = String.Empty;
            for (int i = 0; i < arr.Length; i++)
            {
                toRet += arr[i].ToString();
                toRet += ' ';
            }
            return toRet;
        }
    }
}
