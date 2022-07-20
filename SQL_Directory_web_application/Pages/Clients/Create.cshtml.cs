using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SQL_Directory_web_application.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new();
        public String errorMessage ="";
        public String successMessage ="";

        public void OnGet()
        {
        }
        public void OnPost()
        {
            clientInfo!.name = Request.Form["name"];
            clientInfo!.email = Request.Form["email"];
            clientInfo!.phone = Request.Form["phone"];
            clientInfo!.address = Request.Form["address"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 ||
                    clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
                // save the new clients into the  data base
            try
            {
                String connectionString = "Data Source=DESKTOP-K029E00;Initial Catalog=customer_database_app;Integrated Security=True";
                using SqlConnection connection = new(connectionString);
                connection.Open();
                String sql = "INSERT INTO clients"+
                    "(name,email, phone, address) VALUES "+
                    "(@name,@email,@phone,@address);";

                using SqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@name", clientInfo.name);
                command.Parameters.AddWithValue("@email", clientInfo.email);
                command.Parameters.AddWithValue("@phone", clientInfo.phone);
                command.Parameters.AddWithValue("@address", clientInfo.address);
                command.ExecuteNonQuery();

            }
            catch(Exception ex)
            {
                errorMessage= ex.Message;   
            }
            clientInfo.name = ""; clientInfo.email = ""; clientInfo.phone = ""; clientInfo.address = "";
            successMessage = "New Client Added Correctly";
            Response.Redirect("/Clients/Index");
            
        }
    }
}
