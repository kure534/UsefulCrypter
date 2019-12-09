using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace UsefulCrypter.Realizations
{
    public class ReplaceAction : Action
    {
        private readonly char[] chars;
        private readonly string[] strings;
        public override void Execute(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < chars.Length; j++)
                {
                    if (array[i] == chars[j].ToString()) array[i] = strings[j];
                }
            }
        }
        public override void Undo(string[] array)
        {
            for (int i = array.Length - 1; i > -1; i--)
            {
                for (int j = strings.Length - 1; j > -1; j--)
                {
                    if (array[i] == strings[j]) array[i] = chars[j].ToString();
                }
            }
        }
        public ReplaceAction(Dictionary<char,string> pairs)
        {
            List<string> values = new List<string>();
            List<char> chars = new List<char>();

            foreach (var item in pairs)
            {
                chars.Add(item.Key);
                values.Add(item.Value);
            }
            this.chars = chars.ToArray();
            this.strings = values.ToArray();
        }
    }
}
