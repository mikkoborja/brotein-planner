using BroTeinPlanner.Models;
using BroTeinPlanner.Utils;

using static ConsoleUI;

namespace BroTeinPlanner.Services {
    public class UserService {
        private List<User> users;
        private string filePath;

        public UserService(string path) {
            filePath = path;
            users = JsonFileHandler.Load<User>(filePath);
        }

        public bool Login(string username, string password) {
            var user = users.Find(u => u.Username == username);
            if (user != null && user.Password == password) {
                return true;
            } else {
                return false;
            }
        }

        public void Register(User user) {
            if (users.Exists(u => u.Username == user.Username)) {
                PrintColoredText("Username already exists.", ConsoleColor.Red);
            } else {
                users.Add(user);
                JsonFileHandler.Save(filePath, users);
                PrintColoredText("Account Registered Successfully", ConsoleColor.Green);
            }
        }
    }
}