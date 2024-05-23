namespace TDDLesson;

public interface IRepository
{
    public Task<T> GetAsync<TId, T>(TId id);
    public Task SaveAsync<T>(T item);
}