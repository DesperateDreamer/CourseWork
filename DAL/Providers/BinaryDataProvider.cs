using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DAL.Providers
{
    public class BinaryDataProvider<T> : IDataProvider<T>
    {
        public void Serialize(T data, string nameOfFile)
        {
            using (FileStream stream = new FileStream(nameOfFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(stream, data);
                }
                catch (Exception msg)
                {
                    throw msg;
                }
            }
        }

        public T Deserialize(string nameOfFile)
        {
            T deserialData;
            using (FileStream stream = new FileStream(nameOfFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    deserialData = (T)formatter.Deserialize(stream);
                }
                catch (Exception msg)
                {
                    throw msg;
                }
            }
            return deserialData;
        }
    }
}
