using System;
using System.Collections.Generic;
using System.Text;

namespace UsefulCrypter.Realizations
{
    /// <summary>
    /// Симметричный шифратор
    /// </summary>
    public class SymCrypter : ICrypter<string[]>, IEncrypter<string[]>
    {
        private readonly Action[] actions;
        void ICrypter<string[]>.Crypt(SerializedMessage<string[]> message)
        {
            foreach (var item in message.elementsValue)
            {
                for (int i = 0; i < actions.Length; i++)
                {
                    actions[i].Execute(item.Value);
                }
            }
        }
        void IEncrypter<string[]>.Encrypt(SerializedMessage<string[]> message)
        {
            foreach (var item in message.elementsValue)
            {
                for (int i = actions.Length - 1; i > -1; i--)
                {
                    actions[i].Execute(item.Value);
                }
            }
        }

        public SymCrypter(Action[] actions)
        {
            this.actions = actions;
        }
    }
    public abstract class Action
    {
        public abstract void Execute(string[] array);
        public abstract void Undo(string[] array);
    }
}
