using System;
using UnityEngine;
using UniRx;
using Zenject;

namespace _Project.Scripts.Gameplay
{
    public class TastyTextController : MonoBehaviour
    {
        [SerializeField] private GameObject tastyTextPrefab;
        [SerializeField] private float displayDuration = 1f;
    
        private DiContainer _container;
    
        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }
    
        public void ShowTastyText(Vector3 position)
        {
            var tastyText = _container.InstantiatePrefab(tastyTextPrefab);
            tastyText.transform.position = position + Vector3.up * 2f;
            
            Observable.Timer(TimeSpan.FromSeconds(displayDuration))
                .Subscribe(_ => Destroy(tastyText))
                .AddTo(tastyText);
        }
    }
}