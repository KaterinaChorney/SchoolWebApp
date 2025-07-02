using FluentValidation;
using SchoolWebApplication.DTOs;

public class CreateJournalDtoValidator : AbstractValidator<CreateJournalDto>
{
    public CreateJournalDtoValidator()
    {
        RuleFor(j => j.StudentId).GreaterThan(0);
        RuleFor(j => j.SubjectId).GreaterThan(0);
        RuleFor(j => j.ClassId).GreaterThan(0);
        RuleFor(j => j.TeacherId).GreaterThan(0);
        RuleFor(j => j.Semester).NotEmpty().MaximumLength(10);
        RuleFor(j => j.Mark).InclusiveBetween(1, 12);
        RuleFor(j => j.Date).NotEmpty();
    }
}