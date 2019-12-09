using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;


namespace UsefulCryptor
{
    public interface ICryptor<T>
    {
        IMessage<T> Crypt(IMessage<T> message);
    }
    public interface IEncryptor<T>
    {
        IMessage<T> Encrypt(IMessage<T> message);
    }
    public interface IMessage<T>
    {
        Dictionary<FieldInfo, T> GetElements();
    }
    public class TextMessage<T> : IMessage<T>
    {
        Dictionary<FieldInfo, T> IMessage<T>.GetElements()
        {
            throw new NotImplementedException();
        }
    }
    public class SerializedMessage<T>
    {
        public Type messageType;
        public Dictionary<FieldInfo, T> elementsValue = new Dictionary<FieldInfo, T>();
        private readonly int hash;

        public bool IsCrypted()
        {
            return this.GetHashCode().Equals(hash);
        }
        public SerializedMessage(IMessage<T> message)
        {
            messageType = message.GetType();
            elementsValue = message.GetElements();
            hash = GetHashCode();
        }
        public object Deserialize()
        {
            object obj = messageType.GetConstructor(null).Invoke(null);
            foreach (var item in elementsValue)
            {
                item.Key.SetValue(obj, item.Value);
            }
            return obj;
        }
    }

}