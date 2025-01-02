using Connected2DB.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Connected2DB.DAL
{
    internal class DBContext
    {
        private readonly string conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=DB01;Integrated Security=True";

        // Method to select records from Emp table
        public List<Emp> SelectRecords()
        {
            List<Emp> emps = new List<Emp>();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string selectQuery = "SELECT * FROM Emp";
                SqlCommand cmd = new SqlCommand(selectQuery, con);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        emps.Add(new Emp()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Address = reader["Address"].ToString()
                        });
                    }
                }
            }
            return emps;
        }

        // Method to insert a new record into Emp table
        public int InsertRecords(Emp emp)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string insertQuery = "INSERT INTO Emp (Id, Name, Address) VALUES (@Id, @Name, @Address)";
                SqlCommand cmd = new SqlCommand(insertQuery, con);
                cmd.Parameters.AddWithValue("@Id", emp.Id);
                cmd.Parameters.AddWithValue("@Name", emp.Name ?? string.Empty);
                cmd.Parameters.AddWithValue("@Address", emp.Address ?? string.Empty);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Method to update an existing record in Emp table
        public int UpdateRecords(Emp emp)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string updateQuery = "UPDATE Emp SET Name = @Name, Address = @Address WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(updateQuery, con);
                cmd.Parameters.AddWithValue("@Name", emp.Name ?? string.Empty);
                cmd.Parameters.AddWithValue("@Address", emp.Address ?? string.Empty);
                cmd.Parameters.AddWithValue("@Id", emp.Id);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Method to delete a record from Emp table
        public int DeleteRecords(int id)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string deleteQuery = "DELETE FROM Emp WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(deleteQuery, con);
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Method to check if a given Id exists in the Emp table
        public bool CheckIfIdExists(int id)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string checkQuery = "SELECT COUNT(1) FROM Emp WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(checkQuery, con);
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0; // Returns true if the Id exists
            }
        }
    }
}
