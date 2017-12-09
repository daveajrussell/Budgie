﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budgie.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Budgie.Data
{
    public class EFRepository<T> : IRepository<T> where T : class
	{
		protected DbContext DbContext { get; set; }

		protected DbSet<T> DbSet { get; set; }

		public EFRepository(DbContext dbContext)
		{
			DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			DbSet = DbContext.Set<T>();
		}

		public virtual IQueryable<T> GetAll()
		{
			return DbSet;
		}

		public virtual void Add(T entity)
		{
			EntityEntry dbEntityEntry = DbContext.Entry(entity);

			if (dbEntityEntry.State != EntityState.Detached)
			{
				dbEntityEntry.State = EntityState.Added;
			}
			else
			{
				DbSet.Add(entity);
			}
		}

		public virtual async Task AddRangeAsync(IEnumerable<T> entities)
		{
			foreach (var entity in entities)
			{
				EntityEntry dbEntityEntry = DbContext.Entry(entity);

				if (dbEntityEntry.State != EntityState.Detached)
				{
					dbEntityEntry.State = EntityState.Added;
				}
			}

			await DbSet.AddRangeAsync(entities);
		}

		public virtual void Update(T entity)
		{
			EntityEntry dbEntityEntry = DbContext.Entry(entity);

			if (dbEntityEntry.State == EntityState.Detached)
			{
				DbSet.Attach(entity);
			}

			dbEntityEntry.State = EntityState.Modified;
		}

		public virtual void Delete(T entity)
		{
			EntityEntry dbEntityEntry = DbContext.Entry(entity);

			if (dbEntityEntry.State != EntityState.Deleted)
			{
				dbEntityEntry.State = EntityState.Deleted;
			}
			else
			{
				DbSet.Attach(entity);
				DbSet.Remove(entity);
			}
		}

		public virtual T GetById(int id)
		{
			return DbSet.Find(id);
		}

		public virtual void Delete(int id)
		{
			var entity = GetById(id);

			if (entity == null)
				return;

			Delete(entity);
		}

		public virtual async Task<T> GetByIdAsync(int id)
		{
			return await DbSet.FindAsync(id);
		}

		public virtual async Task AddAsync(T entity)
		{
			var dbEntityEntry = DbContext.Entry(entity);

			if (dbEntityEntry.State != EntityState.Detached)
			{
				dbEntityEntry.State = EntityState.Added;
			}
			else
			{
				await DbSet.AddAsync(entity);
			}
		}
	}
}