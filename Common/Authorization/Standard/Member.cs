namespace Common.Authorization.Standard;

public record Member(
    Guid Id,
    MemberType? MemberType,
    bool SignedOne,
    bool SignedTwo,
    bool SignedThree,
    DateOnly? EffectiveDate);