using ElevenNote.Data;
using ElevenNote.Data.Entities;
using ElevenNote.Models.User;
using Microsoft.AspNetCore.Identity;

namespace ElevenNote.Services.User;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;

    public UserService(ApplicationDbContext context, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public async Task<bool> RegisterUserAsync(UserRegister model)
    {
        if (await CheckEmailAvailabilityAsync(model.Email) == false)
        {
            System.Console.WriteLine("Invalid email, already in use.");
            return false;
        }

        if (await CheckUserNameAvailabilityAsync(model.UserName) == false)
        {
            System.Console.WriteLine("Invalid username, already in use.");
            return false;
        }

        UserEntity entity = new()
        {
            Email = model.Email,
            UserName = model.UserName,
            DateCreated = DateTime.Now
        };

        IdentityResult registerResult = await _userManager.CreateAsync(entity, model.Password);

        return registerResult.Succeeded;
    }

    public async Task<UserDetail?> GetUserByIdAsync(int userId)
    {
        UserEntity? entity = await _context.Users.FindAsync(userId);
        if (entity is null) return null;

        UserDetail detail = new()
        {
            Id = entity.Id,
            Email = entity.Email,
            UserName = entity.UserName,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            DateCreated = entity.DateCreated
        };

        return detail;
    }

    private async Task<bool> CheckUserNameAvailabilityAsync(string userName)
    {
        UserEntity? existingUser = await _userManager.FindByNameAsync(userName);
        return existingUser is null;
    }

    private async Task<bool> CheckEmailAvailabilityAsync(string email)
    {
        UserEntity? existingUser = await _userManager.FindByEmailAsync(email);
        return existingUser is null;
    }
}