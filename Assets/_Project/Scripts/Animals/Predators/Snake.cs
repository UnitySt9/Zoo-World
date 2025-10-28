using System;
using UnityEngine;
using UniRx;
using Zenject;
using _Project.Scripts.Animals.Base;
using _Project.Scripts.Core.Signals;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.WorldBounds;

namespace _Project.Scripts.Animals.Predators
{
    public class Snake : AnimalBase
    {
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float directionChangeInterval = 3f;
    
        private CompositeDisposable _disposables = new();
        private IWorldBoundsService _worldBounds;
        private Vector3 _currentDirection;
        private TastyTextController _tastyTextController;
    
        [Inject]
        public void Construct(IWorldBoundsService worldBounds, TastyTextController tastyTextController)
        {
            _worldBounds = worldBounds;
            _tastyTextController = tastyTextController;
        }
    
        public override void Initialize()
        {
            base.Initialize();
            
            Observable.Interval(TimeSpan.FromSeconds(directionChangeInterval))
                .TakeUntilDisable(this)
                .Subscribe(_ => ChangeDirection())
                .AddTo(_disposables);
            
            ChangeDirection();
            
            Observable.EveryUpdate()
                .TakeUntilDisable(this)
                .Subscribe(_ => Move())
                .AddTo(_disposables);
        }
    
        public override void Move()
        {
            if (!IsAlive.Value) 
                return;
            _rigidbody.velocity = _currentDirection * moveSpeed;
            CheckWorldBounds();
        }
    
        protected override void HandleCollision(IAnimal otherAnimal)
        {
            if (!IsAlive.Value) 
                return;
            
            if (otherAnimal.Diet == AnimalDiet.Prey)
            {
                _tastyTextController.ShowTastyText(transform.position);
                otherAnimal.Die();
                _signalBus.Fire(new AnimalEatenSignal(this, otherAnimal));
            }
            else if (otherAnimal.Diet == AnimalDiet.Predator)
            {
                bool iSurvive = UnityEngine.Random.Range(0, 2) == 0;
                if (iSurvive)
                {
                    otherAnimal.Die();
                    _tastyTextController.ShowTastyText(transform.position);
                    _signalBus.Fire(new AnimalEatenSignal(this, otherAnimal));
                }
                else
                {
                    Die();
                }
            }
        }
    
        private void ChangeDirection()
        {
            _currentDirection = new Vector3(
                UnityEngine.Random.Range(-1f, 1f),
                0,
                UnityEngine.Random.Range(-1f, 1f)
            ).normalized;
        }
    
        private void CheckWorldBounds()
        {
            Vector3 currentPosition = transform.position;
            if (!_worldBounds.IsWithinBounds(currentPosition))
            {
                _currentDirection = (_worldBounds.Center - currentPosition).normalized;
            }
        }
    
        public override void Die()
        {
            _disposables.Clear();
            base.Die();
        }
    }
}