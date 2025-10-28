using System;
using UnityEngine;
using UniRx;
using Zenject;
using _Project.Scripts.Animals.Base;
using _Project.Scripts.Gameplay.WorldBounds;

namespace _Project.Scripts.Animals.Prey
{
    public class Frog : AnimalBase
    {
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float jumpInterval = 2f;
    
        private CompositeDisposable _disposables = new();
        private IWorldBoundsService _worldBounds;
    
        [Inject]
        public void Construct(IWorldBoundsService worldBounds)
        {
            _worldBounds = worldBounds;
        }
    
        public override void Initialize()
        {
            base.Initialize();
            
            Observable.Interval(TimeSpan.FromSeconds(jumpInterval))
                .TakeUntilDisable(this)
                .Subscribe(_ => Move())
                .AddTo(_disposables);
        }
        
        public override void Move()
        {
            if (!IsAlive.Value) 
                return;
        
            Vector3 randomDirection = new Vector3(
                UnityEngine.Random.Range(-1f, 1f),
                0,
                UnityEngine.Random.Range(-1f, 1f)
            ).normalized;
        
            _rigidbody.AddForce(randomDirection * jumpForce, ForceMode.Impulse);
            CheckWorldBounds();
        }
        
        public override void Die()
        {
            _disposables.Clear();
            base.Die();
        }
    
        protected override void HandleCollision(IAnimal otherAnimal)
        {
            if (otherAnimal.Diet == AnimalDiet.Predator)
            {
                Die();
            }
        }
        
        private void CheckWorldBounds()
        {
            Vector3 currentPosition = transform.position;
            Vector3 newPosition = _worldBounds.ClampPosition(currentPosition);
        
            if (currentPosition != newPosition)
            {
                Vector3 directionToCenter = (_worldBounds.Center - currentPosition).normalized;
                _rigidbody.AddForce(directionToCenter * jumpForce * 0.5f, ForceMode.Impulse);
            }
        }
    }
}