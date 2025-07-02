using FluentValidation;
using SchoolWebApplication.DTOs;

public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
{
    public CreateStudentDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.MiddleName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.ClassId).GreaterThan(0);
    }
}