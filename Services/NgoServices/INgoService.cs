using System;
using ngotracker.Models.NgoModels;

namespace ngotracker.Services.NgoServices;

public interface INgoService
{
    Task<bool> CreateNgo(NgoModel model);
    Task<bool> DeleteNgo(Guid id);

    NgoModel GetNgo(Guid id);

    Task<NgoModel> UpdateNgo(Guid id,NgoModel model);

}
