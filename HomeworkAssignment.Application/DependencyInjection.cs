﻿using HomeworkAssignment.Application.Abstractions;
using HomeworkAssignment.Application.Abstractions.Contracts;
using HomeworkAssignment.Application.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace HomeworkAssignment.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAssignmentService, AssignmentService>();
        services.AddScoped<IAttemptService, AttemptService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ITeacherService, TeacherService>();

        services.AddScoped<IDatabaseTransactionManager, DatabaseTransactionManager>();

        services.AddSingleton<IPasswordHasher, PasswordHasher>();
    }
}