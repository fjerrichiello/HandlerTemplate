namespace Common.Authorization.Standard;

public enum RuleSet
{
    HasInternalRole,
    HasExternalRole,
    HasAnyRole,
    HasEffectiveMemberPermissions,
    HasNonEffectiveMemberPermissions,
    HasEffectiveNonMemberPermissions,
}