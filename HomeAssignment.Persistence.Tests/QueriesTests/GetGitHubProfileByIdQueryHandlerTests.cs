﻿using AutoMapper;
using FluentAssertions;
using HomeAssignment.Database.Contexts.Abstractions;
using HomeAssignment.DTOs.MappingProfiles;
using HomeAssignment.Persistence.Queries.GitHubProfiles;
using NSubstitute;

namespace HomeAssignment.Persistence.Tests.QueriesTests;

[TestFixture]
public class GetGitHubProfileByIdQueryHandlerTests
{
    private IHomeworkAssignmentDbContext _mockContext;
    private IMapper _mapper;
    private GetGitHubProfileByIdQueryHandler _handler;

    [SetUp]
    public void Setup()
    {
        _mockContext = Substitute.For<IHomeworkAssignmentDbContext>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = config.CreateMapper();

        _handler = new GetGitHubProfileByIdQueryHandler(_mockContext, _mapper);
    }

    [Test]
    public void Constructor_Should_Throw_ArgumentNullException_When_Context_Is_Null()
    {
        Action act = () => new GetGitHubProfileByIdQueryHandler(null!, _mapper);

        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'context')");
    }
    [Test]
    public void Constructor_Should_Throw_ArgumentNullException_When_Mapper_Is_Null()
    {
        Action act = () => new GetGitHubProfileByIdQueryHandler(_mockContext, null!);

        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'mapper')");
    }
    [Test]
    public void Handle_Should_Throw_Exception_When_Query_Is_Null()
    {
        // Act
        Func<Task> act = async () => await _handler.Handle(null!, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'query')");
    }
}