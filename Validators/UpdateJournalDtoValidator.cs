using FluentValidation;
using SchoolWebApplication.DTOs;

public class UpdateJournalDtoValidator : AbstractValidator<UpdateJournalDto>
{
    public UpdateJournalDtoValidator()
    {
        Include(new CreateJournalDtoValidator());
    }
}