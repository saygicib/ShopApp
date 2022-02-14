using FluentValidation;
using ShopApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.ValidationRules.FluentValidation.ProductValidator
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Adı boş bırakamazsınız.");
            RuleFor(x => x.Description).MinimumLength(20).WithMessage("Açıklama minimum 20 karakter uzunluğunda olmalıdır.");
            RuleFor(x => x.Price).GreaterThan(1).WithMessage("Fiyatı 1'den büyük olmak zorundadır.");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Fotoğraf yüklemelisiniz.");
        }
    }
}
