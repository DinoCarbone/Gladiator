using System;
using Zenject;

namespace Core.Services
{
    public interface ITickableService
    {
        event Action OnTick;
    }
    public class TickableService : ITickableService, ITickable
    {
        public event Action OnTick;
        public void Tick() => OnTick?.Invoke();
    }
}