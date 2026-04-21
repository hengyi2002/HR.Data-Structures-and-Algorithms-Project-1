using Spectre.Console;

public static class TeamView
{
    public static void Display() {
        bool exit = false;

        while (!exit)
        {
            AnsiConsole.Clear();
            PrintTeams();
            
            SelectionPrompt<string> Prompt = new SelectionPrompt<string>()
                .Title($"What would you like to do?")
                .AddChoices("Create team", "Update team", "Delete team", "Exit");

            switch (AnsiConsole.Prompt(Prompt))
            {
                case "Create team":
                    CreateTeam();
                    break;
                case "Update team":
                    UpdateTeam();
                    break;
                case "Delete team":
                    DeleteTeam();
                    break;
                case "Exit":
                    exit = true;
                    break;
            }
        }
    }

    public static Team? SelectTeam(IMyCollection<Team> teams)
    {
        string[] options = [];

        foreach (Team team in teams)
        {
            options = options.Append(team.TeamName).ToArray();
        }
        options = options.Append("Exit").ToArray();

        string? selectedTeam = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Select a [green]Team[/]")
            .PageSize(10)
            .EnableSearch()
            .SearchPlaceholderText("Type to search team names...")
            .AddChoices(options));

        if (selectedTeam == "Exit") return null;

        return teams.Filter(t => t.TeamName == selectedTeam);
    }

    private static void PrintTeams()
    {
        Team[] teams = Program.TeamRepo.Read();
        User[] users = Program.UserRepo.Read();

        Table table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Team Name");
        table.AddColumn("Created By");

        foreach (Team team in teams)
        {
            User? creator = users.FirstOrDefault(u => u.ID == team.CreateUserID);

            table.AddRow(team.ID.ToString(), team.TeamName, creator?.Username ?? "Unknown");
        }

        AnsiConsole.Write(table);
    }

    private static void CreateTeam()
    {
        string teamName = AnsiConsole.Ask<string>("Enter the name of the new team:");
        Team newTeam = new Team
        {
            TeamName = teamName,
            CreateUserID = Program.appUser?.ID ?? 0
        };

        Program.TeamRepo.Add(newTeam);
        AnsiConsole.MarkupLine($"[green]Team '{teamName}' created successfully![/]");
    }

    private static void DeleteTeam()
    {
        Team? teamToDelete = null;
        bool Cancel = false;

        while (teamToDelete == null && !Cancel)
        {
            IMyCollection<Team> teams = Program.TeamRepo.Read();
            teamToDelete = SelectTeam(teams);

            if (teamToDelete == null)
            {
                Cancel = AnsiConsole.Confirm("No team selected. Do you want to cancel the delete operation?");
            }
        }

        if (teamToDelete != null)
        {
            Program.TeamRepo.Delete(teamToDelete);
            AnsiConsole.MarkupLine($"[red]Team '{teamToDelete.TeamName}' deleted successfully![/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]Team {teamToDelete?.TeamName ?? "Unknown"} not found.[/]");
        }
    }

    private static void UpdateTeam()
    {
        Team? teamToUpdate = null;
        bool Cancel = false;

        while (teamToUpdate == null && !Cancel)
        {
            IMyCollection<Team> teams = Program.TeamRepo.Read();
            teamToUpdate = SelectTeam(teams);

            if (teamToUpdate == null)
            {
                Cancel = AnsiConsole.Confirm("No team selected. Do you want to cancel the update operation?");
            }
        }

        if (teamToUpdate != null)
        {
            string newTeamName = AnsiConsole.Ask<string>($"Enter the new name for the team ({teamToUpdate.TeamName}):");
            teamToUpdate.TeamName = newTeamName;
            Program.TeamRepo.Update(teamToUpdate);
            AnsiConsole.MarkupLine($"[green]Team '{newTeamName}' updated successfully![/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]Team not found.[/]");
        }
    }
}