using Domain.Entities.Common;

namespace Domain.Entities.Services;

public interface IVerifyerSecurityCode<Code>
{
    public Code GenerateAndSendCode(Contact contact, ContactType whereToSendCode);
}