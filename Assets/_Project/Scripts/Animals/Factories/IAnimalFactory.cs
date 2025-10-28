using UnityEngine;
using _Project.Scripts.Animals.Base;

namespace _Project.Scripts.Animals.Factories
{
    public interface IAnimalFactory
    {
        IAnimal Create(AnimalType animalType, Vector3 position);
    }
}