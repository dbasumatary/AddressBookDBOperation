using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace AddressBookDBOperation
{
    public class Operations
    {
        private SqlConnection connection;
        public Operations(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public void AddEmployee()
        {
            // Prompt the user to enter contact information
            Console.WriteLine("Enter contact details:\n");
            Console.Write("First Name (string): ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name (string): ");
            string lastName = Console.ReadLine();
            Console.Write("Email (string): ");
            string email = Console.ReadLine();
            Console.Write("City (string): ");
            string city = Console.ReadLine();
            Console.Write("State (string): ");
            string state = Console.ReadLine();
            Console.Write("Birthday (date in yyyy-mm-dd format): ");
            DateTime birthday = DateTime.Parse(Console.ReadLine());
            Console.Write("Zip Code (integer): ");
            int zipCode = int.Parse(Console.ReadLine());
            Console.Write("Phone (integer): ");
            int phone = int.Parse(Console.ReadLine());

            try
            {
                using (connection)
                {
                    // Open the connection
                    connection.Open();

                    // Insert the contact into the Contact table
                    string insertContactQuery = "INSERT INTO Contact (FirstName, LastName, Email) VALUES (@FirstName, @LastName, @Email) SELECT SCOPE_IDENTITY();";
                    using (SqlCommand command = new SqlCommand(insertContactQuery, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Email", email);

                        // Get the newly inserted contact's ID
                        int contactID = Convert.ToInt32(command.ExecuteScalar());

                        // Insert the address details into the AddressDetails table
                        string insertAddressQuery = "INSERT INTO AddressDetails (contactID, City, State, Birthday, ZipCode, Phone) VALUES (@ContactID, @City, @State, @Birthday, @ZipCode, @Phone)";
                        using (SqlCommand addressCommand = new SqlCommand(insertAddressQuery, connection))
                        {
                            addressCommand.Parameters.AddWithValue("@ContactID", contactID);
                            addressCommand.Parameters.AddWithValue("@City", city);
                            addressCommand.Parameters.AddWithValue("@State", state);
                            addressCommand.Parameters.AddWithValue("@Birthday", birthday);
                            addressCommand.Parameters.AddWithValue("@ZipCode", zipCode);
                            addressCommand.Parameters.AddWithValue("@Phone", phone);

                            // Execute the query to insert the address details
                            int rowsAffected = addressCommand.ExecuteNonQuery();

                            // Check if the operation was successful
                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Contact added successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to add contact.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            // Connection string for connecting to the database
            string connectionString = @"Data Source=DESKTOP-C00IK64;Database=AddressBookCRUD;Integrated Security=True;";

            Operations operations = new Operations(connectionString);
            operations.AddEmployee();
        }
    }
}