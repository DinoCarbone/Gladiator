using Data.ScriptableObjects.Animatios;

namespace Core.Behaviors.Interaction
{
    /// <summary>Сервис получения событий анимации (внешний API).</summary>
    public interface IAnimationEventReceiveService
    {
        /// <summary>Обработка входящего события анимации.</summary>
        void OnAnimationEvent(AnimationEventSO animationEvent);
    }

    /// <summary>Сервис приёма внутренних событий и маршрутизации их в систему сущности.</summary>
    public interface IInternalEventReceiverService
    {
        /// <summary>Принимает внутреннее событие типа <see cref="IEvent"/>.</summary>
        void ReceiveEvent(IEvent @event);
    }

    /// <summary>Интерфейс для компонентов, которые могут получать внутренние события.</summary>
    public interface IInternalEventReceiver
    {
        void ReceiveEvent(IEvent @event);
    }

    /// <summary>Интерфейс для получения событий анимации на уровне компонента.</summary>
    public interface IAnimationEventReceiver
    {
        void ReceiveAnimationEvent(AnimationEventSO animationEvent);
    }

    /// <summary>Маркерный интерфейс для внутренних событий взаимодействия.</summary>
    public interface IEvent { }

    /// <summary>Событие урона — предоставляет величину урона.</summary>
    public interface IDamageEvent
    {
        int Damage { get; }
    }

    /// <summary>Излучатель внешних событий (например, столкновений или радиусных эффектов).</summary>
    public interface IExternalEventEmitter
    {
        void EmitEvent(IEvent @event, float distance, float radius);
    }

    /// <summary>Получатель внешних событий.</summary>
    public interface IExternalEventReceiver
    {
        void ReceiveEvent(IEvent @event);
    }
}