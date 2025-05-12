using System;
using Microsoft.EntityFrameworkCore;
using ngotracker.Context.AppDbContext;
using ngotracker.Models.NgoModels;

namespace ngotracker.Services.NgoServices;

public class NgoService(AppDbContext appDb) : INgoService
{
    private readonly AppDbContext _appDb = appDb;

    public async Task<bool> CreateNgo(NgoModel model) /* Service function to create a ngo */
    {
        if (model is null) return false;
        await _appDb.NgoModels.AddAsync(model);
        await _appDb.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteNgo(Guid id) /* Service function to delete a ngo */
    {
        var ngo = await _appDb.NgoModels.FindAsync(id);
        if (ngo is null)
        {
            return false;
        }
        _appDb.NgoModels.Remove(ngo);
        await _appDb.SaveChangesAsync();
        return true;
    }

    public async Task<NgoModel?> GetNgo(Guid id) /* Service function to get an ngo */
    {
        return await _appDb.NgoModels.FindAsync(id);
    }

    public async Task<IEnumerable<NgoModel>> GetNgos() /* Service function to get a ngos */
    {
        return await _appDb.NgoModels.ToListAsync();
    }

    public async Task<NgoModel?> UpdateNgo(Guid id, NgoModel model) /* Service function to update an ngo */
    {
        if (id != model.Id) return null;

        var ngo = await _appDb.NgoModels.FindAsync(id);
        if (ngo == null) return null;

        ngo.Id = id;
        ngo.Name = model.Name;
        ngo.RegistrationNumber = model.RegistrationNumber;
        ngo.Description = model.Description;
        ngo.Sector = model.Sector;
        ngo.Country = model.Country;
        ngo.Address = model.Address;
        ngo.ContactEmail = model.ContactEmail;
        ngo.ContactPhone = model.ContactPhone;
        ngo.Website = model.Website;
        ngo.LogoUrl = model.LogoUrl;
        ngo.Verified = model.Verified;
        ngo.CreatedAt = model.CreatedAt;
        ngo.UpdatedAt = DateTime.UtcNow;

        _appDb.NgoModels.Update(ngo);
        await _appDb.SaveChangesAsync();
        return ngo;
    }
}
