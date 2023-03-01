using Dapper;
using MySql.Data.MySqlClient;
using PersonaleWeb.Models.Entities;

namespace PersonaleWeb.Helpers
{
    public class DatabaseHelper
    {
        public static string ConnectionString { get; set; }
        public static List<Personale> GetAllPersonale()
        {
            try
            {
                // Connect to the database
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    // Create a query that retrieves all personale"    
                    /*
                    var sql = "SELECT p.*, superiore.* " +
                        "FROM personale  p " +
                        "LEFT JOIN  personale superiore " +
                        "ON p.superioreid = superiore.id";
                    // Use the Query method to execute the query and return a list of objects
                    var listPersonale = connection.Query<Personale, Personale, Personale>(sql,
                        (p, superiore) =>
                        {
                            p.Superiore = superiore;
                            return p;
                        },
                        splitOn: "superioreid"
                        ).ToList();
                    */
                    var sql = "SELECT * " +
                        "FROM personale  ";
                    var listPersonale = connection.Query<Personale>(sql).ToList();
                    foreach (var item in listPersonale)
                    {
                        if (item.SuperioreId > 0)
                            item.Superiore = listPersonale.FirstOrDefault(t => t.Id == item.SuperioreId);
                    }
                    return listPersonale;
                }
            }
            catch (Exception ex)
            {
                // dovrei loggare un messaggio: problema di accesso al database
                return null;
            }
        }

        public static Personale GetPersonaleById(int id)
        {
            try
            {
                // Connect to the database
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    // Create a query that retrieves all personale"    
                    var sql = "SELECT * " +
                        "FROM personale " +
                        "WHERE id=@id";
                    // Use the Query method to execute the query and return a list of objects
                    var personale = connection.Query<Personale>(sql, new { id = id }).FirstOrDefault();
                    return personale;
                }
            }
            catch (Exception ex)
            {
                // dovrei loggare un messaggio: problema di accesso al database
                return null;
            }
        }

        public static Personale InsertPersonale(Personale p)
        {
            try
            {
                using (var db = new MySqlConnection(ConnectionString))
                {
                    var sql = "INSERT INTO personale (cognome,datanascita, professione, reparto, stipendio, superioreid) " +
                        "VALUES (@cognome,@datanascita, @professione, @reparto, @stipendio, @superioreid); " +
                        "SELECT * " +
                        "FROM personale " +
                        "WHERE id = LAST_INSERT_ID()";

                    return db.Query<Personale>(sql, p).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Personale UpdatePersonale(Personale p)
        {
            try
            {
                using (var db = new MySqlConnection(ConnectionString))
                {
                    var sql = "UPDATE personale " +
                        "SET cognome = @cognome, " +
                        " dataNascita = @dataNascita, " +
                        " professione =  @professione, " +
                        " stipendio =  @stipendio, " +
                        " reparto = @reparto," +
                        " superioreId = @superioreId " +
                        "WHERE id = @id; " +
                        "SELECT * FROM personale WHERE id = @id;";
                    var personale = db.Query(sql, p).FirstOrDefault();
                    return (personale);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
