using RemoteNotes.Domain.Core.Navigation;

namespace RemoteNotes.Domain.Core.Extensions
{
    public static class NavigationDataExtensions
    {
        public static TParameter GetParameter<TParameter>(this NavigationData data, string key)
        {
            return (TParameter)data.Parameters[key];
        }

        public static bool TryGetParameter<TParameter>(this NavigationData data, string key, out TParameter parameter)
        {
            parameter = default;

            var sameType = false;
            var containsKey = data.Parameters.ContainsKey(key);

            if (containsKey)
                sameType = data.Parameters[key] is TParameter;

            if (containsKey && sameType)
            {
                parameter = (TParameter)data.Parameters[key];
            }

            return containsKey && sameType;
        }
    }
}
