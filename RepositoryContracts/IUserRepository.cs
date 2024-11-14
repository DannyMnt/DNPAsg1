using Entities;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<User> getSingleAsync(int id);
    Task<User> getSingleAsync(string username);
    IQueryable<User> getMany();
}