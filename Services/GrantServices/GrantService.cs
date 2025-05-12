using System;
using Microsoft.EntityFrameworkCore;
using ngotracker.Context.AppDbContext;
using ngotracker.Models.GrantModels;

namespace ngotracker.Services.GrantServices;

public class GrantService(AppDbContext appDb) : IGrantService
{
    private readonly AppDbContext _appDb = appDb;

    public async Task<bool> CreateGrant(GrantModel model) /* Service function to create a grant */
    {
        if (model is null) return false;
        await _appDb.GrantModels.AddAsync(model);
        await _appDb.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteGrant(Guid id) /* Service function to delete an grant */
    {
        var grant = await _appDb.GrantModels.FindAsync(id);
        if (grant is null)
        {
            return false;
        }
        _appDb.GrantModels.Remove(grant);
        await _appDb.SaveChangesAsync();
        return true;
    }

    public async Task<GrantModel?> GetGrant(Guid id) /* Service function to get an grant */
    {
        return await _appDb.GrantModels.FindAsync(id);
    }

    public async Task<IEnumerable<GrantModel>> GetGrants() /* Service function to get a grant */
    {
        return await _appDb.GrantModels.ToListAsync();
    }

    public async Task<GrantModel?> UpdateGrant(Guid id, GrantModel model) /* Service function to update a grant*/
    {
        if (id != model.Id) return null;

        var grant = await _appDb.GrantModels.FindAsync(id);
        if (grant == null) return null;

        grant.Id = id;
        grant.Title = model.Title;
        grant.Provider = model.Provider;
        grant.Amount = model.Amount;
        grant.Currency = model.Currency;
        grant.Description = model.Description;
        grant.Eligibility = model.Eligibility;
        grant.Status = model.Status;
        grant.ContactPhone = model.ContactPhone;
        grant.CreatedAt = model.CreatedAt;
        grant.Deadline = DateTime.UtcNow;

        _appDb.GrantModels.Update(grant);
        await _appDb.SaveChangesAsync();
        return grant;
    }
}
