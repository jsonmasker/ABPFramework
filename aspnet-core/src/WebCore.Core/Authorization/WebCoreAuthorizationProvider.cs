using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace WebCore.Authorization;

public class WebCoreAuthorizationProvider : AuthorizationProvider
{
    public override void SetPermissions(IPermissionDefinitionContext context)
    {
        context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
        context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
        context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
        context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

        // Restaurant permissions
        var restaurants = context.CreatePermission(PermissionNames.Pages_Restaurants, L("Restaurants"));
        restaurants.CreateChildPermission(PermissionNames.Pages_Restaurants_Create, L("RestaurantsCreate"));
        restaurants.CreateChildPermission(PermissionNames.Pages_Restaurants_Edit, L("RestaurantsEdit"));
        restaurants.CreateChildPermission(PermissionNames.Pages_Restaurants_Delete, L("RestaurantsDelete"));

        // Booking permissions
        var bookings = context.CreatePermission(PermissionNames.Pages_Bookings, L("Bookings"));
        bookings.CreateChildPermission(PermissionNames.Pages_Bookings_Create, L("BookingsCreate"));
        bookings.CreateChildPermission(PermissionNames.Pages_Bookings_Edit, L("BookingsEdit"));
        bookings.CreateChildPermission(PermissionNames.Pages_Bookings_Delete, L("BookingsDelete"));
        bookings.CreateChildPermission(PermissionNames.Pages_Bookings_ManageAll, L("BookingsManageAll"));
    }

    private static ILocalizableString L(string name)
    {
        return new LocalizableString(name, WebCoreConsts.LocalizationSourceName);
    }
}
