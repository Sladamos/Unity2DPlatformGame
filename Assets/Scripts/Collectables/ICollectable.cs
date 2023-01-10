using UnityEngine;

namespace MIIProjekt.Collectables
{
    public interface ICollectable
    {
        Vector2 Position { get; set; }
        Vector2 DisplayOffset { set; }
        string Name { get; }

        bool Collidable { set; }
        bool Active { set; }
    }
}
