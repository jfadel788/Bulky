﻿using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		public CategoryRepository CategoryRepository { get; private set; }
		public ProductRepository ProductRepository { get; private set; }
		private readonly ApplicationDbContext _db;
		public UnitOfWork(ApplicationDbContext db) {

			_db = db;
			CategoryRepository = new CategoryRepository(_db);
			ProductRepository = new ProductRepository(_db);
		}

		public void Save()
		{
			_db.SaveChanges();
		}
	}
}
