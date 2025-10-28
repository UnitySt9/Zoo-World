using System;
using UnityEngine;
using UniRx;
using Zenject;
using _Project.Scripts.Core.Signals;

namespace _Project.Scripts.Animals.Base
{
    public abstract class AnimalBase : MonoBehaviour, IAnimal, IDisposable
    {
        public AnimalType Type => animalType;
        public AnimalDiet Diet => diet;
        public GameObject GameObject => gameObject;
        public ReactiveProperty<bool> IsAlive { get; private set; } = new(true);
        
        [SerializeField] protected AnimalType animalType;
        [SerializeField] protected AnimalDiet diet;
    
        protected Rigidbody _rigidbody;
        protected Collider _collider;
        protected SignalBus _signalBus;
    
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
    
        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }
    
        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (!IsAlive.Value) return;
            var otherAnimal = collision.gameObject.GetComponent<IAnimal>();
            if (otherAnimal != null)
            {
                HandleCollision(otherAnimal);
            }
        }
        
        public virtual void Initialize()
        {
            IsAlive.Value = true;
            gameObject.SetActive(true);
        }
    
        public virtual void Die()
        {
            if (!IsAlive.Value) return;
        
            IsAlive.Value = false;
            _signalBus.Fire(new AnimalDiedSignal(this));
            Dispose();
        }
    
        public virtual void Dispose()
        {
            Destroy(gameObject);
        }
        
        public abstract void Move();
        protected abstract void HandleCollision(IAnimal otherAnimal);
    }
}