using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

public class DatabaseContext : DbContext {
    private readonly string _connectionString;

    public DatabaseContext(string connectionString) {
        if (string.IsNullOrEmpty(connectionString)) {
            throw new ArgumentException("Connection string cannot be null or empty.", nameof(connectionString));
        }
        _connectionString = connectionString;
    }

    public virtual MySqlConnection GetConnection() {
        return new MySqlConnection(_connectionString);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
    }
}