using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;



namespace SQL_Directory_web_application.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-K029E00;Initial Catalog=customer_database_app;Integrated Security=True";

                using SqlConnection connection = new(connectionString);
                connection.Open();
                String sql = "SELECT * FROM clients";
                using SqlCommand command = new(sql, connection);
                {
                    using SqlDataReader reader = command.ExecuteReader();
                    {
                        while (reader.Read())
                        {
                            ClientInfo clientInfo = new() { id = "" + reader.GetInt32(0), name = reader.GetString(1), email = reader.GetString(2), phone = reader.GetString(3), address = reader.GetString(4), created_at = reader.GetDateTime(5).ToString() };

                            listClients.Add(clientInfo);
                        }
                    }
                } 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }  

        }
    }                
                    
    public class ClientInfo
    {
        public String? id;
        public String? name;
        public String? email;
        public String? phone;
        public String? address;
        public String? created_at;

    }
}
