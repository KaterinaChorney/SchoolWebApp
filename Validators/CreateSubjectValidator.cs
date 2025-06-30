using FluentValidation;
using SchoolWebApplication.DTOs;

public class CreateSubjectDtoValidator : AbstractValidator<CreateSubjectDto>
{
    public CreateSubjectDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.TeacherId).GreaterThan(0);
    }
}