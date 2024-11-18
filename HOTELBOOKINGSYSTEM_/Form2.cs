using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HOTELBOOKINGSYSTEM_
{
    public partial class Form2 : Form
    {
        private List<Client> clients;
        public Form2()
        {
            InitializeComponent();
            clients = new List<Client>(); // Initialize the client list
            InitializeDataGridView();
        }
        public class Client
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }

            public Client(string name, string address, string phone)
            {
                Name = name;
                Address = address;
                Phone = phone;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        private void AddClient(string name, string address, string phone)
        {
            // Create a new client and add it to the list
            Client newClient = new Client(name, address, phone);
            clients.Add(newClient);
        }
        private void InitializeDataGridView()
        {
            // Add columns to the DataGridView
            dataGridViewClients.Columns.Add("Name", "Name");
            dataGridViewClients.Columns.Add("Address", "Address");
            dataGridViewClients.Columns.Add("Phone", "Phone");
        }
        private void btnAddClient_Click(object sender, EventArgs e)
        {
            // Get data from the form fields
            string clientName = txtClientName.Text;
            string clientAddress = txtClientAddress.Text;
            string clientPhone = txtClientPhone.Text;
            AddClient(clientName, clientAddress, clientPhone);

            // Get data from the form fields

            // Add the client to the list


            // Optionally, show a message that the client was added
            MessageBox.Show("Client added successfully!");

            // Clear the textboxes after adding
            txtClientName.Clear();
            txtClientAddress.Clear();
            txtClientPhone.Clear();

            // Display the updated list of clients in the ListBox
            DisplayClients();
        }
        private void DisplayClients()
        {
            // Clear the ListBox before adding the new clients
            dataGridViewClients.Rows.Clear();

            // Add each client to the ListBox
            foreach (var client in clients)
            {
                dataGridViewClients.Rows.Add(client.Name, client.Address, client.Phone);
            }

        }
    }
}
