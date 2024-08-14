using Oracle.ManagedDataAccess.Client;
using Shared.Data;
using Shared.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace Shared.Services;

public class DesignationService : IDesignationService
{
    private readonly AppDbContext _context;
    public DesignationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<DesignationModel>> GetAllAsync()
    {
        List<DesignationModel> designations = new List<DesignationModel>();

        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "NFT_AUTH_TEST_PCK.Designations_GA";
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
                        designations.Add(new DesignationModel
                        {
                            Designation_Id = result["designation_id"].ToString()!,
                            Designation_Name = result["designation_name"].ToString(),
                            Designation_Priority = result["designation_priority"].ToString(),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching designations: {ex.Message}");
            }
            finally
            {
                await _context.Database.CloseConnectionAsync();
            }
        }

        return designations;
    }
}
