using Core.Entities;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseClass
    {
        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> GetAllAsync();

        /* Task<T> GetByIdWithSpec(ISpecification<T> spec);

        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);

        Task<int> CountAsync(ISpecification<T> spec); */

        Task<int> Add(T entity);

        Task<int> Update(T entity);

        void AddEntity(T Entity);

        void UpdateEntity(T Entity);

        void DeleteEntity(T Entity);
    }
}