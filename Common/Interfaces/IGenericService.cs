using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IGenericService<T> where T : class
    {
       T Insert(T entity);
       T Update(T entity);
       void Delete(T entity);
       List<T> GetAll();
       T GetByKey(object key);
       
    }
}
