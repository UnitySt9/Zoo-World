using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Animals.Base;

namespace _Project.Scripts.Core.Configs
{
    [CreateAssetMenu(menuName = "ZooWorld/AnimalRegistry")]
    public class AnimalRegistry : ScriptableObject
    {
        [Serializable]
        public class AnimalPrefab
        {
            public AnimalType Type;
            public GameObject Prefab;
        }
    
        [SerializeField] private List<AnimalPrefab> animalPrefabs = new();
    
        private Dictionary<AnimalType, GameObject> _prefabDictionary;
    
        public void Initialize()
        {
            _prefabDictionary = animalPrefabs.ToDictionary(x => x.Type, x => x.Prefab);
        }
    
        public GameObject GetAnimalPrefab(AnimalType type)
        {
            if (_prefabDictionary == null) 
                Initialize();
        
            if (_prefabDictionary.TryGetValue(type, out var prefab))
            {
                return prefab;
            }
        
            throw new Exception($"Prefab for animal type {type} not found!");
        }
    }
}