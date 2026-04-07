using Spectre.Console;
using Spectre.Console.Rendering;

public class MenuView()
{
    public static bool Main()
    {
        AnsiConsole.Clear();

        AnsiConsole.Write(Align.Center(Logo()));

        switch(AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"Good {TimeGreeting()}, What would you like to do today?")
                .AddChoices("View currently doing tasks", "View kanban", "View specific boards", "View all tasks in system", "exit program")))
        {
            case "View currently doing tasks":
                TaskView.DisplayCurrentlyDoing();
                break;
            case "View kanban":
                // TaskView.DisplayKanBan();
                break;
            case "View specific boards":
                TaskView.SelectBoard();
                break;
            case "View all tasks in system":
                TaskView.DisplayAll();
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
}