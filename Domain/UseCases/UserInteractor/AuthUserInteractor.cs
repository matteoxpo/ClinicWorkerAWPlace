using Domain.Common;
using Domain.Entities.App;
using Domain.Entities.App.Role;
using Domain.Entities.Services;
using Domain.Repositories.App;
namespace Domain.UseCases.UserInteractor;

public sealed class AuthUserInteractor<ID>
{
    private readonly IAuthRepository<ID> _userRepository;
    private readonly IVerifyerSecurityCode<string> _verifyerSecurityCodeService;

    public AuthUserInteractor(IAuthRepository<ID> userRepository, IVerifyerSecurityCode<string> verifyerSecurityCode)
    {
        _userRepository = userRepository;
        _verificationCode = null;
        _verifyerSecurityCodeService = verifyerSecurityCode;
    }
    public bool Auth(string login, string password) => _userRepository.Auth(login, password);

    public void ResetPasswordRequest(string login, ContactType whereToSend)
    {
        var contacts = _userRepository.GetContacts(login) ?? throw new AuthUserInteractorException($"No contacts at user with login: {login}");

        _verificationCode = _verifyerSecurityCodeService.GenerateAndSendCode(contacts.First(), whereToSend);
    }
    private string? _verificationCode;
    public void ResetPassword(string newPassword, string verificationCode)
    {
        if (_verificationCode is null)
        {
            throw new AuthUserInteractorException("There wasn't any request to reset password");
        }
        if (!string.Equals(_verificationCode, verificationCode))
        {
            _verificationCode = null;
            throw new AuthUserInteractorException($"Verification code {verificationCode} isn't correct. Retry request to reset password!");
        }
        _userRepository.ResetPassword(newPassword);
    }

}

public sealed class AuthUserInteractorException : Exception
{
    public AuthUserInteractorException(string message) : base(message) { }
}