﻿using HomeworkAssignment.Infrastructure.Abstractions.Contracts;
using HomeworkAssignment.Infrastructure.Abstractions.TestsSection;

namespace HomeworkAssignment.Infrastructure.Implementations.TestsSection;

public class JavaTestsRunner : ITestsRunner
{
    public async Task<IEnumerable<TestResult>> RunTestsAsync(string repositoryPath, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}