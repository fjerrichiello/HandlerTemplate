namespace Common.Authorization.Standard;

public record StandardAuthorizerParameters(
    Member Member,
    List<string> Roles,
    List<string> InternalRoles,
    DateOnly Date);