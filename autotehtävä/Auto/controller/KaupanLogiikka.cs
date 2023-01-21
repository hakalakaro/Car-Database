using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Autokauppa.model;
using Autokauppa.view;

namespace Autokauppa.controller
{

    
    public class KaupanLogiikka
    {
        // Creates an object that we can use to call the methods in DatabaseHallinta class
        DatabaseHallinta dbModel = new DatabaseHallinta();

        // Tests database connection
        public bool TestDatabaseConnection()
        {
            bool doesItWork = dbModel.connectDatabase();
            return doesItWork;
            
            
        }

        // Saves car to database
        public bool saveAuto(Auto newAuto) 
        {
            bool didItGoIntoDatabase = dbModel.saveAutoIntoDatabase(newAuto);
            return didItGoIntoDatabase;
        }
                      
        // Gets all car models from database
        public List<AutonMalli> getAutoModels(int ID) {

            return dbModel.getAutoModelsByMakerId(ID);
        }

        // Gets all cars from database 
        public List<Auto> getAllCars()
        {
            return dbModel.getAllCarsFromDatabase();
        }

        // Deletes car from database
        public bool deleteCar(Auto selectedCar)
        {
            bool didItGoOutOfDatabase = dbModel.deleteCarFromDatabase(selectedCar);
            return didItGoOutOfDatabase;
        }

        // Get color id from database
        public List<Varit> getColor()
        {
            return dbModel.getColorFromDatabase();
        }

        // Get gas id from database
        public List<Polttoaine> getGasType()
        {
            return dbModel.getGasTypeFromDatabase();
        }

        // Get car manufacturers from database
        public List<AutonMerkki> getAutos()
        {
            return dbModel.getAllAutos();
        }
    }
}
