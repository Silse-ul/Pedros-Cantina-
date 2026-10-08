using Microsoft.Data.SqlClient;
namespace Pedros_Cantina_3;

public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; } = string.Empty; /// altid gyldigt tekst object der bare er tomt
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Active { get; set; } = true;
    public int RoleId { get; set; }

}