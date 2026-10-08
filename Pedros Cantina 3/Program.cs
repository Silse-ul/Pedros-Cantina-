using Microsoft.Data.SqlClient;
namespace Pedros_Cantina_3;

class Program
{
    static void Main(string[] args)
    {
        EmployeeRepository repo = new EmployeeRepository();
        bool korer = true;

        while (korer)
        {
            Console.Clear();
            Console.WriteLine("=== VELKOMMEN TIL PEDROS CANTINA ===");
            Console.WriteLine("1. Vis alle medarbejdere");
            Console.WriteLine("2. Opret ny medarbejder");
            Console.WriteLine("3. Opdater medarbejder");
            Console.WriteLine("4. Slet medarbejder");
            Console.WriteLine("5. Afslut programmet");
            Console.Write("Vælg en mulighed (1-5): ");
            

            string valg = Console.ReadLine();

            if (valg == "1")
            {
                Console.Clear();
                Console.WriteLine("--- MEDARBEJDERLISTE ---");

                var liste = repo.Getall();

                if (liste.Count == 0)
                {
                    Console.WriteLine("Der er endnu ikke oprettet nogen medarbejdere.");
                }
                else
                {
                    foreach (var emp in liste)
                    {
                        // 2. Tjek om RoleId hentyder til en leder (f.eks. hvis RoleId == 1 er Leder)
                        string lederStatus = (emp.RoleId == 1) ? "Leder" : "Medarbejder";
                        string aktivStatus = emp.Active ? "Aktiv" : "Inaktiv";
                        Console.WriteLine(
                            $"ID: {emp.EmployeeId} | Navn: {emp.Name} | Tlf: {emp.Phone} | Status: {lederStatus} ({aktivStatus})");
                    }
                }

                Console.WriteLine("\nTryk på Enter for at gå tilbage til menuen...");
                Console.ReadLine();
            }
            else if (valg == "2")
            {
                Console.Clear();
                Console.WriteLine("--- OPRET NY MEDARBEJDER ---");

                Employee nyAnsat = new Employee();

                Console.Write("Indtast navn: ");
                nyAnsat.Name = Console.ReadLine();

                Console.Write("Indtast telefonnummer: ");
                nyAnsat.Phone = Console.ReadLine();

                Console.Write("Indtast email: ");
                nyAnsat.Email = Console.ReadLine();

                Console.Write("Er personen leder? (ja/nej): ");
                string lederSvar = Console.ReadLine().ToLower();
                nyAnsat.RoleId = (lederSvar == "ja" || lederSvar == "j") ? 1 : 2;

                // Medarbejderen starter som aktiv
                nyAnsat.Active = true;

                Console.WriteLine("\nGemmer i databasen...");

                repo.Create(nyAnsat);

                Console.WriteLine("Medarbejderen blev oprettet med succes!");
                Console.WriteLine("Tryk på Enter for at gå tilbage til menuen...");
                Console.ReadLine();
            }
            else if (valg == "3")
            {
                Console.Clear();
                Console.WriteLine("--- MEDARBEJDERLISTE ---");

                Console.WriteLine("Indtast ID på den medarbejdere du vil opdatere:");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    Employee UpdateEmployee = new Employee();
                    UpdateEmployee.EmployeeId = id;

                    Console.Write("Indtast nyt navn: ");
                    UpdateEmployee.Name = Console.ReadLine();

                    Console.Write("Indtast nyt telefonnummer: ");
                    UpdateEmployee.Phone = Console.ReadLine();

                    Console.Write("Indtast ny email: ");
                    UpdateEmployee.Email = Console.ReadLine();

                    Console.Write("Er personen leder? (ja/nej): ");
                    string lederSvar = Console.ReadLine().ToLower();
                    UpdateEmployee.RoleId = (lederSvar == "ja" || lederSvar == "j") ? 1 : 2;

                    Console.Write("Er medarbejderen aktiv? (ja/nej): ");
                    string aktivSvar = Console.ReadLine().ToLower();
                    UpdateEmployee.Active = aktivSvar == "ja" || aktivSvar == "j";

                    Console.WriteLine("\nOpdaterer i databasen...");
                    repo.Update(UpdateEmployee);

                    Console.WriteLine("Medarbejderen blev oprettet med succes!");
                }
                else
                {
                    Console.WriteLine("Ugyldigt ID.");
                }

                Console.WriteLine("Tryk enter for at gå tilbage til menuen..");
                Console.ReadLine();

            }
            else if (valg == "4")
            {
                Console.Clear();
                Console.WriteLine("--- SLET MEDARBEJDER ---");

                Console.Write("Indtast ID på den medarbejder du vil slette: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.Write($" Er du sikker på at du vil slette medarbejder med ID {id} (Ja/nej): ");
                    string bekraeft = Console.ReadLine()?.ToLower() ?? "";

                    if (bekraeft == "ja" || bekraeft == "j")
                    {
                        Console.WriteLine("\nSletter fra databasen...");
                        repo.Delete(id);
                        Console.WriteLine("Medarbejderen blev slettet med succes!");
                    }
                    else
                    {
                        Console.WriteLine("Sletning annulleret.");
                    }
                    
                }
                else 
                {
                    Console.WriteLine("Ugyldigt ID.");
                }

                Console.WriteLine("Tryk på Enter for at gå tilbage til menuen...");
                Console.ReadLine();
            }
            else if (valg == "5")
            {
                Console.Clear();
                korer = false;
                Console.WriteLine("Programmet afsluttes hav en god dag");
            }
            else
            {
                Console.WriteLine("Ugyldigt valg. prøv igen tryk enter..");
                Console.ReadLine();
            }
                
        }
    }

}
