using System.Linq.Expressions;
using TaskAndTeamManagement.Core.Interface.ISpecification;

namespace TaskAndTeamManagement.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {

        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } =
            new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        public Expression<Func<T, object>> ThenOrderByDescending { get; private set; }

        public Expression<Func<T, object>> ThenOrderByDescendingSecond { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }
        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }

        protected void AddThenOrderByDescending(Expression<Func<T, object>> thenOrderByDescExpression)
        {
            ThenOrderByDescending = thenOrderByDescExpression;
        }
        protected void AddThenOrderByDescendingSecond(Expression<Func<T, object>> thenOrderByDescExpression)
        {
            ThenOrderByDescendingSecond = thenOrderByDescExpression;
        }
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}
