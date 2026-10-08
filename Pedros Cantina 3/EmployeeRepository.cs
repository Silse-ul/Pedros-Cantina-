using Microsoft.Data.SqlClient;

namespace Pedros_Cantina_3;

public class EmployeeRepository
{
    private readonly DatabaseRepository _db = new DatabaseRepository();
    
    public List<Employee> Getall()
    {
        var Employee = new List<Employee>();

        string sql = "SELECT employeeid, name, phone, email, active, roleid from Employee";

        using (SqlConnection connection = _db.GetConnection())
        {
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee emp = new Employee()
                        {
                            EmployeeId = reader.GetInt32(0), // employeeid
                            Name = reader.GetString(1),       // name
                            Phone = reader.GetString(2),      // phone
                            Email = reader.GetString(3),      // email
                            Active = reader.GetBoolean(4),    // active
                            RoleId = reader.GetInt32(5)       // roleid
                        };
                        
                        Employee.Add(emp);

                    }

                }
            }
        }
        return Employee;
    }
    public class DatabaseConnection
    {
    
        private readonly DatabaseRepository _dbRepository = new DatabaseRepository();

        public void GetEmployees()
        {
            // Hent den åbne forbindelse direkte fra din private repository
            using (SqlConnection connection = _dbRepository.GetConnection())
            {
                
            }
        }
    }
    
    
    public void Create(Employee NyAnsat)
    {
        string Sql = "insert into Employee (name, Phone, email, active, roleid) values (@name, @Phone, @email, @active , @roleid) ";
        using (SqlConnection connection = _db.GetConnection())
        {
            using (SqlCommand command = new SqlCommand(Sql, connection))
            {
                command.Parameters.AddWithValue("@name", NyAnsat.Name);
                command.Parameters.AddWithValue("@Phone", NyAnsat.Phone);
                command.Parameters.AddWithValue("@email", NyAnsat.Email);
                command.Parameters.AddWithValue("@active", NyAnsat.Active);
                command.Parameters.AddWithValue("@roleid", NyAnsat.RoleId);
                connection.Open();
                // når vi bare sender data og ikke skal have rækker retur
                command.ExecuteNonQuery();
            }
        }
    }

    public void Update(Employee NyAnsat)
    {
        string sql = "UPDATE Employee SET name = @name, phone = @phone, email = @email, active = @active, roleid = @roleid WHERE employeeid = @employeeid";
        
        using (SqlConnection connection = _db.GetConnection())
        {
            using (SqlCommand command = new SqlCommand(sql, connection))
                {
                command.Parameters.AddWithValue("@mployeeid", NyAnsat.EmployeeId);    
                command.Parameters.AddWithValue("@name", NyAnsat.Name);
                command.Parameters.AddWithValue("@phone", NyAnsat.Phone);
                command.Parameters.AddWithValue("@email", NyAnsat.Email);
                command.Parameters.AddWithValue("@active", NyAnsat.Active);
                command.Parameters.AddWithValue("@roleid", NyAnsat.RoleId);
                
                connection.Open();
                command.ExecuteNonQuery();
                }
        }
    }

    public void Delete( int EmployeeId)
    {
        string sql = "DELETE FROM Employee WHERE employeeid = @employeeid";

        using (SqlConnection connection = _db.GetConnection())
        
        {
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@employeeid", EmployeeId);
                
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

}
