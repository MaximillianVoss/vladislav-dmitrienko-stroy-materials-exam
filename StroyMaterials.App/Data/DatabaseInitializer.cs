using Microsoft.Data.Sqlite;

namespace StroyMaterials.App;

internal static class DatabaseInitializer
{
    public static void EnsureDatabase()
    {
        Directory.CreateDirectory(AppPaths.DataDirectory);
        Directory.CreateDirectory(AppPaths.ProductImagesDirectory);

        if (File.Exists(AppPaths.DatabasePath))
        {
            return;
        }

        var schemaPath = Path.Combine(AppPaths.DatabaseDirectory, "create_schema.sql");
        var seedPath = Path.Combine(AppPaths.DatabaseDirectory, "seed_data.sql");
        if (!File.Exists(schemaPath) || !File.Exists(seedPath))
        {
            throw new FileNotFoundException("Не найдены SQL-скрипты для создания локальной базы данных.");
        }

        using var connection = new SqliteConnection($"Data Source={AppPaths.DatabasePath}");
        connection.Open();
        ExecuteScript(connection, File.ReadAllText(schemaPath));
        ExecuteScript(connection, File.ReadAllText(seedPath));
    }

    public static string CreateBackup(string targetDirectory)
    {
        Directory.CreateDirectory(targetDirectory);
        var fileName = $"UserXX_РКБД_Дмитриенко_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
        var targetPath = Path.Combine(targetDirectory, fileName);
        File.Copy(AppPaths.DatabasePath, targetPath, overwrite: true);
        return targetPath;
    }

    private static void ExecuteScript(SqliteConnection connection, string script)
    {
        using var command = connection.CreateCommand();
        command.CommandText = script;
        command.ExecuteNonQuery();
    }
}
