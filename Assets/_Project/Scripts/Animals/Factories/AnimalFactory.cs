using UnityEngine;
using Zenject;
using _Project.Scripts.Animals.Base;
using _Project.Scripts.Core.Configs;

namespace _Project.Scripts.Animals.Factories
{
    public class AnimalFactory : IAnimalFactory
    {
        private readonly DiContainer _container;
        private readonly AnimalRegistry _animalRegistry;

        public AnimalFactory(DiContainer container, AnimalRegistry animalRegistry)
        {
            _container = container;
            _animalRegistry = animalRegistry;
        }

        public IAnimal Create(AnimalType animalType, Vector3 position)
        {
            var prefab = _animalRegistry.GetAnimalPrefab(animalType);
            var animalObject = _container.InstantiatePrefab(prefab);
            var animal = animalObject.GetComponent<IAnimal>();
            animal.GameObject.transform.position = position;
            animal.Initialize();
            return animal;
        }
    }
}