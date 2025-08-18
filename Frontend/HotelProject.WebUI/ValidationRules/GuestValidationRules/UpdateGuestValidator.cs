using FluentValidation;
using HotelProject.WebUI.Dtos.GuestDto;

namespace HotelProject.WebUI.ValidationRules.GuestValidationRules
{
    public class UpdateGuestValidator : AbstractValidator<UpdateGuestDto>
    {
        public UpdateGuestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı boş geçilemez");
            RuleFor(x => x.SurName).NotEmpty().WithMessage("Soyisim alanı boş geçilemez");
            RuleFor(x => x.City).NotEmpty().WithMessage("Şehir alanı boş geçilemez");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("En az 3 karakter olmalıdır").MaximumLength(20).WithMessage("En fazla 20 karakter olmalıdır");
            RuleFor(x => x.SurName).MinimumLength(2).WithMessage("En az 2 karakter olmalıdır").MaximumLength(30).WithMessage("En fazla 30 karakter olmalıdır");
            RuleFor(x => x.City).MinimumLength(3).WithMessage("En az 3 karakter olmalıdır").MaximumLength(20).WithMessage("En fazla 20 karakter olmalıdır");
        }
    }

}

