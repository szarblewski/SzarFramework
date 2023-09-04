using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzarFramework.SAP.DB.Repositories
{
    public interface IRepositoryBaseUdo<TEntity> where TEntity : class
    {
        TEntity GetByKey(string key, TEntity entity);
        int Add(TEntity entity);
        int Update(string key, TEntity entity);
        int Delete(TEntity entity);

    }
}
