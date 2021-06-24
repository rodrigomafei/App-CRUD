using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static Xamarin.Essentials.Permissions;

namespace CRUD.Util
{
    public static class PermissionHelper
    {
        public static async Task<bool> RequestLocationPermission()
        {
            var status = await CheckStatusAsync<LocationWhenInUse>();

            if (status == PermissionStatus.Granted)
                return true;

            var permissionResult = await RequestAsync<LocationWhenInUse>();

            return permissionResult == PermissionStatus.Granted;
        }
    }
}
