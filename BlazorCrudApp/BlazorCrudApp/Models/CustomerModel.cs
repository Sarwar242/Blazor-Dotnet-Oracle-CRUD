using System.ComponentModel.DataAnnotations;

namespace BlazorCrudApp.Models;

public class CustomerModel
{
    [Key]
    public string Customer_Id { get; set; }
    public string? Customer_Name { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Auth1stBy { get; set; }
    public string? MakeBy { get; set; }
    public string? Auth2ndBy { get; set; }
    public string? AuthStatus { get; set; }
    public DateTime? Auth1stDt { get; set; }
    public DateTime? Auth2ndDt { get; set; }
    public DateTime? MakeDt { get; set; }
}
