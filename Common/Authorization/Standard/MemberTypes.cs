using System.Collections.Immutable;

namespace Common.Authorization.Standard;

public static class MemberTypes
{
    private static IEnumerable<MemberType>
        MemberServicer = ImmutableList.Create(MemberType.Non7th, MemberType.Servicer);

    public static bool IsMemberType(MemberType? memberType) => MemberType.Member.Equals(memberType);

    public static bool IsMemberServicer(MemberType memberType) => MemberServicer.Contains(memberType);
}