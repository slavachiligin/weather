namespace LocationService
{
    public static class LocationServiceProvider
    {
        public static ILocationService GetLocationService()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new AndroidLocationService();
#elif UNITY_IOS && !UNITY_EDITOR
            return new IosLocationService();
#elif UNITY_EDITOR
            return new EditorLocationService();
#endif
        }
    }
}