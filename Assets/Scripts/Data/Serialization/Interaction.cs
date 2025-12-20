using Core.Behaviors.Interaction;

namespace Data.Serialization
{
    public class DamageData : IEvent
    {
        public readonly int Damage;

        public DamageData(int damage)
        {
            Damage = damage;
        }
    }
}