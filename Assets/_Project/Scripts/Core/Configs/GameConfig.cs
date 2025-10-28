using UnityEngine;

namespace _Project.Scripts.Core.Configs
{
    [CreateAssetMenu(menuName = "ZooWorld/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Spawning")]
        public float MinSpawnInterval = 1f;
        public float MaxSpawnInterval = 2f;
    
        [Header("Frog Settings")]
        public float FrogJumpForce = 2f;
        public float FrogJumpInterval = 2f;
        public float FrogJumpHeight = 10f;
    
        [Header("Snake Settings")]
        public float SnakeMoveSpeed = 3f;
        public float SnakeDirectionChangeInterval = 3f;
    }
}