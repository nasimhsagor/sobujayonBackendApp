using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories;

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
            user.UserID = Guid.NewGuid();

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

    public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
    {
        // Query user by Email and Password using LINQ
        ApplicationUser? user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

        return user;
    }
}