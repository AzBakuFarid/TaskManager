using System.Collections.Generic;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Domain.DataAccessAbstractions
{
    public interface IBaseRepository
    {
        void Create<TDbEntity>(TDbEntity model) where TDbEntity : class;
        void Update<TDbEntity>(TDbEntity model) where TDbEntity : class;
        List<TDbEntity> List<TDbEntity>(params string[] relations) where TDbEntity : class;
        TDbEntity FindById<TDbEntity, TKeyType>(TKeyType id, params string[] relations) where TDbEntity : class, IPrimaryKey<TKeyType>;
        void Delete<TDbEntity>(TDbEntity entity) where TDbEntity : class;
        void Commit();
    }
}
