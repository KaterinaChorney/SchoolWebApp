using FluentValidation;
using SchoolWebApplication.DTOs;

public class UpdateStudentDtoValidator : AbstractValidator<UpdateStudentDto>
{
    public UpdateStudentDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.MiddleName).NotEmpty();
        RuleFor(x => x.FirstName).MaximumLength(50);
        RuleFor(x => x.LastName).MaximumLength(50);
        RuleFor(x => x.MiddleName).MaximumLength(50);

    }
}