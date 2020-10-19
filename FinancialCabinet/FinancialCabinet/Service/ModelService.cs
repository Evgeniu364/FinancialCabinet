using AutoMapper;
using FinancialCabinet.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialCabinet.Service
{
    public class ModelService<TEntity, TModel, TContext, TMapper> where TEntity: class where TModel: class where TContext: ApiDbContext where TMapper: IMapper
    {

        private readonly TContext context;
        private readonly TMapper mapper;
        private readonly DbSet<TEntity> dbset;

        public ModelService(TContext context, TMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.dbset = context.Set<TEntity>();
        }

        public virtual async Task<TModel> GetAsync(Guid ID)
        {
            TEntity entity = await context.FindAsync<TEntity>(ID);
            TModel model = mapper.Map<TModel>(entity);

            return model;
        }

        public virtual async Task<List<TModel>> GetAllAsync()
        {
            List<TEntity> entityList = await dbset.ToListAsync();
            List<TModel> modelList = mapper.Map<List<TModel>>(entityList);

            return modelList;
        }

        public virtual async Task<int> GetTotalAsync()
        {
            return await dbset.CountAsync();
        }

        public virtual async Task<TModel> InsertAsync(TModel model)
        {
            TEntity entity = mapper.Map<TEntity>(model);
            entity = dbset.Add(entity).Entity;
            model = mapper.Map<TModel>(entity);
            await context.SaveChangesAsync();

            return model;
        }

        public virtual async Task InsertManyAsync(List<TModel> modelList)
        {
            List<TEntity> entityList = mapper.Map<List<TEntity>>(modelList);
            dbset.AddRange(entityList);
            await context.SaveChangesAsync();
        }

        public virtual async Task<TModel> UpdateAsync(TModel model)
        {
            TEntity entity = mapper.Map<TEntity>(model);
            entity = dbset.Update(entity).Entity;
            model = mapper.Map<TModel>(entity);
            await context.SaveChangesAsync();

            return model;
        }

        public virtual async Task DeleteByIdAsync(Guid ID)
        {
            TEntity entity = await context.FindAsync<TEntity>(ID);
            dbset.Remove(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task DeleteAllAsync()
        {
            dbset.RemoveRange(dbset);
            await context.SaveChangesAsync();
        }
    }
}
