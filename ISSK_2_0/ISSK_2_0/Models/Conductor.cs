using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ISSK_2_0.Models
{
    public class Conductor
    {
        public int ConductorId { get; set; }
        [Display(Name = "Nr legitymacji")]
        public int Code { get; set; }
        [Display(Name = "Adres email")]
        public string Email { get; set; }
        public string Password { get; set; }
        [Display(Name = "Ostatnie logowanie")]
        public DateTime LastActiveDateTime { get; set; }
        public bool IsActive { get; set; }
        [Display(Name = "Kod aktywacyjny")]
        public string ActivationCode { get; set; }
        public virtual ConductorData ConductorData { get; set; }
        [Display(Name = "Role")]
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<BrigadeConductor> BrigadeConductors { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<NotificationRecipient> NotificationRecipients { get; set; }
        public virtual ICollection<MessageRecipient> MessageRecipients { get; set; }

        public Conductor()
        {
            IsActive = false;
        }
    }

    public class ConductorData
    {
        [ForeignKey("Conductor")]
        public int ConductorDataId { get; set; }
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Drugie imię")]
        public string MiddleName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime BirthDate { get; set; }
        public string Pesel { get; set; }
        public bool IsTrained { get; set; }       
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        [Display(Name = "Miasto")]
        public string City { get; set; }
        public virtual Conductor Conductor { get; set; }

        public ConductorData()
        {
            IsTrained = false;
            Avatar = @"/Content/Images/DefaultAvatar.png";
        }
    }

    public class LoginView
    {
        [Display(Name = "Adres Email")]
        [Required(ErrorMessage = "Adres Email jest wymagany!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Nieprawidłowy format adresu Email")]
        public string Email { get; set; }
        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Hasło jest wymagane!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegisterView
    {
        [Display(Name = "Adres Email")]
        [Required(ErrorMessage = "Adres Email jest wymagany!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Nieprawidłowy format adresu Email")]
        public string Email { get; set; }
        [Display(Name = "Kod aktywacyjny")]
        [Required(ErrorMessage = "Kod aktywacyjny jest wymagany!")]
        public string ActivationCode { get; set; }
    }

    public class SetPasswordView
    {
        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Hasło jest wymagane!")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Hasło musi mieć minimum 6 znaków!")]
        public string Password { get; set; }
        [Display(Name = "Powtórz hasło")]
        [Required(ErrorMessage = "Powtórzenie hasła jest wymagane!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Wprowadzone hasła są różne!")]
        public string ConfirmPassword { get; set; }
    }

    public class AccountView
    {
        public int Code { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public string ActivationCode { get; set; }
        public List<string> RoleName { get; set; }
    }

    public class CreateView
    {
        [Display(Name = "Nr legitymacji")]
        public int Code { get; set; }
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Display(Name = "Adres email")]
        [Required(ErrorMessage = "Adres Email jest wymagany!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Nieprawidłowy format adresu Email")]
        public string Email { get; set; }

        [Display(Name = "Data urodzenia")]
        [Required(ErrorMessage = "Data urodzenia jest wymagana")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [MinimumAge(13)]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Miasto")]
        [Required(ErrorMessage = "Miasto jest wymagane!")]
        public string City { get; set; }

    }
}