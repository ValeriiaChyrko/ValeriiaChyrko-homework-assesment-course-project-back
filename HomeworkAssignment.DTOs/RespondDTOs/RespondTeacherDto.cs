﻿namespace HomeAssignment.DTOs.RespondDTOs;

public class RespondTeacherDto
{
    public Guid UserId { get; init; }
    public Guid GitHubProfileId { get; set; }
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string GithubUsername { get; set; } = string.Empty;
    public string GithubProfileUrl { get; set; } = string.Empty;
    public string? GithubPictureUrl { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}