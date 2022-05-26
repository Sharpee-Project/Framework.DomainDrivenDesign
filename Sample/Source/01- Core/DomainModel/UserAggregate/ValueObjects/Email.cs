using Framework.DomainModel.Entities;
using Sharpee.Framework.DomainModel.Exceptions;
using System.Text.RegularExpressions;

namespace Sample.Core.DomainModel.UserAggregate.ValueObjects;

public sealed record class Email
{
    public Email(string value)
    {
        Validate(value);
        Value = value;
    }

    public static string Default => new Email("default@emofid.com");

    public string Value { get; private set; }

    private static void Validate(string value)
    {
        Regex regex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(value);
        if (!match.Success)
        {
            throw new SharpeeException("Email is invalid", ExceptionStatus.InvalidArgument);
        }
    }

    public static implicit operator Email(string value)
    {
        return new Email(value);
    }

    public static implicit operator string(Email email)
    {
        return email.Value;
    }

    public override string ToString()
    {
        return Value;
    }
}
