
using LearningCoachAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningCoachAPI.Data;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        { }

            public DbSet<User> Users{get;set;}
            public DbSet<Subject> Subjects{get;set;}
            
            public DbSet<LearningSession> LearningSessions{get;set;}
            

        
}