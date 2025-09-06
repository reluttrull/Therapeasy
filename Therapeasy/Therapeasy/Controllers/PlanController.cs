using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Therapeasy.Data;
using Therapeasy.Model;

namespace Therapeasy.Controllers;

[Route("plandb")]
[ApiController]
public class PlanController : Controller
{
    private readonly PlanDbContext _db;

    public PlanController(PlanDbContext db)
    {
        _db = db;
    }

    [HttpGet("get/plans")]
    public async Task<ActionResult<List<Plan>>> GetPlans()
    {
        return (await _db.Plans.ToListAsync()).OrderBy(p => p.DateCreated).ToList();
    }

    [HttpGet("get/planfromid/{id}")]
    public async Task<IActionResult> GetPlanById(int id)
    {
        var plan = await _db.Plans.FindAsync(id);
        if (plan == null)
        {
            return NotFound(); // 404
        }
        return Ok(plan); // 200
    }

    [HttpGet("get/plansforuser/{username}")]
    public async Task<ActionResult<List<Plan>>> GetPlansForUser(string username)
    {
        return (await _db.Plans.ToListAsync()).Where(p => p.CreatedByUser == username).ToList();
    }

    [HttpPost]
    [Route("create/plan/")]
    public async Task<IActionResult> CreatePlan(Plan plan)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // 400
        }
        _db.Plans.Add(plan);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPlanById), new { id = plan.Id }, plan);
    }

    [HttpPut]
    [Route("update/plan/{id}")]
    public async Task<IActionResult> UpdateExercise(int id, Plan plan)
    {
        if (id != plan.Id)
        {
            return BadRequest("Product ID mismatch.");  // 400
        }

        _db.Entry(plan).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_db.Plans.Any(p => p.Id == id))
            {
                return NotFound(); // 404
            }
            throw;
        }
        return NoContent(); // 204
    }

    [HttpDelete("delete/plan/{id}")]
    public IActionResult DeletePlan(int id)
    {
        Plan plan = _db.Plans.First(p => p.Id == id);
        if (plan == null)
        {
            return NotFound(); // 404
        }
        _db.Plans.Remove(plan);
        // remove references to deleted plan
        List<PlanExerciseLink> links = _db.PlanExerciseLinks.Where(l => l.PlanId == id).ToList();
        _db.PlanExerciseLinks.RemoveRange(links);
        _db.SaveChanges();
        return NoContent(); // 204
    }

    [HttpGet("get/planexerciselinksforplan/{planid}")]
    public async Task<ActionResult<List<PlanExerciseLink>>> GetPlanExerciseLinksForPlan(int planId)
    {
        return (await _db.PlanExerciseLinks.ToListAsync()).Where(l => l.PlanId == planId).ToList();
    }

    [HttpGet("get/planexerciselinkfromid/{id}")]
    public async Task<ActionResult<PlanExerciseLink>> GetPlanExerciseLinkById(int id)
    {
        var link = await _db.PlanExerciseLinks.FindAsync(id);
        if (link == null)
        {
            return NotFound();
        }
        return Ok(link); // 200
    }

    [HttpPost]
    [Route("create/planexerciselink/")]
    public async Task<IActionResult> CreatePlanExerciseLink(PlanExerciseLink link)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // 400
        }
        _db.PlanExerciseLinks.Add(link);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPlanExerciseLinkById), new { id = link.Id }, link);
    }

    [HttpGet("get/exercisefromid/{id}")]
    public async Task<ActionResult<Exercise>> GetExerciseById(int id)
    {
        var exercise = await _db.Exercises.FindAsync(id);
        if (exercise == null)
        {
            return NotFound();
        }
        return Ok(exercise); // 200
    }

    [HttpGet("get/exercisesforuser/{username}")]
    public async Task<ActionResult<List<Exercise>>> GetExercisesForUser(string username)
    {
        return (await _db.Exercises.ToListAsync()).Where(e => e.CreatedByUser == username).ToList();
    }

    [HttpPut]
    [Route("update/exercise/{id}")]
    public async Task<IActionResult> UpdateExercise(int id, Exercise exercise)
    {
        if (id != exercise.Id)
        {
            return BadRequest("Product ID mismatch."); // 400
        }

        _db.Entry(exercise).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_db.Exercises.Any(e => e.Id == id))
            {
                return NotFound(); // 404
            }
            throw;
        }
        return NoContent(); // 204
    }

    [HttpPost]
    [Route("create/exercise/")]
    public async Task<IActionResult> CreateExercise(Exercise exercise)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // 400
        }
        _db.Exercises.Add(exercise);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetExerciseById), new { id = exercise.Id }, exercise);
    }

    [HttpDelete("delete/exercise/{id}")]
    public IActionResult DeleteExercise(int id)
    {
        Exercise exercise = _db.Exercises.First(e => e.Id == id);
        if (exercise == null)
        {
            return NotFound(); // 404
        }
        _db.Exercises.Remove(exercise);
        // remove references to deleted exercise
        List<PlanExerciseLink> links = _db.PlanExerciseLinks.Where(l => l.ExerciseId == id).ToList();
        _db.PlanExerciseLinks.RemoveRange(links);
        _db.SaveChanges();
        return NoContent(); // 204
    }
    
    [HttpGet("get/exerciseimagesforuser/{username}")]
    public async Task<ActionResult<List<ExerciseImage>>> GetExerciseImagesForUser(string username)
    {
        return (await _db.ExerciseImages.ToListAsync()).Where(e => e.CreatedByUser == username).ToList();
    }

    [HttpGet("get/exerciseimagefromid/{id}")]
    public async Task<IActionResult> GetExerciseImageById(int id)
    {
        var plan = await _db.ExerciseImages.FindAsync(id);
        if (plan == null)
        {
            return NotFound(); // 404
        }
        return Ok(plan); // 200
    }

    [HttpPost]
    [Route("create/exerciseimage/")]
    public async Task<IActionResult> CreateExerciseImage(ExerciseImage img)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // 400
        }
        _db.ExerciseImages.Add(img);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetExerciseImageById), new { id = img.Id }, img);
    }
    
    [HttpDelete("delete/exerciseimage/{id}")]
    public IActionResult DeleteExerciseImage(int id)
    {
        ExerciseImage img = _db.ExerciseImages.First(i => i.Id == id);
        if (img == null)
        {
            return NotFound(); // 404
        }
        _db.ExerciseImages.Remove(img);
        List<Exercise> exercisesWImg = _db.Exercises.Where(e => e.ExerciseImageId == id).ToList();
        // remove references to deleted image
        foreach (Exercise exercise in exercisesWImg)
        {
            exercise.ExerciseImageId = 0;
        }
        _db.SaveChanges();
        return NoContent(); // 204
    }
}