namespace Common.Authorization.Standard;

public record Member
{
    public MemberType? MemberType { get; init; }

    public bool SignedOne { get; init; }
    public bool SignedTwo { get; init; }
    public bool SignedThree { get; init; }

    public DateOnly? EffectiveDate { get; init; }
}