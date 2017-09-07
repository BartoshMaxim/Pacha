using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pacha.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Введите имя")]
        [StringLength(30, ErrorMessage = "Имя слишком длинное (не должно превышать 30 символов)")]
        [DisplayName("Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [DisplayName("Фамилия")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Введите Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Не корректный Email")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Введите пароль")]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Пароли должны совпадать")]
        [DisplayName("Повторите пароль")]
        public string PasswordConfirm { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Введите Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Не корректный Email")]
        [DisplayName("Ваш email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [DisplayName("Оставаться в системе")]
        public bool RememberMe { get; set; }
    }

    public class LoginRegistrationModel
    {
        public LoginModel LoginModel { get; set; }
        public RegisterModel RegisterModel { get; set; }
    }
}