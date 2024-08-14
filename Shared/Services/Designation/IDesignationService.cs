using Shared.Models;
namespace Shared.Services;

public interface IDesignationService
{
    Task<List<DesignationModel>> GetAllAsync();
}
