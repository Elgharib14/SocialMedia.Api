using SocialMedia.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Specification
{
    public interface ISpecification<T> where T : BaseEntity
    {
        public List<Expression<Func<T, object>>> Includes { set; get; }
        public Expression<Func<T, bool>> Criteria { get; set; }
    }
}
