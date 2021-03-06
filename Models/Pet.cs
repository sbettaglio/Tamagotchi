using System;
using System.Linq;

namespace Tamagotchi.Models
{
  public class Pet
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Birthday { get; set; }
    public DateTime DeathDate { get; set; }
    public DateTime LastInteractedWith { get; set; }
    public int HungerLevel { get; set; }
    public int HappinessLevel { get; set; }
    public bool IsDead { get; set; }

    public virtual bool KilledPet(bool killed)
    {
      Random rand = new Random();
      //var dead = 0;

      if (rand.Next(1, 101) <= 10)
      {
        //dead++;
        killed = true;
      }
      return killed;
    }
    public virtual bool NeglectedPet(DateTime lastInteractedWith)
    {
      var dead = false;
      var diff = DateTime.Now;
      var neglected = diff.Subtract(lastInteractedWith).TotalDays;
      if (neglected > 3)
      {
        dead = true;
        return dead;
      }

      return dead;

    }
  }
}
