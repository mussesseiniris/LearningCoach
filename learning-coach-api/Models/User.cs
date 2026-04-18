using System.ComponentModel.DataAnnotations;
namespace LearningCoachAPI.Models;


public class User
{
     public int Id{get; set;}
     [MaxLength(256)]
     public required string Name{get; set;}
     
     public List<Subject> Subjects {get;set;} = new();

     [MaxLength(256)]
     public required string PasswordHash { get; set; }
     [MaxLength(256)]
     public required string Email { get; set; }

}
