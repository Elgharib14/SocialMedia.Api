using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entity;
using SocialMedia.Core.Reposatory;
using SocialMedia.Core.Specification;
using SocialMedia.Reposatory.AppContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Reposatory
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private readonly AppDbcontext dbcontext;

        public GenericRepo(AppDbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<T> Creat(T entity)
        {
           await dbcontext.Set<T>().AddAsync(entity);
            await dbcontext.SaveChangesAsync();
            return entity;
        }

        public T Delete(T entity)
        {
            dbcontext.Set<T>().Remove(entity);
             dbcontext.SaveChanges();
            return entity;
           
        }

        public async Task<IEnumerable<T>> GetAll()
        {
           var data = await dbcontext.Set<T>().ToListAsync();
            
            return data;
        }

        public async Task<IEnumerable<T>> GetAllPostsByuserId(ISpecification<T> spec)
        {
           return await ApplySpec(spec).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec)
        {
            return await ApplySpec(spec).ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
          var data = await dbcontext.Set<T>().FirstAsync(x => x.Id == id);
            return data;
        }

        public T Update(T entity)
        {
           dbcontext.Set<T>().Update(entity);
            dbcontext.SaveChanges();
            return entity;
        }



        private IQueryable<T> ApplySpec(ISpecification<T> spec)
        {
            return SpecificationEvelatur<T>.GetQuery(dbcontext.Set<T>(), spec);
        }
    }

}
