using SocialMedia.Core.Entity;
using SocialMedia.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Reposatory
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);
        public Task<IEnumerable<T>> GetAllPostsByuserId(ISpecification<T> spec);
        public Task<T> GetById(int id);
        public Task<T> Creat(T entity);
        public T Delete(T entity);
        public T Update(T entity);
    }
}
