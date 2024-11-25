using System.Globalization;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcCommentRepository: ICommentRepository
{
    private readonly AppContext ctx;

    public EfcCommentRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }
    
    public async Task<Comment> AddAsync(Comment comment)
    {
        EntityEntry<Comment> entityEntry = await ctx.Comments.AddAsync(comment);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;    
    }

    public async Task UpdateAsync(Comment comment)
    {
        if(!await ctx.Comments.AnyAsync(p => p.Id == comment.Id))
            throw new CultureNotFoundException("Comment not found");
        ctx.Comments.Update(comment);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Comment? existing = await ctx.Comments.SingleOrDefaultAsync(p => p.Id == id);
        if(existing == null)
            throw new CultureNotFoundException("Comment not found");
        ctx.Comments.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<Comment> getSingleAsync(int id)
    {
        Comment? existing = await ctx.Comments.SingleOrDefaultAsync(p => p.Id == id);
        if(existing == null)
            throw new CultureNotFoundException("Comment not found");
        return existing;
    }

    public IQueryable<Comment> getMany()
    {
        return ctx.Comments.AsQueryable();
    }
}