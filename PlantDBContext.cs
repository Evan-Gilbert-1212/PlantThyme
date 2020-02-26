using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PlantThyme
{
  public partial class PlantDBContext : DbContext
  {
    public DbSet<Plant> Plants { get; set; }

    public void AddNewPlant(string speciesToPlant, string locationToPlant, int lightNeeded, int waterNeeded)
    {
      var plantToAdd = new Plant()
      {
        Species = speciesToPlant,
        LocatedPlanted = locationToPlant,
        LightNeeded = lightNeeded,
        WaterNeeded = waterNeeded
      };

      Plants.Add(plantToAdd);
      SaveChanges();
    }

    public void RemovePlant(int plantIDToRemove)
    {
      var plantToRemove = Plants.FirstOrDefault(p => p.ID == plantIDToRemove);

      Plants.Remove(plantToRemove);
      SaveChanges();
    }

    public void ViewAllPlants()
    {
      Console.WriteLine("Here are all the plants in your garden:");
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
      Console.WriteLine("| Species         | Location        | Date Planted         | Last Watered On      | Light Needed    | Water Needed    |");
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

      var orderedPlantList = Plants.OrderByDescending(p => p.LocatedPlanted);

      foreach (var plant in orderedPlantList)
      {
        var formatSpecies = String.Format("{0,-15}", plant.Species);
        var formatLocation = String.Format("{0,-15}", plant.LocatedPlanted);
        var formatPlantDate = String.Format("{0,-15}", plant.PlantedDate);
        var formatWateredDate = String.Format("{0,-15}", plant.LastWateredDate);
        var formatLight = String.Format("{0,-15}", plant.LightNeeded);
        var formatWater = String.Format("{0,-15}", plant.WaterNeeded);

        Console.WriteLine($"| {formatSpecies} | {formatLocation} | {formatPlantDate} | {formatWateredDate} | {formatLight} | {formatWater} |");
      }

      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
      Console.WriteLine();
    }

    public void DisplayPlantsToWater()
    {
      Console.WriteLine("Here are all the plants that can be watered:");
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
      Console.WriteLine("| ID             | Species         | Last Watered On      | Water Needed    |");
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

      foreach (var plant in Plants)
      {
        var formatPlantID = String.Format("{0,-14}", plant.ID);
        var formatSpecies = String.Format("{0,-15}", plant.Species);
        var formatWateredDate = String.Format("{0,-15}", plant.LastWateredDate);
        var formatWater = String.Format("{0,-15}", plant.WaterNeeded);

        Console.WriteLine($"| {formatPlantID} | {formatSpecies} | {formatWateredDate} | {formatWater} |");
      }

      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
      Console.WriteLine();
    }

    public void DisplayLocationSummary(string locationToDisplay)
    {
      Console.WriteLine($"Here are all the plants located in the {locationToDisplay}:");
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
      Console.WriteLine("| Species         | Location        | Date Planted         | Last Watered On      | Light Needed    | Water Needed    |");
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

      var locationPlantList = Plants.Where(p => p.LocatedPlanted == locationToDisplay);

      foreach (var plant in locationPlantList)
      {
        var formatSpecies = String.Format("{0,-15}", plant.Species);
        var formatLocation = String.Format("{0,-15}", plant.LocatedPlanted);
        var formatPlantDate = String.Format("{0,-15}", plant.PlantedDate);
        var formatWateredDate = String.Format("{0,-15}", plant.LastWateredDate);
        var formatLight = String.Format("{0,-15}", plant.LightNeeded);
        var formatWater = String.Format("{0,-15}", plant.WaterNeeded);

        Console.WriteLine($"| {formatSpecies} | {formatLocation} | {formatPlantDate} | {formatWateredDate} | {formatLight} | {formatWater} |");
      }

      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
      Console.WriteLine();
    }

    public void WaterPlant(int plantIDToWater)
    {
      Plants.FirstOrDefault(p => p.ID == plantIDToWater).LastWateredDate = DateTime.Now;
      SaveChanges();
    }

    public void ViewUnwateredPlants()
    {
      Console.WriteLine("Here are all the plants that have not been watered today:");
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
      Console.WriteLine("| Species         | Location        | Date Planted         | Last Watered On      | Light Needed    | Water Needed    |");
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

      var unwateredPlants = Plants.Where(p => p.LastWateredDate < DateTime.Today).ToList();

      foreach (var plant in unwateredPlants)
      {
        var formatSpecies = String.Format("{0,-15}", plant.Species);
        var formatLocation = String.Format("{0,-15}", plant.LocatedPlanted);
        var formatPlantDate = String.Format("{0,-15}", plant.PlantedDate);
        var formatWateredDate = String.Format("{0,-15}", plant.LastWateredDate);
        var formatLight = String.Format("{0,-15}", plant.LightNeeded);
        var formatWater = String.Format("{0,-15}", plant.WaterNeeded);

        Console.WriteLine($"| {formatSpecies} | {formatLocation} | {formatPlantDate} | {formatWateredDate} | {formatLight} | {formatWater} |");
      }

      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
      Console.WriteLine();
    }

    public PlantDBContext()
    {
    }

    public PlantDBContext(DbContextOptions<PlantDBContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        //#warning To protect potentially sensitive information in your connection string, 
        //you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 
        //for guidance on storing connection strings.
        optionsBuilder.UseNpgsql("server=localhost;database=PlantDatabase");
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
