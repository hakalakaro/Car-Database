using Autokauppa.controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autokauppa.model;
using System.Data.SqlClient;

// READ BEFORE USE!!! 
//                    There is a problem populating Listbox1 with car objects from database. There seems to appear as many entries of the same ID for each --
//                    model that the corresponding manufacturer has. For example if the car with ID #5 has a car manufacturer that has 4 models available, --
//                    the listbox is populated with 5 entries of the same ID all with different models. I don't know what causes this. --
//                    When you select a car manufacturer, and check model list, it shows models for the previous car manufacturer in the database. --
//                    For example BMW shows Audi's models for some reason. Saving cars and deleting cars from the database work fine though. --
                    
namespace Autokauppa.view
{
    public partial class MainMenu : Form
    {
        // Create class objects so we can use the methods in their respective classes.
        DatabaseHallinta registerHandler2 = new DatabaseHallinta();
        KaupanLogiikka registerHandler = new KaupanLogiikka();
        Auto newAuto = new Auto();
        
        public MainMenu()
        {
            
            InitializeComponent();
        }
        
        // Form load, these actions are executed on program start
        private void MainMenu_Load(object sender, EventArgs e)
        {
            // populate listbox1 with all cars from database
            listBox1.DataSource = registerHandler.getAllCars(); 
            // populate combobox3 with colors from database
            comboBox3.DataSource = registerHandler.getColor(); 
            comboBox3.DisplayMember = "Varin_nimi";
            comboBox3.ValueMember = "ID";
            // populate combobox4 with gas types from database
            comboBox4.DataSource = registerHandler.getGasType(); 
            comboBox4.DisplayMember = "Polttoaineen_nimi";
            comboBox4.ValueMember = "ID";
            //populate combobox5 with car manufacturers from database
            comboBox5.DataSource = registerHandler.getAutos();
            comboBox5.DisplayMember = "Merkki";
            comboBox5.ValueMember = "ID";
            comboBox5.SelectedIndex = 0; // set the default index 
        }
       
        // "About" -> "Test connection" button, tests the connection to database, writes in console accordingly
            private void testDBConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            registerHandler.TestDatabaseConnection();
                                    
        }

        // "File" -> "Exit" button, quits the application.
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            registerHandler2.disconnectDatabase();
            Application.Exit();
        }

        // "Tallenna" button, enters the information into database
        private void button1_Click(object sender, EventArgs e)
        {                      
            newAuto.Hinta = Convert.ToDecimal(textBox3.Text);
            newAuto.Rekisteri_paivamaara = dateTimePicker1.Value;
            newAuto.Moottorin_tilavuus = Convert.ToDecimal(textBox2.Text);
            newAuto.Mittarilukema = Convert.ToInt32(textBox1.Text);
            newAuto.AutonMerkkiID = (int)comboBox5.SelectedValue;
            newAuto.AutonMalliID = (int)comboBox2.SelectedValue;
            newAuto.VaritID = (int)comboBox3.SelectedValue;
            newAuto.PolttoaineID = (int)comboBox4.SelectedValue;

            registerHandler.saveAuto(newAuto);
            
        }

        
        

        // "Uusi Tietue" button, clears the form
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox2.DataSource = null;
            comboBox2.Items.Clear();
            comboBox3.ResetText();
            comboBox4.ResetText();
            dateTimePicker1.Value = DateTime.Now;


        }

        // "Poista" button, deletes selected car in listbox1 from database
        private void button2_Click(object sender, EventArgs e)
        {
            Auto selectedCar = (Auto)listBox1.SelectedItem;
            registerHandler.deleteCar(selectedCar);
            listBox1.Update(); // update doesn't seem to do anything
        }

        // "Previous" button, selects previous car in listbox1
        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 0)
            {
                listBox1.SelectedIndex = listBox1.SelectedIndex - 1;
            }
        }
        
        // "Seuraava" button, selects next car in listbox1
        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < listBox1.Items.Count - 1)
            {
                listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
            }
        }
        
        // Populate automodel combobox2 according to seleciton from automanufacturer combobox
        private void comboBox5_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(comboBox5.SelectedIndex);

            comboBox2.DataSource = null;
            comboBox2.Items.Clear();
            comboBox2.DataSource = registerHandler.getAutoModels(ID);
            comboBox2.DisplayMember = "Auton_mallin_nimi";
            comboBox2.ValueMember = "ID";
        }

        // Select last index in listbox1
        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        // Select first index in listbox1
        private void button7_Click(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = 0;
        }
    }
}
