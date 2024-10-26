﻿using HomeAssignment.DTOs.RequestDTOs;
using HomeAssignment.DTOs.RespondDTOs;
using MediatR;

namespace HomeAssignment.Persistence.Commands.Teachers;

public sealed record CreateTeacherCommand(RequestTeacherDto TeacherDto) : IRequest<RespondTeacherDto>;