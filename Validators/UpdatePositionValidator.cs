using FluentValidation;
using SchoolWebApplication.DTOs;

public class UpdatePositionDtoValidator : AbstractValidator<UpdatePositionDto>
{
    public UpdatePositionDtoValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MaximumLength(100);
    }
}