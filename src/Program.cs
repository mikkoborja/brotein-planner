using static Constants;
using static ConsoleUI;

using BroTeinPlanner.Models;
using BroTeinPlanner.Services;
using Mscc.GenerativeAI;
using System.Threading.Tasks;

class Program { 
    static async Task Main() { 
        Console.WriteLine(PROGRAM_LOGO);
        Console.WriteLine("(1) Login, (2) Register Administration Account, (3) Insert Client Information, (4) View Client, (5) Meal, (6) Workout Plans");
        Console.Write("Please select a task to continue: ");
        string action = Console.ReadLine();

        var userService = new UserService(ACCOUNTS_FILE_PATH);
        var clientService = new ClientService(CLIENTS_FILE_PATH);

        var googleAI = new GoogleAI(apiKey: GOOGLE_API_KEY);
        var model = googleAI.GenerativeModel(model: Model.Gemini20Flash);

        if(string.IsNullOrEmpty(GOOGLE_API_KEY)) {
            Console.WriteLine("ERROR: Google API Key is not set");
            return; 
        }

        Console.WriteLine("Enter your prompt for Gemini AI (e.g., 'Write a short story about a magic backpack'):");
        string prompt = Console.ReadLine(); 

        var response = await model.GenerateContent(prompt);
        
        Console.WriteLine("\nGemini AI Response:");
        Console.WriteLine(response.Text);

        switch (action) {
            case "1":
                PrintColoredText("Login Administration Account", ConsoleColor.Cyan);

                string inputUsername = Prompt("Enter Username");
                string inputPassword = Prompt("Enter Password");
                                                                                                                                                                
                if (userService.Login(inputUsername, inputPassword)) {
                    PrintColoredText("Logged in Successfully", ConsoleColor.Green);
                } else {
                    PrintColoredText("Logged in Successfully", ConsoleColor.Red);
                }
                break;
            case "2":
                PrintColoredText("Register Administration Account", ConsoleColor.Cyan);
    
                string name = Prompt("Enter Your Name");
                string username = Prompt("Enter Your Username");
                string password = Prompt("Enter Your Password");

                userService.Register(new User { Name = name, Username = username, Password = password });
                break;
            case "3":
                string firstName = Prompt("First Name");
                string lastName = Prompt("Last Name");
                int goal = Convert.ToInt32(Prompt("What are your goals? (1) Lose weight, (2) Gain muscle, (3) Lose weight and Gain muscle"));
                string gender = Prompt("Gender");
                int age = Convert.ToInt32(Prompt("Age"));
                double weight = Convert.ToDouble(Prompt("How much do you weight in kg"));
                double height = Convert.ToDouble(Prompt("What is your height in cm"));
                int weeklyGoal = Convert.ToInt32(Prompt("What is your weekly goal? (1) Gain 0.25 kg per week, (2) Gain 0.5 kg per week"));
                string email = Prompt("Almost done! What is your email address");

                string userGoal = GOALS[goal - 1];
                string user_weeklyGoal = WEEKLY_GOAL[weeklyGoal - 1]
    
                Console.WriteLine("");
                Console.WriteLine("Congrats!, Your custom plan is ready and you're one step closer to your fitness goals.");
                Console.WriteLine("Your daily net goal is: 2,600 Calories, and 324 Protein");

                clientService.Add(new Client { FirstName = firstName, LastName = lastName, Goal = userGoal, Gender = gender, Age = age, Weight = weight, Height = height, WeeklyGoal = weeklyGoal, Email = email });
                break;
            case "4":
                clientService.ViewClient();
                break;
            case "5":
                Console.WriteLine("Meal: ");
                break;
            case "6":
                // TODO: Create display client list void
                Console.WriteLine("Who's client are you working with?");
                string[] clients = clientService.GetClients();
                for (int i = 0; i < clients.Length; i++) {
                    string order = Console.ReadLine();
                    Console.WriteLine($"({i + 1}). {clients[i]}");
                }
                int clientId = Convert.ToInt32(Prompt("Enter Client ID"));
                Console.WriteLine(clientService.GetClientById(clientId));
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        }
    }

    // This Display the prompt message that the user's input just like the string while the console.write is converted into console readline.
    static string Prompt(string message) {
        Console.Write($"{message}: ");
        return Console.ReadLine();
    }
}