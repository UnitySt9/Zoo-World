using UnityEngine;
using UniRx;

namespace _Project.Scripts.Animals.Base
{
    public interface IAnimal
    {
        AnimalType Type { get; }
        AnimalDiet Diet { get; }
        GameObject GameObject { get; }
        ReactiveProperty<bool> IsAlive { get; }
        void Initialize();
        void Die();
    }
}