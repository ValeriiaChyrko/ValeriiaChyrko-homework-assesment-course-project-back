﻿using HomeworkAssignment.Infrastructure.Abstractions.Contracts;
using HomeworkAssignment.Infrastructure.Abstractions.GitHubRelated;
using Microsoft.Extensions.Configuration;

namespace HomeworkAssignment.Infrastructure.Implementations.GitHubRelated;

public class GitHubClientProvider : IGitHubClientProvider
{
    private readonly IConfiguration _config;

    public GitHubClientProvider(IConfiguration config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public GitHubClientOptions GetGitHubClientOptions()
    {
        var token = _config["GitHubClient:Token"];
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("GitHubClient:Token type must be specified", nameof(token));

        return new GitHubClientOptions(token);
    }
}