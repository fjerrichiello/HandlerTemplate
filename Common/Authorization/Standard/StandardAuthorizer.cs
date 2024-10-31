using FluentValidation;

namespace Common.Authorization.Standard;

public class StandardAuthorizer : AbstractValidator<StandardAuthorizerParameters>
{
    public StandardAuthorizer()
    {
        RuleSet("IsMemberEffective",
            () => { RuleFor(x => x.Member.EffectiveDate).LessThanOrEqualTo(x => x.Member.EffectiveDate); });

        RuleSet("IsMemberIneffective",
            () => { RuleFor(x => x.Member.EffectiveDate).GreaterThan(x => x.Member.EffectiveDate); });

        RuleSet("IsMember", () => { RuleFor(x => x.Member.MemberType).Must(MemberTypes.IsMemberType); });

        RuleSet("IsMemberServicer",
            () =>
            {
                RuleFor(x => x.Member.MemberType).Must(x => x.HasValue && MemberTypes.IsMemberServicer(x.Value));
            });

        RuleSet("HasOneAndTwoSigned", () =>
        {
            RuleFor(x => x.Member.SignedOne).Equal(true);
            RuleFor(x => x.Member.SignedTwo).Equal(true);
        });

        RuleSet("HasThreeSigned", () => { RuleFor(x => x.Member.SignedThree).Equal(true); });

        RuleSet("IsMSA",
            () => { RuleFor(x => x.Roles).Must(x => x.Contains("MSA")); });

        RuleSet("IsWireUser",
            () => { RuleFor(x => x.Roles).Must(x => x.Contains("Wires")); });

        RuleSet("IsTCA",
            () => { RuleFor(x => x.Roles).Must(x => x.Contains("TCA")); });

        RuleSet("IsVCA",
            () => { RuleFor(x => x.Roles).Must(x => x.Contains("VCA")); });

        RuleSet("IsSuperAdmin",
            () => { RuleFor(x => x.Roles).Must(x => x.Contains("SuperAdmin")); });
    }
}