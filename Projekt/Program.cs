using Microsoft.EntityFrameworkCore;
using Projekt.Helpers;
using Projekt.Models;
using Projekt.Persistance;

class Program
{
    static void Main(string[] args)
    {
        // konfiguracja bazy danych SQLite
        // wymagana instalacja pakietu Microsoft.EntityFrameworkCore.Sqlite
        var sqliteConnectionString = "Data Source=EntityFrameworkRepository.db";
        var options = new DbContextOptionsBuilder<ProjektDbContext>()
            .UseSqlite(sqliteConnectionString)
            .Options;

        using (var unitOfWork = new ProjektUnitOfWork(new ProjektDbContext(options)))
        {
           

        }
    }
}