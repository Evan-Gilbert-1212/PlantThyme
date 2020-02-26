using System;

namespace PlantThyme
{
  public class Plant
  {
    //properties of plant
    public int ID { get; set; }
    public string Species { get; set; }
    public string LocatedPlanted { get; set; }
    public DateTime PlantedDate { get; set; } = DateTime.Now;
    public DateTime LastWateredDate { get; set; } = DateTime.Now;
    public int LightNeeded { get; set; }
    public int WaterNeeded { get; set; }
  }
}