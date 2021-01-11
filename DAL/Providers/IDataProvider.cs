using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Providers
{
    public interface IDataProvider<T> 
    {
        void Serialize(T data, string nameOfFile);
        T Deserialize(string nameOfFile);
    }
}
