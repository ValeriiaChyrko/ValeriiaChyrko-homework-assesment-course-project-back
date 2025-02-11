﻿using HomeAssignment.DTOs.RequestDTOs;
using HomeworkAssignment.Services.Abstractions;
using RepoAnalisys.Grpc;

namespace HomeworkAssignment.Services;

public class TestsGrpcService : ITestsGrpcService
{
    private readonly TestsOperator.TestsOperatorClient _client;
    private readonly ILogger<TestsGrpcService> _logger;

    public TestsGrpcService(TestsOperator.TestsOperatorClient client, ILogger<TestsGrpcService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<int> VerifyProjectPassedTestsAsync(RequestRepositoryWithBranchDto query, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Verifying tests for repo: {RepoTitle}, branch: {BranchTitle}, owner: {OwnerGithubUsername}, author: {AuthorGithubUsername}", 
            query.RepoTitle, query.BranchTitle, query.OwnerGitHubUsername, query.AuthorGitHubUsername);

        var request = new RepositoryWithBranchQuery
        {
            RepoTitle = query.RepoTitle,
            BranchTitle = query.BranchTitle,
            OwnerGithubUsername = query.OwnerGitHubUsername,
            AuthorGithubUsername = query.AuthorGitHubUsername
        };

        _logger.LogInformation("Sending request to tests operator client to verify project tests.");

        var response = await _client.VerifyProjectPassedTestsAsync(request, cancellationToken: cancellationToken);
        
        _logger.LogInformation("Received tests score: {Score} for repo: {RepoTitle}, branch: {BranchTitle}.", response.Score, query.RepoTitle, query.BranchTitle);

        return response.Score;
    }
}
