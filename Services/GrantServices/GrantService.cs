using System;
using ngotracker.Context.AppDbContext;
using ngotracker.Models.GrantModels;

namespace ngotracker.Services.GrantServices;

public class GrantService : IGrantService
{
    private readonly AppDbContext _appDb;
    private readonly IConfiguration _configuration;

    public GrantService(IConfiguration configuration, AppDbContext appDb)
    {
        _configuration = configuration;
        _appDb = appDb;

    }
    public async Task<bool> CreateGrant(GrantModel model)
    {
        if (model is null)
        {
            return false;
        }
        _appDb.GrantModels.Add(model);
        _appDb.SaveChanges();
        return true;

    }

    public async Task<bool> DeleteGrant(Guid id)
    {
        var grant = _appDb.GrantModels.ToList().FirstOrDefault(u => u.Id == id);
        if (grant is null)
        {
            return false;
        }
        _appDb.GrantModels.Remove(grant);
        _appDb.SaveChanges();
        return true;
    }

    public async Task<GrantModel> GetGrant(Guid id)
    {
        var grant = _appDb.GrantModels.ToList().FirstOrDefault(u => u.Id == id);
        if (grant is null)
        {
            return null;
        }
        return grant;
    }

    public async Task<IEnumerable<GrantModel>> GetGrants()
    {
        return _appDb.GrantModels;
    }

    public async Task<GrantModel> UpdateGrant(Guid id, GrantModel model)
    {
        if (id != model.Id) return null;

        GrantModel grant = new()
        {
            Id = id,
            Title = model.Title,
            Provider = model.Provider,
            Amount = model.Amount,
            Currency = model.Currency,
            Description = model.Description,
            Eligibility = model.Eligibility,
            Status = model.Status,
            ContactPhone = model.ContactPhone,
            CreatedAt = model.CreatedAt,
            Deadline = DateTime.UtcNow
        };

        _appDb.GrantModels.Update(grant);
        _appDb.SaveChanges();
        return grant;
    }
}
