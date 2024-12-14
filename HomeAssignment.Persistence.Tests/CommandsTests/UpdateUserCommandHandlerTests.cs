﻿using AutoMapper;
using FluentAssertions;
using HomeAssignment.Database.Contexts.Abstractions;
using HomeAssignment.Database.Entities;
using HomeAssignment.DTOs.SharedDTOs;
using HomeAssignment.Persistence.Commands.Users;
using NSubstitute;

namespace HomeAssignment.Persistence.Tests.CommandsTests;

[TestFixture]
public class UpdateUserCommandHandlerTests
{
    [SetUp]
    public void Setup()
    {
        _mockContext = Substitute.For<IHomeworkAssignmentDbContext>();
        var config = new MapperConfiguration(cfg => { cfg.CreateMap<UserDto, UserEntity>().ReverseMap(); });
        _mapper = config.CreateMapper();

        _handler = new UpdateUserCommandHandler(_mockContext, _mapper);
    }

    private IHomeworkAssignmentDbContext _mockContext;
    private IMapper _mapper;
    private UpdateUserCommandHandler _handler;

    [Test]
    public void Should_Throw_ArgumentNullException_When_Command_Is_Null()
    {
        // Act
        Func<Task> act = async () => await _handler.Handle(null!, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'command')");
    }

    [Test]
    public void Should_Throw_ArgumentNullException_When_Context_Is_Null()
    {
        var act = () => new UpdateUserCommandHandler(null!, _mapper);

        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'context')");
    }

    [Test]
    public void Should_Throw_ArgumentNullException_When_Mapper_Is_Null()
    {
        var act = () => new UpdateUserCommandHandler(_mockContext, null!);

        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'mapper')");
    }

    [Test]
    public async Task Should_Update_User_And_Return_UserDto()
    {
        // Arrange
        var userDto = new UserDto
        {
            Id = Guid.NewGuid(),
            Email = "johndoe@example.com",
            FullName = "John Doe",
            PasswordHash = "sijdiowe9u9-d3",
            RoleType = "teacher"
        };
        var command = new UpdateUserCommand(userDto);

        var expectedEntity = _mapper.Map<UserEntity>(userDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockContext.UserEntities.Received(1)
            .Update(Arg.Is<UserEntity>(entity =>
                entity.Id == expectedEntity.Id &&
                entity.FullName == expectedEntity.FullName &&
                entity.Email == expectedEntity.Email &&
                entity.PasswordHash == expectedEntity.PasswordHash &&
                entity.RoleType == expectedEntity.RoleType));

        result.Should().NotBeNull();
        result.Id.Should().Be(userDto.Id);
        result.FullName.Should().Be(userDto.FullName);
        result.Email.Should().Be(userDto.Email);
        result.PasswordHash.Should().Be(userDto.PasswordHash);
        result.RoleType.Should().Be(userDto.RoleType);
    }
}