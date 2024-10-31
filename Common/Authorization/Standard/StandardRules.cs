using FluentValidation;
using FluentValidation.Results;

namespace Common.Authorization.Standard;

public static class AuthorizerStandards
{
    public static ValidationResult ValidateRuleSets(this IEnumerable<RuleSet> ruleSets,
        StandardAuthorizerParameters authorizerParameters)
    {
        var standardAuthorizer = new StandardAuthorizer();
        var results = ruleSets.Select(ruleSet =>
            standardAuthorizer.Validate(authorizerParameters,
                options => options.IncludeRuleSets(ruleSet.GetRules().ToArray()))).ToList();

        return results.Any(x => x.IsValid)
            ? results.First(x => x.IsValid)
            : new ValidationResult(results.Where(x => !x.IsValid));
    }

    public static IEnumerable<string> GetRules(this IEnumerable<RuleSet> ruleSets)
        => ruleSets.SelectMany(GetRules).ToList();

    public static IEnumerable<string> GetRules(this RuleSet ruleSet) => ruleSet switch
    {
        RuleSet.HasInternalRole => RuleSets.HasInternalRole,
        RuleSet.HasExternalRole => RuleSets.HasExternalRole,
        RuleSet.HasAnyRole => RuleSets.HasAnyRole,
        RuleSet.HasEffectiveMemberPermissions => RuleSets.HasEffectiveMemberPermissions,
        RuleSet.HasEffectiveNonMemberPermissions => RuleSets.HasEffectiveNonMemberPermissions,
        RuleSet.HasNonEffectiveMemberPermissions => RuleSets.HasNonEffectiveMemberPermissions,
        _ => throw new ArgumentOutOfRangeException()
    };

    public static class RuleSets
    {
        public static IEnumerable<string> HasInternalRole = [Rules.IsVCA, Rules.IsTCA, Rules.IsSuperAdmin];

        public static IEnumerable<string> HasExternalRole = [Rules.IsMSA, Rules.IsWireUser];

        public static IEnumerable<string> HasAnyRole = [..HasInternalRole, ..HasExternalRole];

        public static IEnumerable<string> HasEffectiveMemberPermissions =
            [Rules.IsMemberEffective, Rules.IsMember, Rules.HasOneAndTwoSigned, ..HasAnyRole];

        public static IEnumerable<string> HasNonEffectiveMemberPermissions =
            [Rules.IsMemberIneffective, Rules.IsMember, Rules.HasOneAndTwoSigned, ..HasInternalRole];

        public static IEnumerable<string> HasEffectiveNonMemberPermissions =
            [Rules.IsMemberEffective, Rules.IsMemberServicer, Rules.HasThreeSigned, ..HasExternalRole];
    }

    public static class Rules
    {
        public const string IsMemberEffective = "IsMemberEffective";
        public const string IsMemberIneffective = "IsMemberIneffective";
        public const string IsMember = "IsMember";
        public const string IsMemberServicer = "IsMemberServicer";
        public const string HasOneAndTwoSigned = "HasOneAndTwoSigned";
        public const string HasThreeSigned = "HasThreeSigned";
        public const string IsMSA = "IsMSA";
        public const string IsWireUser = "IsWireUser";
        public const string IsTCA = "IsTCA";
        public const string IsVCA = "IsVCA";
        public const string IsSuperAdmin = "IsSuperAdmin";
    }
}