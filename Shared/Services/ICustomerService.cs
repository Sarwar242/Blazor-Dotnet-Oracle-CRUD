using Shared.Models;

namespace Shared.Services;

public interface ICustomerService
{
    bool Create(CustomerModel Model);
    Task<List<CustomerModel>> GetAllAsync();
}
