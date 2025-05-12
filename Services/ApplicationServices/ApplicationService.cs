using System;
using Microsoft.EntityFrameworkCore;
using ngotracker.Context.AppDbContext;
using ngotracker.Models.ApplicationModels;

namespace ngotracker.Services.ApplicationServices;

public class ApplicationService(AppDbContext appDb) : IApplicationService
{
    private readonly AppDbContext _appDb = appDb;

    public async Task<bool> CreateApplication(ApplicationModel model) /* Service function to create an application */
    {
        if (model is null) return false;
        await _appDb.ApplicationModels.AddAsync(model);
        await _appDb.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteApplication(Guid id) /* Service function to delete an application */
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

    public async Task<ApplicationModel?> GetApplication(Guid id) /* Service function to get an application */
    {
        return await _appDb.ApplicationModels.FindAsync(id);
    }

    public async Task<IEnumerable<ApplicationModel>> GetApplications() /* Service function to get many applications */
    {
        return await _appDb.ApplicationModels.ToListAsync();
    }

    public async Task<ApplicationModel?> UpdateApplication(Guid id, ApplicationModel model) /* Service function to update an application*/
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
