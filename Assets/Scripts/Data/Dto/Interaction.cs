using Core.Behaviors.Interaction;

namespace Data.Dto
{
    public class DamageData : IEvent
    {
        /// <summary>Количество урона, передаваемое в событии взаимодействия.</summary>
        public readonly int Damage;

        /// <summary>Создаёт событие урона с указанной величиной.</summary>
        /// <param name="damage">Величина урона.</param>
        public DamageData(int damage)
        {
            Damage = damage;
        }
    }
}