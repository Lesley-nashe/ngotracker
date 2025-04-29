using System;
using ngotracker.Models.GrantModels;

namespace ngotracker.Services.GrantServices;

public interface IGrantService
{
    Task<bool> CreateGrant(GrantModel model);
    Task<bool> DeleteGrant(Guid id);

    Task<GrantModel?> GetGrant(Guid id);

    Task<GrantModel?> UpdateGrant(Guid id,GrantModel model);

     Task<IEnumerable<GrantModel>> GetGrants();

}
