using FluentValidation;
using SchoolWebApplication.DTOs;

public class UpdateClassDtoValidator : AbstractValidator<UpdateClassDto>
{
    public UpdateClassDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(10);
        RuleFor(x => x.ClassTeacher).NotEmpty().MaximumLength(100);
    }
}