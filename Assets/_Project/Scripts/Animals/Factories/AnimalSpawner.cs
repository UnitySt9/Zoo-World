using UnityEngine;
using Zenject;
using _Project.Scripts.Animals.Base;
using _Project.Scripts.Core.Configs;
using _Project.Scripts.Gameplay.WorldBounds;

namespace _Project.Scripts.Animals.Factories
{
    public class AnimalSpawner : IAnimalSpawner, IInitializable, ITickable
    {
        private readonly IAnimalFactory _animalFactory;
        private readonly IWorldBoundsService _worldBounds;
        private readonly GameConfig _gameConfig;
    
        private float spawnTimer;
        private bool isSpawning;
    
        public AnimalSpawner(IAnimalFactory animalFactory, IWorldBoundsService worldBounds, GameConfig gameConfig)
        {
            _animalFactory = animalFactory;
            _worldBounds = worldBounds;
            _gameConfig = gameConfig;
        }
    
        public void Initialize()
        {
            StartSpawning();
        }
    
        public void StartSpawning()
        {
            isSpawning = true;
            spawnTimer = 0f;
        }
    
        public void StopSpawning()
        {
            isSpawning = false;
        }
    
        public void Tick()
        {
            if (!isSpawning) return;
        
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0f)
            {
                SpawnRandomAnimal();
                spawnTimer = Random.Range(_gameConfig.MinSpawnInterval, _gameConfig.MaxSpawnInterval);
            }
        }
    
        private void SpawnRandomAnimal()
        {
            AnimalType randomType = GetRandomAnimalType();
            Vector3 spawnPosition = _worldBounds.GetRandomPositionWithinBounds();
        
            _animalFactory.Create(randomType, spawnPosition);
        }
    
        private AnimalType GetRandomAnimalType()
        {
            // 50% шанс для каждого типа, можно настроить в конфиге
            return Random.Range(0, 2) == 0 ? AnimalType.Frog : AnimalType.Snake;
        }
    }
}