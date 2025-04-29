using System;
using Microsoft.EntityFrameworkCore;
using ngotracker.Context.AppDbContext;
using ngotracker.Models.ApplicationModels;

namespace ngotracker.Services.ApplicationServices;

public class ApplicationService : IApplicationService
{
    private readonly AppDbContext _appDb;
    private readonly IConfiguration _configuration;
    public ApplicationService(IConfiguration configuration, AppDbContext appDb)
    {
        _configuration = configuration;
        _appDb = appDb;

    }

    public async Task<bool> CreateApplication(ApplicationModel model)
    {
        if (model is null) return false;
        await _appDb.ApplicationModels.AddAsync(model);
        await _appDb.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteApplication(Guid id)
    {
        var application = await _appDb.ApplicationModels.FindAsync(id);
        if (application is null)
        {
            return false;
        }
        _appDb.ApplicationModels.Remove(application);
        await _appDb.SaveChangesAsync();
        return true;
    }

    public async Task<ApplicationModel?> GetApplication(Guid id)
    {
        return await _appDb.ApplicationModels.FindAsync(id);
    }

    public async Task<IEnumerable<ApplicationModel>> GetApplications()
    {
        return await _appDb.ApplicationModels.ToListAsync();
    }

    public async Task<ApplicationModel?> UpdateApplication(Guid id, ApplicationModel model)
    {
        if (id != model.Id) return null;
        var app = await _appDb.ApplicationModels.FindAsync(id);
        if (app is null) return null;

        app.Id = id;
        app.NgoId = model.NgoId;
        app.Ngo = model.Ngo;
        app.GrantId = model.GrantId;
        app.Grant = model.Grant;
        app.Status = model.Status;
        app.SubmissionDate = model.SubmissionDate;
        app.CreatedAt = model.CreatedAt;
        app.Notes = model.Notes;

        _appDb.ApplicationModels.Update(app);
        await _appDb.SaveChangesAsync();
        return app;
    }
}
