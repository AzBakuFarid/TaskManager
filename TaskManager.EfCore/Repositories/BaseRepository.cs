using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Domain.DataAccessAbstractions;
using TaskManager.Domain.Interfaces;
using TaskManager.EfCore.Data;
using TaskManager.EfCore.Extensions;

namespace TaskManager.EfCore.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly AppDbContext _dbContext;
        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public virtual void Create<TDbEntity>(TDbEntity model) where TDbEntity : class
        {
            if (model != null)
            {
                _dbContext.Set<TDbEntity>().Add(model);
            }
        }

        public virtual void Update<TDbEntity>(TDbEntity model) where TDbEntity : class
        {
            if (model != null)
            {
                var entity = _dbContext.Set<TDbEntity>().Attach(model);
                entity.State = EntityState.Modified;
            }
        }
        public virtual List<TDbEntity> List<TDbEntity>(params string[] relations) where TDbEntity : class
        {
            return _dbContext.Set<TDbEntity>().AddInclude(relations).ToList();
        }
        public virtual TDbEntity FindById<TDbEntity, TKeyType>(TKeyType id, params string[] relations) where TDbEntity : class, IPrimaryKey<TKeyType>
        {
            return _dbContext.Set<TDbEntity>().AddInclude(relations).FirstOrDefault(f => f.Id.Equals(id));
        }

        public virtual void Delete<TDbEntity>(TDbEntity model) where TDbEntity : class
        {
            if (model != null)
            {
                _dbContext.Remove(model);
            }
        }
    }
}
