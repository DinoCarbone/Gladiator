using Core.Behaviors.Interaction;

namespace Data.Dto
{
    public struct DamageData : IEvent
    {
        public readonly int Damage;

        public DamageData(int damage)
        {
            Damage = damage;
        }
    }
}