using FluentValidation;
using FluentValidation.Results;

namespace Common.Authorization.Standard;

public static class AuthorizerStandards
{
    public static ValidationResult ValidateRuleSets(this IEnumerable<RuleSet> ruleSets,
        StandardAuthorizerParameters authorizerParameters)
    {
        var standardAuthorizer = new StandardAuthorizer();
        List<ValidationResult> results = [];
        foreach (var ruleSet in ruleSets)
        {
            var result = standardAuthorizer.Validate(authorizerParameters,
                options => options.IncludeRuleSets(ruleSet.GetRules().ToArray()));

            if (!result.IsValid)
            {
                results.Add(result);
                continue;
            }

            var rolesResults =
                ruleSet.GetRoles().Select(rule => standardAuthorizer.Validate(authorizerParameters,
                    options => options.IncludeRuleSets(rule))).ToList();

            if (rolesResults.Any(x => x.IsValid))
            {
                break;
            }

            results.Add(new ValidationResult(rolesResults));
        }

        return new ValidationResult(results);
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

    public static IEnumerable<string> GetRoles(this RuleSet ruleSet) => ruleSet switch
    {
        RuleSet.HasEffectiveMemberPermissions => RuleSets.HasAnyRole,
        RuleSet.HasEffectiveNonMemberPermissions => RuleSets.HasInternalRole,
        RuleSet.HasNonEffectiveMemberPermissions => RuleSets.HasExternalRole,
        _ => throw new ArgumentOutOfRangeException()
    };

    public static class RuleSets
    {
        public static IEnumerable<string> HasInternalRole = [Rules.IsVCA, Rules.IsTCA, Rules.IsSuperAdmin];

        public static IEnumerable<string> HasExternalRole = [Rules.IsMSA, Rules.IsWireUser];

        public static IEnumerable<string> HasAnyRole = [..HasInternalRole, ..HasExternalRole];

        public static IEnumerable<string> HasEffectiveMemberPermissions =
            [Rules.IsMemberEffective, Rules.IsMember, Rules.HasOneAndTwoSigned];

        public static IEnumerable<string> HasNonEffectiveMemberPermissions =
            [Rules.IsMemberIneffective, Rules.IsMember, Rules.HasOneAndTwoSigned];

        public static IEnumerable<string> HasEffectiveNonMemberPermissions =
            [Rules.IsMemberEffective, Rules.IsMemberServicer, Rules.HasThreeSigned];
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