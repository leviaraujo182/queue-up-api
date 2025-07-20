using Microsoft.EntityFrameworkCore;
using QueueUp.Domain.Entities;
using QueueUp.Domain.Interfaces;
using QueueUp.Infraestructure.Context;

namespace QueueUp.Infraestructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        this._appDbContext = appDbContext;
    }
    
    public async Task<User> CreateAsync(User user)
    {
        var createdUser = await _appDbContext.Users.AddAsync(user);
        var entity = createdUser.Entity;
        await _appDbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<User?> FindByIdAsync(Guid id)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

        return user;
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

        return user;
    }
}