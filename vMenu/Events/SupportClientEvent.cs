using System;

using CitizenFX.Core;

namespace vMenuClient.Events
{
    public class SupportClientEvent : BaseScript
    {
        private static readonly object _padlock = new object();
        private static SupportClientEvent _instance;

        public SupportClientEvent()
        {
            EventHandlers["vMenu:NewReportNotification"] += new Action<string, string>(OnNewReport);
        }

        internal static SupportClientEvent Instance
        {
            get
            {
                lock (_padlock)
                {
                    return _instance ??= new SupportClientEvent();
                }
            }
        }

        private void OnNewReport(string name, string message) =>
            Notify.Alert("A new report has been created. Open Moderation Tools in vMenu to view it.");
    }
}