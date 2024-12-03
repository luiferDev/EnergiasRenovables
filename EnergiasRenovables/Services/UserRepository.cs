using EnergiasRenovables.Data;
using EnergiasRenovables.Model.DTO;
using EnergiasRenovables.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnergiasRenovables.Services;

public class UserRepository(ApplicationDbContext context)
{
    public async Task<Usuario?> GetByUsernameAsync(string username)
    {
        return await context.Usuarios.FirstOrDefaultAsync(u => u.UserName == username);
    }

    public async Task AddUserAsync(Usuario user)
    {
        await context.Usuarios.AddAsync(user);
        await context.SaveChangesAsync();
    }
}