using MetalCollector.WebApi.Models;
using MetalCollector.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace MetalCollector.WebApi.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        public MetalCollectorDbContext DatabaseContext { get; set; }
        public BaseRepository(MetalCollectorDbContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public virtual IQueryable<T> GetAll()
        {
            var entitySet = DatabaseContext.Set<T>();
            return entitySet.AsQueryable();
        }
        public virtual T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public virtual Task<T> GetFirst(Expression<Func<T, bool>> predicate)
        {
            return GetAll().FirstOrDefaultAsync(predicate);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public virtual IQueryable<T> FindByAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            return GetAll().AsNoTracking().Where(predicate);
        }

        public virtual async Task Add(T entity)
        {
            var UpdatedAt = entity.GetType().GetProperty("UpdatedAt");
            if (UpdatedAt != null) entity.GetType().GetProperty("UpdatedAt").SetValue(entity, DateTime.UtcNow);

            var CreatedAt = entity.GetType().GetProperty("CreatedAt");
            if (CreatedAt != null) entity.GetType().GetProperty("CreatedAt").SetValue(entity, DateTime.UtcNow);

            await DatabaseContext.AddAsync(entity);
            DatabaseContext.Entry(entity).State = EntityState.Added;
            await DatabaseContext.SaveChangesAsync();
        }

        public async Task AddRange(List<T> entity)
        {
            entity = entity.Select(x =>
            {
                if (x.GetType().GetProperty("UpdatedAt") != null) x.GetType().GetProperty("UpdatedAt").SetValue(x, DateTime.UtcNow);
                if (x.GetType().GetProperty("CreatedAt") != null) x.GetType().GetProperty("CreatedAt").SetValue(x, DateTime.UtcNow);
                return x;
            }).ToList();

            DatabaseContext.AddRange(entity);
            await DatabaseContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            DatabaseContext.Remove(entity);
            await DatabaseContext.SaveChangesAsync();
        }

        public async Task DeleteRange(List<T> entity)
        {
            DatabaseContext.RemoveRange(entity);
            await DatabaseContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            var UpdatedAt = entity.GetType().GetProperty("UpdatedAt");
            if (UpdatedAt != null) entity.GetType().GetProperty("UpdatedAt").SetValue(entity, DateTime.UtcNow);
            //DatabaseContext.Entry(entity).State = EntityState.Modified;

            DatabaseContext.Update(entity);
            await DatabaseContext.SaveChangesAsync();
        }

        public async Task UpdateRange(List<T> entity)
        {
            entity = entity.Select(x =>
            {
                if (x.GetType().GetProperty("UpdatedAt") != null) x.GetType().GetProperty("UpdatedAt").SetValue(x, DateTime.UtcNow);
                return x;
            }).ToList();

            DatabaseContext.UpdateRange(entity);
            await DatabaseContext.SaveChangesAsync();
        }

        public Task<T> MapperUpdate(T fromDB, T fromRequest)
        {
            // copy fields
            var typeOfSender = fromRequest.GetType();
            var typeOfReceiver = fromDB.GetType();
            foreach (var fieldOfReceiver in typeOfSender.GetFields())
            {
                var fieldOfB = typeOfReceiver.GetField(fieldOfReceiver.Name);
                fieldOfB.SetValue(fromDB, fieldOfReceiver.GetValue(fromRequest));
            }
            // copy properties
            foreach (var propertyOfReceiver in typeOfSender.GetProperties())
            {
                var propertyOfB = typeOfReceiver.GetProperty(propertyOfReceiver.Name);
                propertyOfB.SetValue(fromDB, propertyOfReceiver.GetValue(fromRequest));
            }
            return Task.FromResult(fromDB);
        }

        public async Task Patch(T entity)
        {
            var entry = DatabaseContext.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                // Assuming that the entity has an Id property
                var key = entity.GetType().GetProperty("Id").GetValue(entity, null);
                var originalEntity = await DatabaseContext.Set<T>().FindAsync(key);

                // Attach the original entity and set values from the incoming entity
                entry = DatabaseContext.Entry(originalEntity);
                entry.CurrentValues.SetValues(entity);
            }

            // Update the UpdatedAt property if it exists
            var updatedAtProperty = entity.GetType().GetProperty("UpdatedAt");
            if (updatedAtProperty != null)
            {
                updatedAtProperty.SetValue(entity, DateTime.UtcNow);
                entry.Property("UpdatedAt").IsModified = true;
            }

            // Get a list of properties that have been modified
            var changedProperties = entry.Properties
                .Where(p => p.IsModified)
                .Select(p => p.Metadata.Name);

            // Ensure that only the changed properties will be updated
            foreach (var name in changedProperties)
            {
                entry.Property(name).IsModified = true;
            }

            // Save changes
            await DatabaseContext.SaveChangesAsync();
        }

        public async Task PatchRange(List<T> entities)
        {
            foreach (var entity in entities)
            {
                var entry = DatabaseContext.Entry(entity);

                if (entry.State == EntityState.Detached)
                {
                    // Assuming that the entity has an Id property
                    var key = entity.GetType().GetProperty("Id").GetValue(entity, null);
                    var originalEntity = await DatabaseContext.Set<T>().FindAsync(key);

                    // Attach the original entity and set values from the incoming entity
                    entry = DatabaseContext.Entry(originalEntity);
                    entry.CurrentValues.SetValues(entity);
                }

                // Update the UpdatedAt property if it exists
                var updatedAtProperty = entity.GetType().GetProperty("UpdatedAt");
                if (updatedAtProperty != null)
                {
                    updatedAtProperty.SetValue(entity, DateTime.UtcNow);
                    entry.Property("UpdatedAt").IsModified = true;
                }

                // Get a list of properties that have been modified
                var changedProperties = entry.Properties
                    .Where(p => p.IsModified)
                    .Select(p => p.Metadata.Name);

                // Ensure that only the changed properties will be updated
                foreach (var name in changedProperties)
                {
                    entry.Property(name).IsModified = true;
                }
            }

            // Save changes
            await DatabaseContext.SaveChangesAsync();
        }


    }
}
