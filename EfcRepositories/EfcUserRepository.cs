using System.Globalization;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcUserRepository: IUserRepository
{
    private readonly AppContext ctx;

    public EfcUserRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }
    public async Task<User> AddAsync(User user)
    {
        EntityEntry<User> entityEntry = await ctx.Users.AddAsync(user);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;    
    }

    public async Task UpdateAsync(User user)
    {
        if(!await ctx.Users.AnyAsync(u => u.Id == user.Id))
            throw new KeyNotFoundException();
        ctx.Users.Update(user);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        User? existing = await ctx.Users.SingleOrDefaultAsync(p => p.Id == id);
        if(existing == null)
            throw new CultureNotFoundException("User not found");
        ctx.Users.Remove(existing);
        await ctx.SaveChangesAsync();    
    }

    public async Task<User> getSingleAsync(int id)
    {
        User? existing = await ctx.Users.SingleOrDefaultAsync(p => p.Id == id);
        if(existing == null)
            throw new CultureNotFoundException("User not found");
        return existing;
    }

    public async Task<User> getSingleAsync(string username)
    {
        User? existing = await ctx.Users.SingleOrDefaultAsync(p => p.Username == username);
        if(existing == null)
            throw new CultureNotFoundException("User not found");
        return existing;
    }

    public IQueryable<User> getMany()
    {
        return ctx.Users.AsQueryable();
    }
}