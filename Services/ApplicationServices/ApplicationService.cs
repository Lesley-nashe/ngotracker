using System;
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
        if (model is null)
        {
            return false;
        }
        _appDb.ApplicationModels.Add(model);
        _appDb.SaveChanges();
        return true;
    }

    public async Task<bool> DeleteApplication(Guid id)
    {
        var application = _appDb.ApplicationModels.ToList().FirstOrDefault(u => u.Id == id);
        if (application is null)
        {
            return false;
        }
        _appDb.ApplicationModels.Remove(application);
        _appDb.SaveChanges();
        return true;
    }

    public async Task<ApplicationModel> GetApplication(Guid id)
    {
        var application = _appDb.ApplicationModels.ToList().FirstOrDefault(u => u.Id == id);
        if (application is null)
        {
            return null;
        }
        return application;
    }

    public async Task<IEnumerable<ApplicationModel>> GetApplications()
    {
        return _appDb.ApplicationModels;
    }

    public async Task<ApplicationModel> UpdateApplication(Guid id, ApplicationModel model)
    {
        if (id != model.Id) return null;

        ApplicationModel application = new()
        {
            Id = id,
            NgoId = model.NgoId,
            Ngo = model.Ngo,
            GrantId = model.GrantId,
            Grant = model.Grant,
            Status = model.Status,
            SubmissionDate = model.SubmissionDate,
            CreatedAt = model.CreatedAt,
            Notes = model.Notes,
        };

        _appDb.ApplicationModels.Update(application);
        _appDb.SaveChanges();
        return application;
    }
}
