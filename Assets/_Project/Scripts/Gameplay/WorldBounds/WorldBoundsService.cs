using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.WorldBounds
{
    public class WorldBoundsService : IWorldBoundsService, IInitializable
    {
        public Vector3 Center => _worldBounds.center;
        
        private Camera _mainCamera;
        private Bounds _worldBounds;
    
        public void Initialize()
        {
            _mainCamera = Camera.main;
            CalculateWorldBounds();
        }
    
        public bool IsWithinBounds(Vector3 position)
        {
            return _worldBounds.Contains(position);
        }
    
        public Vector3 ClampPosition(Vector3 position)
        {
            return _worldBounds.ClosestPoint(position);
        }
    
        public Vector3 GetRandomPositionWithinBounds()
        {
            return new Vector3(
                Random.Range(_worldBounds.min.x, _worldBounds.max.x),
                0,
                Random.Range(_worldBounds.min.z, _worldBounds.max.z)
            );
        }
        
        private void CalculateWorldBounds()
        {
            float distanceFromCamera = 10f; 
            Vector3 bottomLeft = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, distanceFromCamera));
            Vector3 topRight = _mainCamera.ViewportToWorldPoint(new Vector3(1, 1, distanceFromCamera));
        
            _worldBounds = new Bounds();
            _worldBounds.SetMinMax(bottomLeft, topRight);
            _worldBounds.Expand(2f);
        }
    }
}