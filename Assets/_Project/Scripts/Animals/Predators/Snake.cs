using System;
using UnityEngine;
using UniRx;
using Zenject;
using _Project.Scripts.Animals.Base;
using _Project.Scripts.Core.Configs;
using _Project.Scripts.Core.Signals;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.WorldBounds;

namespace _Project.Scripts.Animals.Predators
{
    public class Snake : AnimalBase
    {
        private CompositeDisposable _disposables = new();
        private IWorldBoundsService _worldBounds;
        private GameConfig _gameConfig;
        private TastyTextController _tastyTextController;
        private Vector3 _currentDirection;
    
        [Inject]
        public void Construct(IWorldBoundsService worldBounds, TastyTextController tastyTextController, GameConfig gameConfig)
        {
            _worldBounds = worldBounds;
            _tastyTextController = tastyTextController;
            _gameConfig = gameConfig;
        }
    
        public override void Initialize()
        {
            base.Initialize();
            
            Observable.Interval(TimeSpan.FromSeconds(_gameConfig.SnakeDirectionChangeInterval))
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
            _rigidbody.velocity = _currentDirection * _gameConfig.SnakeMoveSpeed;
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