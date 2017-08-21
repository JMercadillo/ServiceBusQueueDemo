using System;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApi.Models
{
    public class Suscripciones
    {
        public int ConsultarSuscripcion()
        {
            var ConnString = ConfigurationManager.ConnectionStrings["NotificationsConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                var query = "SELECT TOP(1) id FROM sys.dm_qn_subscriptions ORDER BY id DESC";
                SqlCommand cmd = new SqlCommand(query, con);
                var result = 0;
                if (con.State != System.Data.ConnectionState.Open)
                    con.Open();

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result = reader.GetInt32(0);
                    }
                }
                catch (Exception ex)
                {
                    result = 0;
                }

                return result;
            }
        }

        public bool EliminarSuscripcion(int SubscripcionId)
        {
            var ConnString = ConfigurationManager.ConnectionStrings["NotificationsConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                var query = $"KILL QUERY NOTIFICATION SUBSCRIPTION {SubscripcionId} ;";
                SqlCommand cmd = new SqlCommand(query, con);
                var result = 0;
                if (con.State != System.Data.ConnectionState.Open)
                    con.Open();

                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    result = 0;
                }
                finally
                {
                    con.Close();
                }

                return result > 0;
            }
        }
    }
}