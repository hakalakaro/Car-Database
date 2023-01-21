
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;




namespace Autokauppa.model
{
    public class DatabaseHallinta
    {
        // Make predefined connection string available to use
        string yhteysTiedot { get; set; }

        // Define SqlConnection
        SqlConnection dbYhteys = new SqlConnection();

        public DatabaseHallinta()
        {
            // Connection string
            yhteysTiedot = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Autokauppa; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False;";
        }

        // Connects to the database
        public bool connectDatabase()
        {
            dbYhteys.ConnectionString = yhteysTiedot;
            
            try
            { 
                dbYhteys.Open();
                Console.WriteLine("Yhteys toimii.");
                return true;
                
            }
            catch(Exception e)
            { 
                Console.WriteLine("Virheilmoitukset:" + e);
                dbYhteys.Close();
                return false;

            }
            
        }

        // Closes the sql connection
        public void disconnectDatabase()
        {
            dbYhteys.Close();
        }

        // Deletes selected car from database
        public bool deleteCarFromDatabase(Auto selectedCar)
        {
            bool palaute1 = false;
            using (SqlConnection connection = new SqlConnection(yhteysTiedot))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM Auto WHERE ID = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", selectedCar.ID);
                    command.ExecuteNonQuery();
                    palaute1 = true;
                    Console.WriteLine("Auto poistettu tietokannasta.");
                }
                connection.Close();
            }
            return palaute1;
            
        }
        // Saves created car object into database
        public bool saveAutoIntoDatabase(Auto newAuto)
        {
            {
                bool palaute = false;
                using (SqlConnection connection = new SqlConnection(yhteysTiedot))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO Auto (Hinta, Rekisteri_paivamaara, Moottorin_tilavuus, Mittarilukema, AutonMerkkiID, AutonMalliID, VaritID, PolttoaineID) VALUES (@Hinta, @Rekisteri_paivamaara, @Moottorin_tilavuus, @Mittarilukema, @AutonMerkkiID, @AutonMalliID, @VaritID, @PolttoaineID)", connection))
                    {
                        command.Parameters.AddWithValue("@Hinta", newAuto.Hinta);
                        command.Parameters.AddWithValue("@Rekisteri_paivamaara", newAuto.Rekisteri_paivamaara);
                        command.Parameters.AddWithValue("@Moottorin_tilavuus", newAuto.Moottorin_tilavuus);
                        command.Parameters.AddWithValue("@Mittarilukema", newAuto.Mittarilukema);
                        command.Parameters.AddWithValue("@AutonMerkkiID", newAuto.AutonMerkkiID);
                        command.Parameters.AddWithValue("@AutonMalliID", newAuto.AutonMalliID);
                        command.Parameters.AddWithValue("@VaritID", newAuto.VaritID);
                        command.Parameters.AddWithValue("@PolttoaineID", newAuto.PolttoaineID);
                        palaute = true;
                        int rowsAffected = command.ExecuteNonQuery();
                        if(rowsAffected > 0)
                        {
                            
                            Console.WriteLine("Added car to database successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Error adding car to database.");
                        }
                    }
                    connection.Close();
                }
                return palaute;
            }
        }

        
        

        // Creates AutonMalli objects for each car model in table, saves them to a list and passes them on 
        public List<AutonMalli> getAutoModelsByMakerId(int ID) 
             
        {
            List<AutonMalli> ModelList= new List<AutonMalli>();
            using (SqlConnection connection = new SqlConnection(yhteysTiedot))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT ID, Auton_mallin_nimi FROM AutonMallit WHERE AutonMerkkiID = @ID", connection))
                {
                    command.Parameters.AddWithValue("@ID", ID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            AutonMalli newAutonMalli = new AutonMalli();
                            newAutonMalli.ID = reader.GetInt32(0);
                            newAutonMalli.Auton_mallin_nimi = reader.GetString(1);
                           
                            ModelList.Add(newAutonMalli);

                        }
                    }
                }
                connection.Close();
            }

            return ModelList;
        }

        // Creates a car object for each entry in Auto table and passes them on as a list
        public List<Auto> getAllCarsFromDatabase()
        {
            List<Auto> CarList = new List<Auto>();
            using (SqlConnection connection = new SqlConnection(yhteysTiedot))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT Auto.*, AutonMallit.Auton_mallin_nimi, AutonMerkki.Merkki FROM Auto JOIN AutonMallit ON Auto.AutonMerkkiID = AutonMallit.AutonMerkkiID JOIN AutonMerkki ON Auto.AutonMerkkiID = AutonMerkki.ID ", connection))
                {
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            Auto car = new Auto();
                            car.ID = (int)reader["ID"];
                            car.Hinta = (decimal)reader["Hinta"];
                            car.Rekisteri_paivamaara = (DateTime)reader["Rekisteri_paivamaara"];
                            car.Moottorin_tilavuus = (decimal)reader["Moottorin_tilavuus"];
                            car.Mittarilukema = (int)reader["Mittarilukema"];
                            car.Merkki = (string)reader["Merkki"];
                            car.Auton_mallin_nimi = (string)reader["Auton_mallin_nimi"];
                            car.VaritID = (int)reader["VaritID"];
                            car.PolttoaineID = (int)reader["PolttoaineID"];
                            CarList.Add(car);


                        }
                    }
                }
                connection.Close();
            }

            return CarList;
        }

        // Creates color objects with attributes and passes them on as a list
        public List<Varit> getColorFromDatabase()
        {
            List<Varit> ColorList = new List<Varit>();
            using (SqlConnection connection = new SqlConnection(yhteysTiedot))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT ID, Varin_nimi FROM Varit", connection))

                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Varit newVarit = new Varit();
                        newVarit.ID = reader.GetInt32(0);
                        newVarit.Varin_nimi = reader.GetString(1);
                        ColorList.Add(newVarit);


                    }
                }
                
                connection.Close();
            }

            return ColorList;
        }

        // Creates Gastype objects with attributes and passes them on as a list
        public List<Polttoaine> getGasTypeFromDatabase()
        {
            List<Polttoaine> GasList = new List<Polttoaine>();
            using (SqlConnection connection = new SqlConnection(yhteysTiedot))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT ID, Polttoaineen_nimi FROM Polttoaine", connection))

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Polttoaine newPolttoaine = new Polttoaine();
                        newPolttoaine.ID = reader.GetInt32(0);
                        newPolttoaine.Polttoaineen_nimi = reader.GetString(1);
                        GasList.Add(newPolttoaine);
                    }
                }
                connection.Close();
            }
            return GasList;
        }

        // Creates car manufacturer objects with attributes and passes them on as a list
        public List<AutonMerkki> getAllAutos()
        {
            List<AutonMerkki> AutonMerkkiLista = new List<AutonMerkki>();
            using (SqlConnection connection = new SqlConnection(yhteysTiedot))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT ID, Merkki FROM AutonMerkki", connection))
                    
                        using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AutonMerkki newAutonMerkki = new AutonMerkki();
                        newAutonMerkki.ID = reader.GetInt32(0);
                        newAutonMerkki.Merkki = reader.GetString(1);
                        AutonMerkkiLista.Add(newAutonMerkki);
                    }
                }
                connection.Close();
            }
            return AutonMerkkiLista;
        }
    }
}
