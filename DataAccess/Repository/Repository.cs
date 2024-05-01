using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Microsoft.EntityFrameworkCore;
namespace Bulky.DataAccess.IRepository;

public class Repository<T> : IRepository<T> where T : class

{
	private readonly ApplicationDbContext _db;
	internal DbSet<T> DbSet;
    public Repository(ApplicationDbContext db)
    {
		_db = db;
		this.DbSet = _db.Set<T>();
        
    }
    public void Add(T entity)
	{
		DbSet.Add(entity);
	}

	public T Get(Expression<Func<T, bool>> filter)
	{
		IQueryable<T> query = DbSet;
		query.Where(filter);
		return query.FirstOrDefault();
	}

	public IEnumerable<T> GetAll()
	{
		IQueryable<T> query = DbSet;
		return query.ToList();
	}

	public void Remove(T entity)
	{
		DbSet.Remove(entity);
	}

	public void RemoveRange(IEnumerable<T> entities)
	{
		DbSet.RemoveRange(entities);
	}
}
