using Spectre.Console;
using Spectre.Console.Rendering;

public class MenuView()
{
    public static bool Main()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(Align.Center(Logo()));

        SelectionPrompt<string> Prompt = new SelectionPrompt<string>()
            .Title($"Good {TimeGreeting()}, What would you like to do today?")
            .AddChoices("View currently doing tasks", "View kanban", "View specific boards", "View all tasks in system", "exit program");

        if (Program.appUser is not null && Program.appUser.Role.Name == "Admin")
        {
            Prompt.AddChoice("Admin options");
        }

        switch(AnsiConsole.Prompt(Prompt))
        {
            case "View currently doing tasks":
                TaskView.DisplayCurrentlyDoing();
                break;
            case "View kanban":
                // TaskView.PrintTasksKanBan();
                break;
            case "View specific boards":
                TaskView.SelectBoard();
                break;
            case "View all tasks in system":
                TaskView.DisplayAll();
                break;
            case "Admin options":
                AdminMenu();
                break;
            case "exit program":
                return true;
        }

        return false;
    }

    public static Panel Logo()
    {
        string logo =
            " __    __                      __                           \n" +
            "/  |  /  |                    /  |                          \n" +
            "$$ | /$$/   ______   _______  $$ |____    ______   _______  \n" +
            "$$ |/$$/   /      \\ /       \\ $$      \\  /      \\ /       \\ \n" +
            "$$  $$<    $$$$$$  |$$$$$$$  |$$$$$$$  | $$$$$$  |$$$$$$$  |\n" +
            "$$$$$  \\   /    $$ |$$ |  $$ |$$ |  $$ | /    $$ |$$ |  $$ |\n" +
            "$$ |$$  \\ /$$$$$$$ |$$ |  $$ |$$ |__$$ |/$$$$$$$ |$$ |  $$ |\n" +
            "$$ | $$  |$$    $$ |$$ |  $$ |$$    $$/ $$    $$ |$$ |  $$ |\n" +
            "$$/   $$/  $$$$$$$/ $$/   $$/ $$$$$$$/   $$$$$$$/ $$/   $$/ ";

        Panel panel = new Panel(logo).Expand();
        panel.Header($"HR.DATA STRUCTURES AND ALGORITHMS - Project One", Justify.Center);

        return panel;
    }

    public static int SelectImplementation()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(Align.Center(Logo()));

        SelectionPrompt<string> Prompt = new SelectionPrompt<string>()
            .Title($"Which implementation would you like to use?)")
            .AddChoices("Hashmap", "Linked List", "Double Linked List");

        switch(AnsiConsole.Prompt(Prompt))
        {
            case "Hashmap":
                return 0;
            case "Linked List":
                return 1;
            case "Double Linked List":
                return 2;
            default:
                return 0;
        }
    }

    public static User? Login(int attempts = 0)
    {
        AnsiConsole.Clear();

        AnsiConsole.Write(Align.Center(Logo()));

        string username = AnsiConsole.Ask<string>("Please enter your username:");
        string password = AnsiConsole.Prompt(
            new TextPrompt<string>("Please enter your password:")
                .PromptStyle("red")
                .Secret());

        // if (UserController.Login(username, password))
        if (username == "admin" && password == "password")
        {
            return new User { FirstName = username, Role = new Role { Name = "Admin" } };
        } else {
            if (attempts < 2)
            {
                AnsiConsole.MarkupLine("[red]Invalid username or password. Please try again.[/]");
                Thread.Sleep(2500);
                return null;
            }
            AnsiConsole.MarkupLine("[red]Too many failed login attempts. Exiting program.[/]");
            Thread.Sleep(2500);
            return null;
        }
    }

    public static void GoodByeMessage()
    {
        //Create a new panel in which we will add our goodbye message
        Panel panel = new Panel(new Text("Thank you for using the program\n\nNow exiting program...")).Expand();
        panel.Header($"SYSTEM", Justify.Center);

        //Print the new UI
        AnsiConsole.Write(panel);
    }

    private static string TimeGreeting()
    {
        if (DateTime.UtcNow.Hour < 12) {
            return "Morning";
        } else if (DateTime.UtcNow.Hour < 17) {
            return "Afternoon";
        } else {
            return "Evening";
        }
    }

    private static void AdminMenu()
    {
        SelectionPrompt<string> Prompt = new SelectionPrompt<string>()
                .Title($"Admin options - What would you like to do?")
                .AddChoices("View users", "View teams", "Exit");

        switch(AnsiConsole.Prompt(Prompt))
        {
            case "View users":
                UserView.Display();
                break;
            case "View teams":
                TeamView.Display();
                break;
            case "Exit":
                break;
        }
    }
}