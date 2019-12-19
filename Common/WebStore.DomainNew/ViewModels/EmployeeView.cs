using System.ComponentModel.DataAnnotations;

namespace WebStore.DomainNew.ViewModels
{
    public class EmployeeView
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Имя является обязательным полем")]
        [Display(Name = "Имя")]
        [StringLength(maximumLength: 200, ErrorMessage = "В имени не может быть больше 200 символов", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Фамилия является обязательным полем")]
        public string SurName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Возраст")]
        public int Age { get; set; }

        public string Position { get; set; }
    }
}
