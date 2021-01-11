using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Providers
{
    public class DataProvider<TEntity> : IDataProvider<TEntity>
    {
        private IDataProvider<TEntity> Provider { get; set; }

        public DataProvider(IDataProvider<TEntity> data)
        {
            Provider = data;
        }

        public void Serialize(TEntity data, string nameOfFile)
        {
            Provider.Serialize(data, nameOfFile);
        }

        public TEntity Deserialize(string nameOfFile)
        {
            return Provider.Deserialize(nameOfFile);
        }
    }
}
