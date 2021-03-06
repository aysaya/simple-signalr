﻿using Infrastructure.Common;
using Pricing.DomainModel;
using System;
using System.Threading.Tasks;

namespace Pricing.ResourceAccessors
{
    public interface ICommandRA<T>
    {
        Task<T> SaveAsync(T t);
    }
    public interface IQueryRA<T>
    {
        Task<T[]> GetAllAsync();
    }

    public class QuotePersistence : ICommandRA<Quote>, IQueryRA<Quote>
    {
        private readonly IProvideRepository<Quote> repository;

        public QuotePersistence(IProvideRepository<Quote> repository)
        {
            this.repository = repository;
        }

        public async Task<Quote> SaveAsync(Quote quote)
        {
            quote.DateCreated = DateTime.UtcNow;

            return await repository.SaveAsync(quote);
        }

        public async Task<Quote[]> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }        
    }
}
