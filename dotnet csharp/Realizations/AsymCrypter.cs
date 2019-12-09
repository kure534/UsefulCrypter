using System;
using System.Collections.Generic;
using System.Text;

namespace UsefulCrypter.Realizations
{
    public abstract class AsymCrypter<T>
    {
        private ICrypter<T> crypter;
        private IEncrypter<T> encrypter;
    }
}
