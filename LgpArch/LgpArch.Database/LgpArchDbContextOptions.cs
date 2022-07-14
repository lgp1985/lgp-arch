namespace LgpArch.Database;

/// <summary>
/// Represents SQL database options
/// </summary>
public class LgpArchDbContextOptions
{
    /// <summary>
    /// Define root level section on which appsettings.json will store data.
    /// </summary>
    public static readonly string Section = "SqlServer";

    /// <summary>
    /// Gets or sets the DatabaseConnection connection string
    /// </summary>
    public string ConnectionString { get; set; }
}
