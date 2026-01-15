using sobujayonApp.Core.Entities;
using sobujayonApp.Core.RepositoryContracts;
using sobujayonApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace sobujayonApp.Infrastructure.Repositories;

internal class UsersRepository : IUsersRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UsersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApplicationUser?> AddUser(ApplicationUser user)
    {
        try
        {
            // Generate a new unique user ID for the user
            user.Id = Guid.NewGuid();

            // Add user to DbSet
            _dbContext.Users.Add(user);

            // Save changes to database
            int rowsAffected = await _dbContext.SaveChangesAsync();

            if (rowsAffected > 0)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
        catch (DbUpdateException ex)
        {
            // Log the inner exception for debugging
            var innerException = ex.InnerException?.Message ?? ex.Message;
            throw new Exception($"Database error while adding user: {innerException}", ex);
        }
    }

    // NEW METHOD: Required for BCrypt verification in the Service Layer
    public async Task<ApplicationUser?> GetUserByEmail(string? email)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    // DEPRECATED: You should remove this or update it to only use Email 
    // because "password" here is now a plain-text string that won't match the DB Hash.
    public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
    {
        // This will likely return null now because the DB stores a hash, not the raw password.
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
    }

    //public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
    //{
    //    // Query user by Email and Password using LINQ
    //    ApplicationUser? user = await _dbContext.Users
    //        .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

    //    return user;
    //}
}