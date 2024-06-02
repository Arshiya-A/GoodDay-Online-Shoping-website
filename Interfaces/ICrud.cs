using System.Linq.Expressions;

namespace crud;

public interface ICrud<TEntity> where TEntity : class
{
  public IEnumerable<TEntity> GetAll();
   public IEnumerable<TEntity> Search(Expression<Func<TEntity,bool>> where);
  public TEntity GetById(int id);
  public void Insert(TEntity entity);
  public bool Update(TEntity entity);
  public bool Delete(TEntity entity);
  public bool Delete(int id);
  public void SaveChanges();
}
