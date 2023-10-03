using EticaretAPI.Application.ViewModels.Products;
using FluentValidation;


namespace EticaretAPI.Application.Validations.Product
{
    public class CreateProductValid: AbstractValidator<Products_VM_Create>
    {
        public CreateProductValid()
        {
            RuleFor(p => p.Name)
                .NotEmpty().NotNull().WithMessage("Lütfen Ürün adını doldurunuz.")
                .MaximumLength(20).MinimumLength(4)
                .WithMessage("Lütfen Ürün adı 20 ila 4 karakter arasında giriniz");
            RuleFor(p => p.Stock)
                 .NotEmpty().NotNull().WithMessage("Lütfen Stok bilgisini giriniz.")
                 .Must(p => p >= 0).WithMessage("Stok bilgisi negatif veya boş değer olamaz");

            RuleFor(p=>p.Price)
                .NotEmpty().NotNull().WithMessage("Lütfen Fiyat bilgisini giriniz.")
                .Must(p => p >= 0).WithMessage("Fiyat bilgisi negatif veya boş değer olamaz");
        }
    }
}
