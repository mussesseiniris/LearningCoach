namespace LearningCoachAPI.Models;

public class Subject
{

    public int Id{get;set;}
    public required string Name{get;set;}  

    public int Duration{get;set;}  
    
    public DateTime Deadline{get;set;}
    
    public DateTime CreatedAt{get;set;}

    public int Priority{get;set;}
    
    public int UserId{get;set;}
    
}