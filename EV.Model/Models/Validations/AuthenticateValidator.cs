using EV.Model.Models.Request.Account;
using FluentValidation;

namespace API_EV.Areas.DaiLy.Models.Validations
{

    public class AuthenticateValidator : AbstractValidator<AuthenticateRequest>
    {
        public AuthenticateValidator()
        {
            //MasterMeta
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName Not Empty").Must((UserName) => IsUserNameValid(UserName)).WithMessage("Does not contain special characters");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password Not Empty").Must((Password) => IsPasswordValid(Password)).WithMessage("Does not contain special characters ");
        }
        public bool IsUserNameValid(string UserName)
        {
            return !UserName.Contains("'");
        }
        public bool IsPasswordValid(string Password)
        {
            return !Password.Contains("'");
        }
    }

}
