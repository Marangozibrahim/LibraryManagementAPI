﻿using Library.Application.Common.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.Specifications
{
    internal static class SpecificationQueryBuilder
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, ISpecification<T> spec)
        where T : class
        {
            var query = inputQuery;

            if (spec.AsNoTracking)
                query = query.AsNoTracking();

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDescending is not null)
                query = query.OrderByDescending(spec.OrderByDescending);

            if (spec.Skip.HasValue)
                query = query.Skip(spec.Skip.Value);

            if (spec.Take.HasValue)
                query = query.Take(spec.Take.Value);

            return query;
        }
    }
}
