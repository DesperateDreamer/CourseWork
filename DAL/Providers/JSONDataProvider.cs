using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace DAL.Providers
{
    public class JSONDataProvider<T> : IDataProvider<T>
    {
        public void Serialize(T data, string nameOfFile)
        {
            using (FileStream stream = new FileStream(nameOfFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
                try
                {
                    jsonSerializer.WriteObject(stream, data);
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
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
                try
                {
                    deserialData = (T)jsonSerializer.ReadObject(stream);
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
