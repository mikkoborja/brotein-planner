using BroTeinPlanner.Models;
using BroTeinPlanner.Utils;

using static ConsoleUI;
using ConsoleTables;

namespace BroTeinPlanner.Services {
    public class ClientService {
        private List<Client> clients;
        private string filePath;

        public ClientService(string path) {
            filePath = path;
            clients = JsonFileHandler.Load<Client>(filePath);
        }

        public string[] GetClients() {
            string[] clientList = new string[clients.Count];
            for (int i = 0; i < clients.Count; i++) {
                clientList[i] = clients[i].FirstName;
            }

            return clientList;
        }

        public string GetClientById(int id) {
            return clients[id - 1].FirstName;
        }

        public void DisplayClients() {
            
        }

        public void ViewClient() {
            var table = new ConsoleTable("ID", "First Name", "Last Name", "Client Goal", "Gender", "Age", "Weight", "Height", "Weekly Goal", "Email Address");
            const int MAX_LENGTH = 10;

            for (int i = 0; i < clients.Count; i++) {
                int id = i + 1;
                string firstName = StringFormatter.Truncate(clients[i].FirstName, MAX_LENGTH);
                string lastName = StringFormatter.Truncate(clients[i].LastName, MAX_LENGTH);
                string goal = StringFormatter.Truncate(clients[i].Goal, MAX_LENGTH);
                string gender = StringFormatter.Truncate(clients[i].Gender, MAX_LENGTH);
                string age = StringFormatter.Truncate(clients[i].Age.ToString(), MAX_LENGTH);
                string weight = StringFormatter.Truncate(clients[i].Weight.ToString(), MAX_LENGTH);
                string height = StringFormatter.Truncate(clients[i].Height.ToString(), MAX_LENGTH);
                string weeklyGoal = StringFormatter.Truncate(clients[i].WeeklyGoal, MAX_LENGTH);
                string email = StringFormatter.Truncate(clients[i].Email, MAX_LENGTH);

                table.AddRow(id, firstName, lastName, goal, gender, age, weight, height, weeklyGoal, email);  
            }

            Console.WriteLine($"Total Clients: {clients.Count}");
            table.Write(Format.Alternative);
        }

        public void Add(Client client) {
            clients.Add(client);
            JsonFileHandler.Save(filePath, clients);
            PrintColoredText("Client Information Saved Successfully", ConsoleColor.Green);
        }
    }
}
