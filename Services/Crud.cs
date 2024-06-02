
using System.Collections.Generic;
using System.Linq.Expressions;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crud;

public class Crud<TEntity> : ICrud<TEntity> where TEntity : class
{
    private DbSet<TEntity> _table;
    private ShopContext _db;

    public Crud(ShopContext context)
    {
        _db = context;
        _table = _db.Set<TEntity>();
    }
    public IEnumerable<TEntity> GetAll()
    {
        return _table.ToList();
    }

    public TEntity GetById(int id)
    {
        return _table.Find(id);
    }
    public IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> where)
    {
        return _table.Where(where).ToList();
    }

    public void Insert(TEntity entity)
    {
        _table.Add(entity);
    }

    public bool Update(TEntity entity)
    {
        try
        {
            _table.Entry(entity).State = EntityState.Modified;
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool Delete(TEntity entity)
    {
        try
        {
            _table.Remove(entity);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Delete(int id)
    {
        try
        {
            var deleteId = GetById(id);
            Delete(deleteId);
            return true;
        }
        catch
        {

            return false;
        }
    }

    public void SaveChanges()
    {
       _db.SaveChanges();
    }


}
