using Spectre.Console;

public static class RoleView
{
    public static Role? SelectRole(Role[] roles)
    {
        string[] options = [];

        foreach (Role role in roles)
        {
            options = options.Append(role.Name).ToArray();
        }
        options = options.Append("Exit").ToArray();

        string? selectedRole = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Select a [green]Role[/]")
            .PageSize(10)
            .EnableSearch()
            .SearchPlaceholderText("Type to search role names...")
            .AddChoices(options));

        if (selectedRole == "Exit") return null;

        return roles.FirstOrDefault(r => r.Name == selectedRole);
    }
}