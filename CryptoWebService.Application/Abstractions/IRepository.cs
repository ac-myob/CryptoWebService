namespace CryptoWebService.Application.Abstractions;

public interface IRepository<T>
{
    void Create(T entity);
    IEnumerable<T> ReadAll();
    T? Read(int id);
    void Update(T entity);
    bool Delete(int id);
}