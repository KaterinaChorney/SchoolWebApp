using FluentValidation;
using SchoolWebApplication.DTOs;

public class CreatePositionDtoValidator : AbstractValidator<CreatePositionDto>
{
    public CreatePositionDtoValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MaximumLength(100);
    }
}