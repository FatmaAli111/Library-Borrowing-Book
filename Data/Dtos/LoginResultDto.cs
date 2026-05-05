using Models.DTOs.Auth;

namespace Data.Dtos
{
    public enum LoginFailureKind
    {
        None,
        InvalidCredentials,
        EmailNotConfirmed
    }

    public class LoginResultDto
    {
        public LoginResponseDTO? Success { get; set; }
        public LoginFailureKind Failure { get; set; }
    }
}
