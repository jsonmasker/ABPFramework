using Abp.Authorization;
using WebCore.Authorization.Roles;
using WebCore.Authorization.Users;

namespace WebCore.Authorization;

public class PermissionChecker : PermissionChecker<Role, User>
{
    public PermissionChecker(UserManager userManager)
        : base(userManager)
    {
    }
}
