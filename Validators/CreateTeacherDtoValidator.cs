﻿using FluentValidation;
using SchoolWebApplication.DTOs;

public class CreateTeacherDtoValidator : AbstractValidator<CreateTeacherDto>
{
    public CreateTeacherDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.MiddleName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.PositionId).GreaterThan(0);
        RuleFor(x => x.Experience).InclusiveBetween(0, 60);
    }
}