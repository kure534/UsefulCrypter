using System;
using System.Collections.Generic;
using System.Reflection;
using System.Drawing;

namespace UsefulCrypter.Realizations
{
    public class TextMessage : IMessage<string[]>
    {
        public string header;
        public string text;
        Dictionary<FieldInfo, string[]> IMessage<string[]>.GetElements()
        {
            Dictionary<FieldInfo, string[]> valuePairs = new Dictionary<FieldInfo, string[]>();
            #region Adding elements to the dictionary
            valuePairs.Add(typeof(TextMessage).GetField("header"), StringToArray(header));
            valuePairs.Add(typeof(TextMessage).GetField("text"), StringToArray(text));
            #endregion

            return valuePairs;
        }
        private string[] StringToArray(string str)
        {
            string[] array = new string[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                array[i] = str[i].ToString();
            }

            return array;
        }
    }
    public class PictureMessage : IMessage<string[]>
    {
        Dictionary<FieldInfo, string[]> IMessage<string[]>.GetElements()
        {
            throw new NotImplementedException();
        }
    }
}
