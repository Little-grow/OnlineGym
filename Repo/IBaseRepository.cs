namespace OnlineGym.Repo
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> Get(int id);
        void Create(T entity);
        Task<T> Update(int id, T entity);
        Task<T> Delete(int id);
        List<T> GetAll();
    }
}
