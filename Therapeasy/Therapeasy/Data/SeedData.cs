using Therapeasy.Model;
using Therapeasy.Enums;

namespace Therapeasy.Data;

public static class SeedData
{
    public static void Initialize(PlanDbContext db)
    {
        var Plans = new Plan[]
        {
            new Plan()
            {
                Id = 1,
                Name = "Rotator cuff tendonitis therapy plan",
                BodyRegion = BodyRegion.Shoulder,
                CreatedByUser = "jkljsflsfjsjflsf@gmail.com",
                DateCreated = new DateTime(2025, 8, 6),
            }, 
            new Plan()
            {
                Id = 2,
                Name = "Full knee replacement therapy plan",
                BodyRegion = BodyRegion.Knee,
                CreatedByUser = "jkljsflsfjsjflsf@gmail.com",
                DateCreated = new DateTime(2025, 8, 16)
            }
        };
        var Exercises = new Exercise[]
        {
            new Exercise()
            {
                Id = 1,
                CreatedByUser = "jkljsflsfjsjflsf@gmail.com",
                DateCreated = new DateTime(2025, 8, 7),
                Name = "doorway stretch",
                Notes = "hold each stretch 5 seconds or longer",
                SetsPerDay = 3,
                RepsPerSet = 8,
                ExerciseImageId = 1
            },
            new Exercise()
            {
                Id = 2,
                CreatedByUser = "jkljsflsfjsjflsf@gmail.com",
                DateCreated = new DateTime(2025, 8, 7),
                Name = "external rotation",
                SetsPerDay = 2,
                RepsPerSet = 15,
                ExerciseImageId = 2
            },
            new Exercise()
            {
                Id = 3,
                CreatedByUser = "jkljsflsfjsjflsf@gmail.com",
                DateCreated = new DateTime(2025, 8, 7),
                Name = "up the back shoulder stretch",
                Notes = "hold each stretch 5 seconds or longer",
                SetsPerDay = 3,
                RepsPerSet = 8
            },
            new Exercise()
            {
                Id = 4,
                CreatedByUser = "jkljsflsfjsjflsf@gmail.com",
                DateCreated = new DateTime(2025, 8, 7),
                Name = "1up the back shoulder stretch",
                Notes = "hold each stretch 5 seconds or longer",
                SetsPerDay = 3,
                RepsPerSet = 8
            },
            new Exercise()
            {
                Id = 5,
                CreatedByUser = "jkljsflsfjsjflsf@gmail.com",
                DateCreated = new DateTime(2025, 8, 7),
                Name = "2up the back shoulder stretch",
                Notes = "hold each stretch 5 seconds or longer",
                SetsPerDay = 3,
                RepsPerSet = 8
            },
            new Exercise()
            {
                Id = 6,
                CreatedByUser = "jkljsflsfjsjflsf@gmail.com",
                DateCreated = new DateTime(2025, 8, 7),
                Name = "3up the back shoulder stretch",
                Notes = "hold each stretch 5 seconds or longer",
                SetsPerDay = 3,
                RepsPerSet = 8
            },
        };

        var PlanExerciseLinks = new PlanExerciseLink[]{
            new PlanExerciseLink(){ Id = 1, ExerciseId = 1, PlanId = 1 },
            new PlanExerciseLink(){ Id = 2, ExerciseId = 2, PlanId = 1 },
            new PlanExerciseLink(){ Id = 3, ExerciseId = 3, PlanId = 1 },
            new PlanExerciseLink(){ Id = 4, ExerciseId = 4, PlanId = 2 },
            new PlanExerciseLink(){ Id = 5, ExerciseId = 5, PlanId = 2 },
            new PlanExerciseLink(){ Id = 6, ExerciseId = 6, PlanId = 2 },
        };

        string imagePath1 = "Data/SampleImages/doorwayShoulderStretch.jpg"; // Replace with your image path
        byte[] imageBytes1 = File.ReadAllBytes(imagePath1);
        string imagePath2 = "Data/SampleImages/externalRotation.gif"; // Replace with your image path
        byte[] imageBytes2 = File.ReadAllBytes(imagePath2);
        string imagePath3 = "Data/SampleImages/upTheBackShoulderStretch.jpg"; // Replace with your image path
        byte[] imageBytes3 = File.ReadAllBytes(imagePath3);
        var ExerciseImages = new ExerciseImage[] {
            new ExerciseImage(){
                Id = 1,
                ContentType = "image/jpeg",
                Name = "doorwayShoulderStretch.jpg",
                CreatedByUser = "jkljsflsfjsjflsf@gmail.com",
                DateCreated = new DateTime(2025, 9, 1),
                ImageData = imageBytes1
            },
            new ExerciseImage(){
                Id = 2,
                ContentType = "image/gif",
                Name = "externalRotation.gif",
                CreatedByUser = "jkljsflsfjsjflsf@gmail.com",
                DateCreated = new DateTime(2025, 9, 1),
                ImageData = imageBytes2
            },
            new ExerciseImage(){
                Id = 3,
                ContentType = "image/jpeg",
                Name = "upTheBackShoulderStretch.jpg",
                CreatedByUser = "jkljsflsfjsjflsf@gmail.com",
                DateCreated = new DateTime(2025, 9, 1),
                ImageData = imageBytes3
            },
        };

        db.Plans.AddRange(Plans);
        db.Exercises.AddRange(Exercises);
        db.ExerciseImages.AddRange(ExerciseImages);
        db.PlanExerciseLinks.AddRange(PlanExerciseLinks);
        db.SaveChanges();
    }
}