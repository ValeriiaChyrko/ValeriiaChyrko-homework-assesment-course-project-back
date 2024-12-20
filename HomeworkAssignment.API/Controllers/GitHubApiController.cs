﻿using System.Diagnostics.CodeAnalysis;
using HomeworkAssignment.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkAssignment.Controllers;

[Route("api/github")]
[Produces("application/json")]
[ApiController]
public class GitHubApiController : ControllerBase
{
    private readonly IGitHubService _gitHubService;

    public GitHubApiController(IGitHubService gitHubService)
    {
        _gitHubService = gitHubService;
    }

    [HttpGet("{githubProfileId:guid}/{assignmentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<string>>> Get(Guid githubProfileId, Guid assignmentId)
    {
        var result = await _gitHubService.GetStudentBranches(githubProfileId, assignmentId);
        return StatusCode(StatusCodes.Status200OK, result);
    }

    [HttpGet("compilation/{githubProfileId:guid}/{assignmentId:guid}/{branch}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SuppressMessage("ReSharper.DPA", "DPA0011: High execution time of MVC action")]
    public async Task<ActionResult<int>> GetProjectCompilationVerification(Guid githubProfileId, Guid assignmentId,
        string branch)
    {
        var result = await _gitHubService.VerifyProjectCompilation(githubProfileId, assignmentId, branch);
        return StatusCode(StatusCodes.Status200OK, result);
    }

    [HttpGet("quality/{githubProfileId:guid}/{assignmentId:guid}/{branch}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> GetProjectQualityVerification(Guid githubProfileId, Guid assignmentId,
        string branch)
    {
        var result = await _gitHubService.VerifyProjectQuality(githubProfileId, assignmentId, branch);
        return StatusCode(StatusCodes.Status200OK, result);
    }

    [HttpGet("tests/{githubProfileId:guid}/{assignmentId:guid}/{branch}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> GetProjectTestsVerification(Guid githubProfileId, Guid assignmentId,
        string branch)
    {
        var result = await _gitHubService.VerifyProjectTests(githubProfileId, assignmentId, branch);
        return StatusCode(StatusCodes.Status200OK, result);
    }
}