
using Tamagotchi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Tamagotchi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PetController : ControllerBase
  {
    public DatabaseContext db { get; set; } = new DatabaseContext();

    [HttpGet]
    public List<Pet> GetAllPets()
    {
      var pets = db.Pets.OrderBy(n => n.Name);
      return pets.ToList();
    }
    [HttpGet("{id}")]
    public Pet GetOnePet(int id)
    {
      var pet = db.Pets.FirstOrDefault(p => p.Id == id);
      return pet;
    }
    [HttpPost]
    public Pet CreateNewPet(Pet newPet)
    {
      db.Pets.Add(newPet);
      newPet.LastInteractedWith = DateTime.Now;
      db.SaveChanges();
      return newPet;
    }
    [HttpPatch("{id}/play")]
    public Pet Play(int id)
    {
      var playPet = db.Pets.FirstOrDefault(p => p.Id == id);
      var killed = playPet.IsDead;
      killed = playPet.KilledPet(killed);
      if (killed == true)
      {
        playPet.IsDead = true;
        playPet.DeathDate = DateTime.Now;
        playPet.LastInteractedWith = DateTime.Now;
        db.SaveChanges();
      }
      else if (killed == false)
      {
        playPet.LastInteractedWith = DateTime.Now;
        playPet.HappinessLevel = playPet.HappinessLevel + 5;
        playPet.HungerLevel = playPet.HungerLevel + 3;
        db.SaveChanges();
      }
      return playPet;
    }

    [HttpPatch("{id}/feed")]
    public Pet Feed(int id)
    {
      var feedPet = db.Pets.FirstOrDefault(p => p.Id == id);
      var killed = feedPet.IsDead;
      killed = feedPet.KilledPet(killed);
      if (killed == true)
      {
        feedPet.LastInteractedWith = DateTime.Now;
        feedPet.IsDead = true;
        feedPet.DeathDate = DateTime.Now;

        db.SaveChanges();
      }
      else if (killed == false)
      {
        feedPet.LastInteractedWith = DateTime.Now;
        feedPet.HappinessLevel = feedPet.HappinessLevel + 3;
        feedPet.HungerLevel = feedPet.HungerLevel - 3;
        db.SaveChanges();
      }
      return feedPet;
    }
    [HttpPatch("{id}/scold")]
    public Pet Scold(int id)
    {
      var scoldPet = db.Pets.FirstOrDefault(p => p.Id == id);
      var killed = scoldPet.IsDead;
      killed = scoldPet.KilledPet(killed);
      if (killed == true)
      {
        scoldPet.LastInteractedWith = DateTime.Now;
        scoldPet.IsDead = true;
        scoldPet.DeathDate = DateTime.Now;
        db.SaveChanges();
      }
      else if (killed == false)
      {
        scoldPet.LastInteractedWith = DateTime.Now;
        scoldPet.HappinessLevel = scoldPet.HappinessLevel - 5;
        db.SaveChanges();
      }
      return scoldPet;
    }
    [HttpDelete("{id}")]
    public ActionResult DeletePet(int id)
    {
      var deletePete = db.Pets.FirstOrDefault(p => p.Id == id);
      db.Pets.Remove(deletePete);
      db.SaveChanges();
      return Ok();
    }
  }

}

