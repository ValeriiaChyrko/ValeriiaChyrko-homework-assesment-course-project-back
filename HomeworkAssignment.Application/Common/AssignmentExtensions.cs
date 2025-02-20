﻿using HomeAssignment.DTOs.RespondDTOs;
using HomeAssignment.DTOs.SharedDTOs;

namespace HomeworkAssignment.Application.Common;

internal static class AssignmentExtensions
{
    public static ScoreSectionDto? GetSection(this RespondAssignmentDto assignment, SectionType sectionType)
    {
        return sectionType switch
        {
            SectionType.Compilation => assignment.CompilationSection,
            SectionType.Quality => assignment.QualitySection,
            SectionType.Tests => assignment.TestsSection,
            _ => null
        };
    }
}