using System;
using ngotracker.Context.AppDbContext;
using ngotracker.Models.NgoModels;

namespace ngotracker.Services.NgoServices;

public class NgoService : INgoService
{
    private readonly AppDbContext _appDb;
    private readonly IConfiguration _configuration;
    public NgoService(IConfiguration configuration, AppDbContext appDb)
    {
        _configuration = configuration;
        _appDb = appDb;

    }
    public async Task<bool> CreateNgo(NgoModel model)
    {
        if (model is null)
        {
            return false;
        }
        _appDb.NgoModels.Add(model);
        _appDb.SaveChanges();
        return true;

    }

    public async Task<bool> DeleteNgo(Guid id)
    {
        var ngo = _appDb.NgoModels.ToList().FirstOrDefault(u => u.Id == id);
        if (ngo is null)
        {
            return false;
        }
        _appDb.NgoModels.Remove(ngo);
        _appDb.SaveChanges();
        return true;
    }

    public async Task<NgoModel> GetNgo(Guid id)
    {
        var ngo = _appDb.NgoModels.ToList().FirstOrDefault(u => u.Id == id);
        if (ngo is null)
        {
            return null;
        }
        return ngo;
    }

    public async Task<IEnumerable<NgoModel>> GetNgos()
    {
        return _appDb.NgoModels;
    }

    public async Task<NgoModel> UpdateNgo(Guid id, NgoModel model)
    {
        if (id != model.Id) return null;

        NgoModel ngo = new()
        {
            Id = id,
            Name = model.Name,
            RegistrationNumber = model.RegistrationNumber,
            Description = model.Description,
            Sector = model.Sector,
            Country = model.Country,
            Address = model.Address,
            ContactEmail = model.ContactEmail,
            ContactPhone = model.ContactPhone,
            Website = model.Website,
            LogoUrl = model.LogoUrl,
            Verified = model.Verified,
            CreatedAt = model.CreatedAt,
            UpdatedAt = DateTime.UtcNow,
        };

        _appDb.NgoModels.Update(ngo);
        _appDb.SaveChanges();
        return ngo;
    }
}
