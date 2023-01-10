using System.Collections.Generic;

namespace MIIProjekt.Collectables
{
    public interface ICollector
    {
        List<ICollectable> Collectables { get; }

        void AddCollectable(ICollectable collectable);
        void RemoveCollectable(ICollectable collectable);
    }
}
