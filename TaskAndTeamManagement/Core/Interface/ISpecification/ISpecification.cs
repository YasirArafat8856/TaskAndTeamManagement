﻿using System.Linq.Expressions;

namespace TaskAndTeamManagement.Core.Interface.ISpecification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        Expression<Func<T, object>> ThenOrderByDescending { get; }
        Expression<Func<T, object>> ThenOrderByDescendingSecond { get; }
        int Skip { get; }
        int Take { get; }
        bool IsPagingEnabled { get; }
    }
}
