﻿using HomeAssignment.Domain.Abstractions.Contracts;
using HomeAssignment.Domain.Abstractions.Enums;

namespace HomeAssignment.Domain.Abstractions;

public class Student : IUser, IHaveGitHubProfile
{
    private Student(Guid userId, string fullName, string email, string passwordHash, string roleType,
        DateTime createdAt, DateTime updatedAt, Guid gitHubProfileId, string githubUsername,
        string githubProfileUrl, string? githubPictureUrl)
    {
        UserId = userId;
        GitHubProfileId = gitHubProfileId;
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        RoleType = roleType;
        GithubUsername = githubUsername;
        GithubProfileUrl = githubProfileUrl;
        GithubPictureUrl = githubPictureUrl;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Guid GitHubProfileId { get; set; }
    public string GithubUsername { get; set; }
    public string GithubProfileUrl { get; set; }
    public string? GithubPictureUrl { get; set; }
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string RoleType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static Student Create(string fullName, string email, string passwordHash,
        string githubUsername, string githubProfileUrl, string? githubPictureUrl)
    {
        var userId = Guid.NewGuid();
        var gitHubProfileId = Guid.NewGuid();
        var roleType = UserRoles.Student.ToString().ToLower();
        var dateTime = DateTime.UtcNow;

        var newStudent = new Student(
            userId,
            fullName,
            email,
            passwordHash,
            roleType,
            dateTime,
            dateTime,
            gitHubProfileId,
            githubUsername,
            githubProfileUrl,
            githubPictureUrl
        );

        return newStudent;
    }

    public void Update(string fullName, string email, string passwordHash,
        string githubUsername, string githubProfileUrl, string? githubPictureUrl)
    {
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        GithubUsername = githubUsername;
        GithubProfileUrl = githubProfileUrl;
        GithubPictureUrl = githubPictureUrl;
        UpdatedAt = DateTime.UtcNow;
    }
}