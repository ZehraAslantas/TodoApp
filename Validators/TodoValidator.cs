using FluentValidation;
using TodoApp.Models;

namespace TodoApp.Validators;

public class TodoValidator : AbstractValidator<Todo> //abstract yazdık ve usingi ekledik
{
    //TodoValidator, AbstractValidator<Todo>'dan türeyecek ve todo modeline ait kuralları tanımlayacak
    public TodoValidator()
    {
        RuleFor(todo => todo.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz.")
            .MaximumLength(100).WithMessage("Başlık 100 karakteri geçemez");

        RuleFor(todo => todo.Description)
            .MaximumLength(500).WithMessage("Açıklama 500 karakteri geçemez");

        RuleFor(todo => todo.Priority)
            .IsInEnum().WithMessage("Öncelik geçerli değere sahip değil!"); //listede varmı öncelik

        RuleFor(todo => todo.DueDate)
            .Must(date=> !date.HasValue || date.Value.Date >= DateTime.Now)
          .WithMessage("Son tarih geçmiş zamana işaret edemez.");

    }




}
