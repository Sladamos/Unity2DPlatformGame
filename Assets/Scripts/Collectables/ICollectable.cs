using UnityEngine;

namespace MIIProjekt.Collectables
{
    public interface ICollectable
    {
        Transform Transform { get; }
        string Name { get; }

        bool Collidable { set; }
        bool Active { set; }
    }
}
