using FluentValidation;
using SB.Entities;
using System;
using static SB.Entities.Enums;

namespace Solucion
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name)
               .NotNull()
               .WithErrorCode(ErrorCodes.NotNullName.ToString());

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.NotEmptyName.ToString());

            RuleFor(x => x.Name)
                .MaximumLength(60)
                .WithErrorCode(ErrorCodes.MaxLenght.ToString());

            RuleFor(x => x.Address)
                .NotNull()
                .WithErrorCode(ErrorCodes.NotNullName.ToString());

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.NotEmptyName.ToString());

            RuleFor(x => x.Address)
                .MaximumLength(80)
                .WithErrorCode(ErrorCodes.MaxLenght.ToString());

            RuleFor(x => x.DocumentNumber)
                .NotNull()
                .WithErrorCode(ErrorCodes.NotNullName.ToString());

            RuleFor(x => x.DocumentNumber)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.NotEmptyName.ToString());

            RuleFor(x => x.DocumentNumber)
                .MaximumLength(20)
                .WithErrorCode(ErrorCodes.MaxLenght.ToString());

            RuleFor(x => x.CountryId)
               .NotEmpty()
               .WithErrorCode(ErrorCodes.NotEmptyCountry.ToString());

            RuleFor(x => x.StateId)
               .NotEmpty()
               .WithErrorCode(ErrorCodes.NotEmptyState.ToString());

            RuleFor(x => x.CityId)
               .NotEmpty()
               .WithErrorCode(ErrorCodes.NotEmptyCity.ToString());

            RuleFor(x => x.BirthDate)
                .LessThan(new DateTime().AddYears(-100))
                .WithErrorCode(ErrorCodes.MinimumDate.ToString());

        }
    }
}
