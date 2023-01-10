using System.Collections.Generic;

namespace MIIProjekt.Collectables
{
    public interface ICollectableTriggerTarget
    {
        void InvokeCollectableTarget(List<ICollectable> collectables);
    }
}
