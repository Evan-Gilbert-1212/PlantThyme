using System;
using System.Linq;

namespace PlantThyme
{
  class Program
  {
    static void Main(string[] args)
    {
      var isRunning = true;

      var plantDb = new PlantDBContext();

      MyClearConsole();

      while (isRunning)
      {
        Console.WriteLine("What would you like to do in your garden?");
        Console.WriteLine("(VIEW ALL) plants in your garden");
        Console.WriteLine("(PLANT) a new plant in your garden");
        Console.WriteLine("(REMOVE) a plant from your garden");
        Console.WriteLine("(WATER) your plants");
        Console.WriteLine("(VIEW UNWATERED) plants in your garden");
        Console.WriteLine("View the (LOCATION) summary for your garden");
        Console.WriteLine("(QUIT) the program");

        var userInput = Console.ReadLine().ToLower();

        while (userInput != "view all" && userInput != "plant" && userInput != "remove" && userInput != "water" &&
               userInput != "view unwatered" && userInput != "location" && userInput != "quit")
        {
          Console.WriteLine("Please enter a valid command. Valid command options are:");
          Console.WriteLine("(VIEW ALL), (PLANT), (REMOVE), (WATER), (VIEW UNWATERED), (LOCATION) and (QUIT).");

          userInput = Console.ReadLine().ToLower();
        }

        switch (userInput)
        {
          case "plant":
            Console.WriteLine("What species of plant are your planting today?");
            var speciesToPlant = Console.ReadLine();

            Console.WriteLine("What location are you planting the new plant in?");
            var locationToPlant = Console.ReadLine();

            Console.WriteLine("How much light does this plant need?");
            var lightNeeded = Console.ReadLine();
            var intLightNeeded = 0;

            while (!Int32.TryParse(lightNeeded, out intLightNeeded))
            {
              Console.WriteLine("Please enter a valid value. Light needed should be a whole number.");
              lightNeeded = Console.ReadLine();
            }

            Console.WriteLine("How much water does this plant need?");
            var waterNeeded = Console.ReadLine();
            var intWaterNeeded = 0;

            while (!Int32.TryParse(waterNeeded, out intWaterNeeded))
            {
              Console.WriteLine("Please enter a valid value. Light needed should be a whole number.");
              waterNeeded = Console.ReadLine();
            }

            plantDb.AddNewPlant(speciesToPlant, locationToPlant, intLightNeeded, intWaterNeeded);

            MyClearConsole();

            Console.WriteLine("Plant added successfully!");
            Console.WriteLine();

            break;
          case "view all":
            MyClearConsole();

            plantDb.ViewAllPlants();

            break;
          case "view unwatered":
            MyClearConsole();

            plantDb.ViewUnwateredPlants();

            break;
          case "remove":
            Console.WriteLine("What species do you want to remove from your garden?");
            var plantToRemove = Console.ReadLine();

            while (plantDb.Plants.Count(p => p.Species == plantToRemove) == 0)
            {
              Console.WriteLine("Species not found. Please enter a valid species to remove.");
              plantToRemove = Console.ReadLine();
            }

            var plantIDToRemove = plantDb.Plants.First(p => p.Species == plantToRemove).ID;

            plantDb.RemovePlant(plantIDToRemove);

            MyClearConsole();

            Console.WriteLine("Species removed successfully!");
            Console.WriteLine();

            break;
          case "water":
            plantDb.DisplayPlantsToWater();

            Console.WriteLine("Please enter the ID of the plant you wish to water.");
            var plantIDToWater = Console.ReadLine();
            var intPlantIDToWater = 0;

            while (!Int32.TryParse(plantIDToWater, out intPlantIDToWater) || plantDb.Plants.Count(p => p.ID == intPlantIDToWater) == 0)
            {
              Console.WriteLine("Please enter a valid value. Plant ID should be a number listed in the table above.");
              plantIDToWater = Console.ReadLine();
            }

            plantDb.WaterPlant(intPlantIDToWater);

            MyClearConsole();

            Console.WriteLine("Plant watered successfully!");
            Console.WriteLine();

            break;
          case "location":
            Console.WriteLine("What location would you like to view plants for?");

            var locationToDisplay = Console.ReadLine();

            MyClearConsole();

            plantDb.DisplayLocationSummary(locationToDisplay);

            break;
          case "quit":
            isRunning = false;

            break;
        }
      }

      Console.WriteLine();
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
      Console.WriteLine("Have a wonderful day! Goodbye!");
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
    }

    static void MyClearConsole()
    {
      Console.Clear();
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~");
      Console.WriteLine("Welcome to your Garden!");
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~");
      Console.WriteLine();
    }
  }
}
