using BlazorCrudApp.Models;

namespace BlazorCrudApp.Services;

public interface ICustomerService
{
    bool Create(CustomerModel Model);
    List<CustomerModel> GetAllAsync();
}
