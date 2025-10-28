using System;
using UnityEngine;
using UniRx;
using Zenject;
using _Project.Scripts.Animals.Base;
using _Project.Scripts.Core.Configs;
using _Project.Scripts.Gameplay.WorldBounds;

namespace _Project.Scripts.Animals.Prey
{
    public class Frog : AnimalBase
    {
        private CompositeDisposable _disposables = new();
        private IWorldBoundsService _worldBounds;
        private GameConfig _gameConfig;
        private bool _isJumping = false;
    
        [Inject]
        public void Construct(IWorldBoundsService worldBounds, GameConfig gameConfig)
        {
            _worldBounds = worldBounds;
            _gameConfig = gameConfig;
        }
    
        public override void Initialize()
        {
            base.Initialize();
            
            Observable.Interval(TimeSpan.FromSeconds(_gameConfig.FrogJumpInterval))
                .TakeUntilDisable(this)
                .Subscribe(_ => {
                    if (!_isJumping && IsAlive.Value)
                        Move();
                })
                .AddTo(_disposables);
        }
        
        public override void Move()
        {
            if (!IsAlive.Value || _isJumping) 
                return;
            StartJump();
        }
        
        private void FixedUpdate()
        {
            if (!IsAlive.Value) 
                return;
            Vector3 currentPosition = transform.position;
            Vector3 clampedPosition = _worldBounds.ClampPosition(currentPosition);
            if (currentPosition != clampedPosition)
            {
                transform.position = clampedPosition;
                Vector3 directionToCenter = (_worldBounds.Center - currentPosition).normalized;
                _rigidbody.velocity = directionToCenter * Mathf.Abs(_rigidbody.velocity.magnitude);
            }
        }
        
        private void StartJump()
        {
            _isJumping = true;
            
            Vector3 randomDirection = new Vector3(
                UnityEngine.Random.Range(-1f, 1f),
                0,
                UnityEngine.Random.Range(-1f, 1f)
            ).normalized;
            
            Vector3 targetPosition = transform.position + randomDirection * _gameConfig.FrogJumpForce;
            targetPosition = _worldBounds.ClampPosition(targetPosition);
            
            Vector3 jumpVector = (targetPosition - transform.position);
            
            Vector3 jumpForceVector = new Vector3(
                jumpVector.x, 
                _gameConfig.FrogJumpHeight, 
                jumpVector.z
            );
            
            _rigidbody.AddForce(jumpForceVector, ForceMode.Impulse);
            
            Observable.Timer(TimeSpan.FromSeconds(0.5f))
                .TakeUntilDisable(this)
                .Subscribe(_ => _isJumping = false)
                .AddTo(_disposables);
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
    }
}