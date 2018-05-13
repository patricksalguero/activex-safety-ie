using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace tmicliente
{
    class Util
    {
        public static string fn_ConvertHexToString(string HexValue)
        {
            string text = "";
            while (HexValue.Length > 0)
            {
                text += Convert.ToChar(Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                HexValue = HexValue.Substring(2, HexValue.Length - 2);
            }
            return text;
        }

        public static string fn_ConvertHexToString(string hexInput, Encoding encoding)
        {
            int length = hexInput.Length;
            byte[] array = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                array[i / 2] = Convert.ToByte(hexInput.Substring(i, 2), 16);
            }
            return encoding.GetString(array);
        }

        public static string fn_ConvertHexToString2(string HexValue)
        {
            string text = "";
            while (HexValue.Length > 0)
            {
                text += Convert.ToChar(Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                HexValue = HexValue.Substring(2, HexValue.Length - 2);
            }
            return text;
        }

        public static string fn_HexString2Ascii(string hexString)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i <= hexString.Length - 2; i += 2)
            {
                stringBuilder.Append(Convert.ToString(Convert.ToChar(int.Parse(hexString.Substring(i, 2), NumberStyles.HexNumber))));
            }
            return stringBuilder.ToString();
        }


        public static string ConvertHex(String hexString)
        {
            try
            {
                string ascii = string.Empty;

                for (int i = 0; i < hexString.Length; i += 2)
                {
                    String hs = string.Empty;

                    hs = hexString.Substring(i, 2);
                    uint decval = System.Convert.ToUInt32(hs, 16);
                    char character = System.Convert.ToChar(decval);
                    ascii += character;

                }

                return ascii;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return string.Empty;
        }


        public static string fConvertirString(byte[] bVar)
        {
            string text = Convert.ToBase64String(bVar);
            string text2 = BitConverter.ToString(bVar);
            string hexValue = BitConverter.ToString(bVar).Replace("-", string.Empty);
            string text3 = Util.fn_ConvertHexToString(hexValue);
            string text4 = Util.fn_ConvertHexToString(hexValue).Replace("\u0005", string.Empty);
            string text5 = Util.fn_ConvertHexToString(hexValue).Replace("\0", string.Empty);
            string[] array = text4.Split(".".ToCharArray());
            string[] array2 = text5.Split(".".ToCharArray());
            string[] array3 = text3.Split("\0".ToCharArray());
            string text6 = Util.fn_ConvertHexToString(hexValue).Replace("\0", string.Empty);
            string text7 = Util.fn_ConvertHexToString(hexValue).Replace("\0", "\u0005");
            string text8 = Util.fn_ConvertHexToString(hexValue).Replace("\0", " ");
            text8 = text8.Replace("SOL", "");
            string text9 = text8.Replace("♣", "\t");
            int length = text8.Length;
            int num = 0;
            string str = "";
            for (int i = 0; i < length; i++)
            {
                if (text8[i] == '\u0003')
                {
                    num = i;
                }
                else
                {
                    if (text8[i] == '\u0010')
                    {
                        int num2 = i;
                        str = str + "\r\n" + text8.Substring(num + 1, num2 - num - 1);
                    }
                }
            }
            text8 = text8.Replace(" .\u0005   ", "");
            text8 = text8.Replace("\u0005  ", "");
            text8 = text8.Replace("\u0001", "\r\n");
            text8 = text8.Replace("\b\u0002", "\r\n");
            text8 = text8.Replace("#", "\r\n");
            return text8.Trim();
        }
    }
}
