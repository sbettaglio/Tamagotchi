
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
      var lastInteractedWith = pet.LastInteractedWith;
      var neglectedPet = pet.IsDead;
      neglectedPet = pet.NeglectedPet(lastInteractedWith);
      if (neglectedPet == true)
      {
        neglectedPet = true;
        lastInteractedWith = DateTime.Now;
        pet.DeathDate = DateTime.Now;
        db.SaveChanges();
      }
      else if (neglectedPet == false)
      {
        lastInteractedWith = DateTime.Now;
        db.SaveChanges();
      }
      return pet;
    }
    [HttpGet("alive")]
    public List<string> AlivePets()
    {
      var alivePets = db.Pets.Where(p => p.IsDead == false).ToList();
      var alivePetList = new List<string>();
      foreach (var pet in alivePets)
      {
        alivePetList.Add(pet.Name);
      }
      return alivePetList;

    }
    [HttpPost]
    public Pet CreateNewPet(Pet newPet)
    {
      db.Pets.Add(newPet);
      var killed = newPet.IsDead;
      killed = newPet.KilledPet(killed);
      if (killed == true)
      {
        newPet.IsDead = true;
        newPet.DeathDate = DateTime.Now;

        db.SaveChanges();
      }
      else if (killed == false)
      {


        db.SaveChanges();
      }
      return newPet;
    }
    [HttpPatch("{id}/play")]
    public Pet Play(int id)
    {
      var playPet = db.Pets.FirstOrDefault(p => p.Id == id);
      var lastInteractedWith = playPet.LastInteractedWith;
      var neglectedPet = playPet.IsDead;
      neglectedPet = playPet.NeglectedPet(lastInteractedWith);
      if (neglectedPet == true)
      {
        playPet.IsDead = true;
        playPet.DeathDate = DateTime.Now;
        playPet.LastInteractedWith = DateTime.Now;
        db.SaveChanges();
      }
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
      var lastInteractedWith = feedPet.LastInteractedWith;
      var neglectedPet = feedPet.NeglectedPet(lastInteractedWith);
      if (neglectedPet == true)
      {
        feedPet.IsDead = true;
        feedPet.DeathDate = DateTime.Now;
        feedPet.LastInteractedWith = DateTime.Now;
        db.SaveChanges();
      }
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
      var lastInteractedWith = scoldPet.LastInteractedWith;
      var neglectedPet = scoldPet.NeglectedPet(lastInteractedWith);
      if (neglectedPet == true)
      {
        scoldPet.IsDead = true;
        scoldPet.DeathDate = DateTime.Now;
        scoldPet.LastInteractedWith = DateTime.Now;
        db.SaveChanges();
      }
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

