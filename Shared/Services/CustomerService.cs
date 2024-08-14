using Oracle.ManagedDataAccess.Client;
using Shared.Data;
using Shared.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Shared.Services;

public class CustomerService : ICustomerService
{
    private readonly AppDbContext _context;
    public CustomerService(AppDbContext context)
    {
        _context = context;
    }
	
    public bool Create(CustomerModel Model)
    {
        var transaction = _context.Database.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            var customer = Model;
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "NFT_AUTH_TEST_PCK.Sarwar_Customer_I";
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = transaction.GetDbTransaction();

                var Customer_Id = new OracleParameter("p_customer_id", OracleDbType.Varchar2, 50) { Direction = ParameterDirection.Output };
                var Customer_Name = new OracleParameter("p_customer_name", OracleDbType.Varchar2) { Value = customer.Customer_Name };
                var Address = new OracleParameter("p_address", OracleDbType.Varchar2) { Value = customer.Address };
                var Phone = new OracleParameter("p_phone", OracleDbType.Varchar2) { Value = customer.Phone };
                var Make_By = new OracleParameter("p_make_by", OracleDbType.Varchar2) { Value = "Sarwar" };
                var Last_Action = new OracleParameter("p_last_action", OracleDbType.Varchar2) { Value = "ADD" };
                var Error_Msg = new OracleParameter("p_error_msg", OracleDbType.Varchar2, 4000) { Direction = ParameterDirection.Output };
                var Error_Code = new OracleParameter("p_error_code", OracleDbType.Varchar2, 20) { Direction = ParameterDirection.Output };
                var RowId = new OracleParameter("v_rowId", OracleDbType.Varchar2, 18) { Direction = ParameterDirection.Output };

                command.Parameters.AddRange(new[] { Customer_Id, Customer_Name, Address, Phone, Make_By, Last_Action, Error_Msg, Error_Code, RowId });

                _context.Database.OpenConnection();
                command.ExecuteNonQuery();

                // Check for errors
                //var testb = Error_Code.Value.ToString();
                if (Error_Code.Value!=null && Error_Code.Value.ToString() != "null")
                {
                    Console.WriteLine($"Error Code: {Error_Code.Value}, Error Message: {Error_Msg.Value}");
                    return false;
                }

                // Update the customer object with the new ID
                if (Customer_Id.Value != null)
                {
                    customer.Customer_Id = Customer_Id.Value.ToString()!;
                }
            }

            // Add the customer to the context and save changes
            //_context.Customers_By_Sarwar.Add(customer);
            //var success = _context.SaveChanges() > 0;

            // If everything is successful, commit the transaction
            transaction.Commit();
            return true;
        }
        catch (Exception ex)
        {
            // If an error occurs, roll back the transaction
            transaction.Rollback();
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            _context.Database.CloseConnection();
        }
    }

    public async Task<List<CustomerModel>> GetAllAsync()
    {
        List<CustomerModel> customers = new List<CustomerModel>();

        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "NFT_AUTH_TEST_PCK.Customers_GA";
            command.CommandType = CommandType.StoredProcedure;

            // Output cursor parameter
            var cursorParam = new OracleParameter("p_sys_cursor", OracleDbType.RefCursor) { Direction = ParameterDirection.Output };
            command.Parameters.Add(cursorParam);

            try
            {
                await _context.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        customers.Add(new CustomerModel
                        {
                            Customer_Id = result["customer_id"].ToString()!,
                            Customer_Name = result["customer_name"].ToString(),
                            Address = result["address"].ToString(),
                            Phone = result["phone"].ToString(),
                            Auth1stBy = result["auth1stby"].ToString(),
                            Auth2ndBy = result["auth2ndby"].ToString(),
                            AuthStatus = result["authstatus"].ToString(),
                            Auth2ndDt = result["auth2nddt"] != DBNull.Value ? Convert.ToDateTime(result["auth2nddt"]) : (DateTime?)null,
                            MakeDt = result["makedt"] != DBNull.Value ? Convert.ToDateTime(result["makedt"]) : (DateTime?)null,
                            Auth1stDt = result["auth1stdt"] != DBNull.Value ? Convert.ToDateTime(result["auth1stdt"]) : (DateTime?)null,
                            MakeBy = result["makeby"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching customers: {ex.Message}");
            }
            finally
            {
                await _context.Database.CloseConnectionAsync();
            }
        }

        return customers;
    }
}
