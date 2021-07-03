using System;

namespace eShop.UseCases.PluginInterfaces.StateStore
{
    public interface IStateStore
    {
        public void AddStateChangeListeners(Action listener);
        public void RemoveStateChangeListeners(Action listener);
        public void BroadCastStateChange();
    }
}
