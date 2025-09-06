using System.Numerics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Therapeasy.Model;

namespace Therapeasy.Data;

public class PlanDbContext : DbContext
{

    public PlanDbContext(DbContextOptions<PlanDbContext> options) : base(options)
    {
    }

    public DbSet<Plan> Plans { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<ExerciseImage> ExerciseImages { get; set; }
    public DbSet<PlanExerciseLink> PlanExerciseLinks { get; set; }
}
