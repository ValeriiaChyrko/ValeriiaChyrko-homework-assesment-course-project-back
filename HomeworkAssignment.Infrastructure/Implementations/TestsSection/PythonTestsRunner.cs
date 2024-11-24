﻿using HomeAssignment.Domain.Abstractions.Contracts;
using HomeworkAssignment.Infrastructure.Abstractions.Contracts;
using HomeworkAssignment.Infrastructure.Abstractions.TestsSection;
using HomeworkAssignment.Infrastructure.Abstractions.DockerRelated;
using System.Text.RegularExpressions;

namespace HomeworkAssignment.Infrastructure.Implementations.TestsSection;

public partial class PythonTestsRunner : ITestsRunner
{
    private const string DockerImage = "python:3.11";
    private const string Command = "python3";
    private static readonly Regex PassedPattern = GeneratePassedPatternRegex();
    private static readonly Regex FailedPattern = GenerateFailedPatternRegex();

    private readonly ILogger _logger;
    private readonly IDockerService _dockerService;

    public PythonTestsRunner(ILogger logger, IDockerService dockerService)
    {
        _logger = logger;
        _dockerService = dockerService;
    }

    public async Task<IEnumerable<TestResult>> RunTestsAsync(string repositoryPath, CancellationToken cancellationToken)
    {
        var testFiles = Directory.GetFiles(Path.Combine(repositoryPath, "tests"), "*.py", SearchOption.AllDirectories);
        var testResults = new List<TestResult>();

        foreach (var testFile in testFiles)
        {
            try
            {
                var resultSet = await RunTestsInDockerAsync(repositoryPath, cancellationToken);
                testResults.AddRange(resultSet); 
            }
            catch (Exception ex)
            {
                _logger.Log($"Error running tests for {testFile}: {ex.Message}");
            }
        }

        return testResults;
    }

    private async Task<List<TestResult>> RunTestsInDockerAsync(string repositoryPath, CancellationToken cancellationToken)
    {
        const string arguments = "-m unittest discover -v";

        var result = await _dockerService.RunCommandAsync(
            repositoryPath,
            string.Empty,
            DockerImage,
            Command,
            arguments,
            cancellationToken
        );

        return ParseTestResults(result.OutputDataReceived);
    }

    private static List<TestResult> ParseTestResults(string output)
    {
        var testResults = new List<TestResult>();

        using var reader = new StringReader(output);
        while (reader.ReadLine() is { } line)
        {
            var match = PassedPattern.Match(line);
            if (match.Success)
            {
                testResults.Add(new TestResult
                {
                    TestName = match.Groups["TestName"].Value,
                    IsPassed = true,
                    ExecutionTimeMs = ExtractExecutionTime(match.Groups["Time"].Value)
                });
            }
            else
            {
                match = FailedPattern.Match(line);
                if (match.Success)
                {
                    testResults.Add(new TestResult
                    {
                        TestName = match.Groups["TestName"].Value,
                        IsPassed = false,
                        ExecutionTimeMs = ExtractExecutionTime(match.Groups["Time"].Value)
                    });
                }
            }
        }

        return testResults;
    }

    private static double ExtractExecutionTime(string timeOutput)
    {
        var cleanTime = timeOutput.Replace("<", "").Replace(" ms", "").Trim();
        return double.TryParse(cleanTime, out var time) ? time : 0;
    }
        
    [GeneratedRegex("(OK|PASSED)", RegexOptions.Compiled)]
    private static partial Regex GeneratePassedPatternRegex();
        
    [GeneratedRegex("(FAIL|FAILED)", RegexOptions.Compiled)]
    private static partial Regex GenerateFailedPatternRegex();
}