using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CitizenFX.Core;

using vMenuShared;

using static CitizenFX.Core.Native.API;
namespace vMenuServer.Events
{
    public class SupportServerEvent : BaseScript
    {
        public SupportServerEvent()
        {
            EventHandlers["AnyStaffOnline"] += new Action<Player>(StaffOnline);
        }

        private void StaffOnline([FromSource] Player source)
        {
            bool isOnline = AnyStaffOnline();
        }
        
        [EventHandler("vMenu:NewReport")]
        private void OnNewReport([FromSource] Player source, string message)
        {
            if (!PermissionsManager.IsAllowed(PermissionsManager.Permission.POSubmitReports, source))
            {
                return;
            }

            if (!AnyStaffOnline())
            {
                source.TriggerEvent("vMenu:NoStaffOnline");
                return;
            }

            foreach (Player player in GetOnlineStaff())
            {
                Debug.WriteLine($"Support Event: {player.Name}: {message}");
                player.TriggerEvent("vMenu:NewReportNotification", source.Name, message);
            }
        }


        private bool AnyStaffOnline() => GetOnlineStaff().Count > 0;

        private List<Player> GetOnlineStaff()
        {
            
            List<Player> staff = [];

            foreach (Player p in Players)
            {
                if (IsPlayerAceAllowed(p.Handle, PermissionsManager.GetAceName(PermissionsManager.Permission.POViewReports)))
                {
                    Debug.WriteLine($"Player {p.Handle} was added as a staff member internally.");
                    staff.Add(p);
                }
            }
            return staff;
        }
    }
    
}