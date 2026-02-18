namespace LearningCoachAPI.Models;

public class LearningSession
{
    
public int Id{get;set;}

    public int Duration{get;set;}      
    public DateTime StartTime{get;set;}
    
    public DateTime EndTime{get;set;}

    public int SubjectId{get;set;}    
    public int UserId{get;set;}
    public required string Note{get;set;}
}