﻿namespace HomeworkAssignment.Infrastructure.Abstractions.CompilationSection;

public interface ICodeBuildService
{
    Task<bool> VerifyProjectCompilation(string repositoryName, CancellationToken cancellationToken = default);
}