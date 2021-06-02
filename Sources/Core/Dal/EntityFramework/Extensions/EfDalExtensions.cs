﻿using System;
using Core.Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Z.EntityFramework.Extensions;

namespace Core.Dal.EntityFramework.Extensions
{
    public static class EfDalExtensions
    {
        public static IServiceCollection AddDbContextFactory<TContext>(this IServiceCollection services, string tag, Action<DbContextOptionsBuilder>? optionAction)
            where TContext : DbContext
        {
            EntityFrameworkManager.ContextFactory = context => context;

            DbContextOptions<TContext> options = CreateDbContextOptions<TContext>(optionAction);
            var factory = new DbContextFactory<TContext>(tag, () => (TContext)Activator.CreateInstance(typeof(TContext), options));
            return services.AddSingleton<IDbContextFactory>(factory);
        }

        public static IServiceCollection AddRepository<TService, TImplementation>(this IServiceCollection services)
            where TService : IRepository
            where TImplementation : EfRepository, TService
        {
            static EfRepositoryFactory<TImplementation> Factory(IServiceProvider provider) =>
                context => ActivatorUtilities.CreateInstance<TImplementation>(provider, context);

            return services.AddSingleton<EfRepositoryFactory<TService>>(Factory);
        }

        private static DbContextOptions<TContext> CreateDbContextOptions<TContext>(
            Action<DbContextOptionsBuilder>? optionsAction)
            where TContext : DbContext
        {
            var builder = new DbContextOptionsBuilder<TContext>(new DbContextOptions<TContext>());
            optionsAction?.Invoke(builder);

            return builder.Options;
        }
    }
}
