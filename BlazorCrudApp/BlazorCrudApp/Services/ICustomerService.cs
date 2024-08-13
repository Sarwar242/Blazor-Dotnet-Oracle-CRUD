using BlazorCrudApp.Models;

namespace BlazorCrudApp.Services;

public interface ICustomerService
{
    bool Create(CustomerModel Model);
    Task<List<CustomerModel>> GetAllAsync();
}
