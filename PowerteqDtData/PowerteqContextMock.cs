﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PowerteqDTModels;

namespace PowerteqDtData
{
	public class PowerteqContextMock : IPowerteqContext
	{
		public PowerteqContextMock()
		{

			this.Locations = new TestDbSet<LocationModel>();
			this.Systems = new TestDbSet<SystemModel>();
			this.Departments = new TestDbSet<DepartmentModel>();
			this.DowntimeEvents = new TestDbSet<DowntimeEventModel>();

			this.Locations.Add(new LocationModel { id = 0, locationName = "mock location" });
            this.Systems.Add(new SystemModel { id = 0, systemName = "mock system" });
            this.Departments.Add(new DepartmentModel { id = 0, departmentName = "mock department", startWorkingHours = 900, endWorkingHours = 1700, workdayMon = true, workdayTue = true });

		}

		public DbSet<LocationModel> Locations { get; set; }

		public DbSet<SystemModel> Systems { get; set; }

		public DbSet<DepartmentModel> Departments { get; set; }

		public DbSet<DowntimeEventModel> DowntimeEvents { get; set; }

		public void SaveChanges()
		{
			//this is an in memory mock so this will probably not do anyting.
		}
	}

	public class TestDbSet<TEntity> : DbSet<TEntity>, IQueryable, IEnumerable<TEntity>, IDbAsyncEnumerable<TEntity>
		where TEntity : class
	{
		ObservableCollection<TEntity> _data;
		IQueryable _query;

		public TestDbSet()
		{
			_data = new ObservableCollection<TEntity>();
			_query = _data.AsQueryable();
		}

		public override TEntity Add(TEntity item)
		{
			_data.Add(item);
			return item;
		}

		public override TEntity Remove(TEntity item)
		{
			_data.Remove(item);
			return item;
		}

		public override TEntity Attach(TEntity item)
		{
			_data.Add(item);
			return item;
		}

		public override TEntity Create()
		{
			return Activator.CreateInstance<TEntity>();
		}

		public override TDerivedEntity Create<TDerivedEntity>()
		{
			return Activator.CreateInstance<TDerivedEntity>();
		}

		public override ObservableCollection<TEntity> Local
		{
			get { return _data; }
		}

		Type IQueryable.ElementType
		{
			get { return _query.ElementType; }
		}

		Expression IQueryable.Expression
		{
			get { return _query.Expression; }
		}

		IQueryProvider IQueryable.Provider
		{
			get { return new TestDbAsyncQueryProvider<TEntity>(_query.Provider); }
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _data.GetEnumerator();
		}

		IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
		{
			return _data.GetEnumerator();
		}

		IDbAsyncEnumerator<TEntity> IDbAsyncEnumerable<TEntity>.GetAsyncEnumerator()
		{
			return new TestDbAsyncEnumerator<TEntity>(_data.GetEnumerator());
		}
	}


	internal class TestDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
	{
		private readonly IQueryProvider _inner;

		internal TestDbAsyncQueryProvider(IQueryProvider inner)
		{
			_inner = inner;
		}

		public IQueryable CreateQuery(Expression expression)
		{
			return new TestDbAsyncEnumerable<TEntity>(expression);
		}

		public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			return new TestDbAsyncEnumerable<TElement>(expression);
		}

		public object Execute(Expression expression)
		{
			return _inner.Execute(expression);
		}

		public TResult Execute<TResult>(Expression expression)
		{
			return _inner.Execute<TResult>(expression);
		}

		public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
		{
			return Task.FromResult(Execute(expression));
		}

		public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
		{
			return Task.FromResult(Execute<TResult>(expression));
		}
	}

	internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
	{
		public TestDbAsyncEnumerable(IEnumerable<T> enumerable)
			: base(enumerable)
		{ }

		public TestDbAsyncEnumerable(Expression expression)
			: base(expression)
		{ }

		public IDbAsyncEnumerator<T> GetAsyncEnumerator()
		{
			return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
		}

		IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
		{
			return GetAsyncEnumerator();
		}

		IQueryProvider IQueryable.Provider
		{
			get { return new TestDbAsyncQueryProvider<T>(this); }
		}
	}

	internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
	{
		private readonly IEnumerator<T> _inner;

		public TestDbAsyncEnumerator(IEnumerator<T> inner)
		{
			_inner = inner;
		}

		public void Dispose()
		{
			_inner.Dispose();
		}

		public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult(_inner.MoveNext());
		}

		public T Current
		{
			get { return _inner.Current; }
		}

		object IDbAsyncEnumerator.Current
		{
			get { return Current; }
		}
	}
}
