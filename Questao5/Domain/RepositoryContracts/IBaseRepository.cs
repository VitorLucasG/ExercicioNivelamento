namespace Questao5.Domain.RepositoryContracts
{
    public interface IBaseRepository<TEntity>
    {
        bool Add(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        TEntity GetById(string id);
        IEnumerable<TEntity> GetAll();

    }
}
