namespace LearningCoachAPI.Models;


public class User
{
    
     public int Id{get; set;}
     public required string Name{get; set;}
     
     public List<Subject> Subjects {get;set;} = new();
    
}