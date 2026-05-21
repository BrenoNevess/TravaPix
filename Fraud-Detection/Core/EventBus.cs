using System;

namespace FraudDetection.Core
{
    public static class EventBus
    {
        public static event Action?
            OnDataChanged;

        public static void NotifyDataChanged()
        {
            OnDataChanged?.Invoke();
        }
    }
}