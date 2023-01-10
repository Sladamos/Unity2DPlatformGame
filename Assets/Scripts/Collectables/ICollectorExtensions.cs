namespace MIIProjekt.Collectables
{
    public static class ICollectorExtensions
    {
        public static bool Contains(this ICollector collector, string name)
        {
            foreach (ICollectable collectable in collector.Collectables)
            {
                if (collectable.Name == name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
