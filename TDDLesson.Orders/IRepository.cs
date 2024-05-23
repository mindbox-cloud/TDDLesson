namespace TDDLesson;

public interface IRepository
{
    /// <summary>
    /// Загружаем сущность из БД
    /// </summary>
    /// <param name="id"></param>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Task<T> GetAsync<TId, T>(TId id);
    
    /// <summary>
    /// Сохраняем сущность в БД
    /// </summary>
    /// <param name="item"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Task SaveAsync<T>(T item);
}