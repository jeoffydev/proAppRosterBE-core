namespace RosterSoftwareApp.Api.Data;

public static class UserRolePolicy
{
    public const string AdminRole = "Admin";
    public const string MemberRole = "Member";
}


public static class PoliciesClaim
{
    public const string ReadAccess = "read_access";
    public const string WriteAccess = "write_access";
}
