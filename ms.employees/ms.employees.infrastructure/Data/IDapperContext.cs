using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.employees.infrastructure.Data
{
    public interface IDapperContext
    {
        IDbConnection Connection { get; }
        T Transation<T>(Func<IDbTransaction,T> query);
        IDbTransaction BeginTransaction();
        void Commit();
        void Rollback();
        void Dispose();

    }
}
