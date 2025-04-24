using System;
using ngotracker.Models.ApplicationModels;

namespace ngotracker.Services.ApplicationServices;

public interface IApplicationService
{
    Task<bool> CreateApplication(ApplicationModel model);
    Task<bool> DeleteApplication(Guid id);

    Task<ApplicationModel> GetApplication(Guid id);

    Task<ApplicationModel> UpdateApplication(Guid id,ApplicationModel model);

    Task<IEnumerable<ApplicationModel>> GetApplications();

}
