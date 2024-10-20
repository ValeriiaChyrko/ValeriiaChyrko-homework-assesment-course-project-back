﻿namespace HomeAssignment.Domain.Abstractions;

public class Attempt
{
    public Guid Id { get; set; }
    public Guid StudentId  { get; set; }
    public Guid AssignmentId  { get; set; }
    public int AttemptNumber { get; set; }
    public DateTime FinishedAt { get; set; }
    public int CompilationScore { get; set; }
    public int TestsScore { get; set; }
    public int QualityScore { get; set; }
    public int FinalScore { get; set; }

    public Attempt(Guid id, Guid studentId, Guid assignmentId, int attemptNumber, DateTime finishedAt, int compilationScore, int testsScore, int qualityScore, int finalScore)
    {
        Id = id;
        StudentId = studentId;
        AssignmentId = assignmentId;
        AttemptNumber = attemptNumber;
        FinishedAt = finishedAt;
        CompilationScore = compilationScore;
        TestsScore = testsScore;
        QualityScore = qualityScore;
        FinalScore = finalScore;
    }
    
    public static Attempt Create(Guid studentId, Guid assignmentId, int attemptNumber, int compilationScore, int testsScore, int qualityScore)
    {
        var attemptId = Guid.NewGuid();
        var finalScore = compilationScore + testsScore + qualityScore;
        var finishedAt = DateTime.UtcNow;

        var newAttempt = new Attempt(
            attemptId, 
            studentId, 
            assignmentId, 
            attemptNumber, 
            finishedAt, 
            compilationScore,
            testsScore, 
            qualityScore, 
            finalScore
        );

        return newAttempt;
    }
    
    public void Update(Guid studentId, Guid assignmentId, int attemptNumber, int compilationScore, int testsScore, int qualityScore)
    {
        var finalScore = compilationScore + testsScore + qualityScore;
        var finishedAt = DateTime.UtcNow;

        StudentId = studentId;
        AssignmentId = assignmentId;
        AttemptNumber = attemptNumber;
        FinishedAt = finishedAt;
        CompilationScore = compilationScore;
        TestsScore = testsScore;
        QualityScore = qualityScore;
        FinalScore = finalScore;
    }
}