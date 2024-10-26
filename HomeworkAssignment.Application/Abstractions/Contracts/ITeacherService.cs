﻿using HomeAssignment.DTOs.RequestDTOs;
using HomeAssignment.DTOs.RespondDTOs;

namespace HomeworkAssignment.Application.Abstractions.Contracts;

public interface ITeacherService
{
    Task<RespondTeacherDto> CreateTeacherAsync(RequestTeacherDto teacherDto, CancellationToken cancellationToken = default);
    Task<RespondTeacherDto> UpdateTeacherAsync(Guid userId, Guid githubProfileId, RequestTeacherDto teacherDto, CancellationToken cancellationToken = default);
    Task DeleteTeacherAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<RespondTeacherDto?> GetTeacherByIdAsync(Guid userId, Guid githubProfileId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RespondTeacherDto>> GetTeacherAsync(CancellationToken cancellationToken = default);
}