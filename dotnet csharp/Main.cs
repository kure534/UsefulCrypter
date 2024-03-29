﻿using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;


namespace UsefulCrypter
{
    /// <summary>
    /// Общий интерфейс для шифраторов
    /// </summary>
    /// <typeparam name="T">Тип данных в котором будут хранится поля SerializedMessage</typeparam>
    public interface ICrypter<T>
    {
        void Crypt(SerializedMessage<T> message);
    }
    /// <summary>
    /// Общий интерфейс для дешифраторов
    /// </summary>
    /// <typeparam name="T">Тип данных в котором будут хранится поля SerializedMessage</typeparam>
    public interface IEncrypter<T>
    {
        void Encrypt(SerializedMessage<T> message);
    }
    public interface IMessage<T>
    {
        Dictionary<FieldInfo, T> GetElements();
    }
    /// <summary>
    /// Упрощенное представление сообщения в виде словаря поле-значение
    /// </summary>
    /// <typeparam name="T">Тип данных в котором будут хранится поля SerializedMessage</typeparam>
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
        public override string ToString()
        {
            string main = messageType.ToString() + System.Environment.NewLine;
            foreach (var item in elementsValue)
            {
                main += item.Key.Name + " ";

                if (item.Value.GetType() == typeof(System.String[]))
                {
                    string[] k = item.Value as string[];

                    string value = "";
                    foreach (var ve in k)
                    {
                        value += ve;
                    }
                    main += value;
                }//если обобщенный тип является string[]
                else
                {
                    main += item.Value.ToString();
                }
                main += System.Environment.NewLine;
            }
            return main;
        }
    }
}