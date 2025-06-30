using FluentValidation;
using SchoolWebApplication.DTOs;

public class CreateClassDtoValidator : AbstractValidator<CreateClassDto>
{
    public CreateClassDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(10);
        RuleFor(x => x.ClassTeacher).NotEmpty().MaximumLength(100);
    }
}
