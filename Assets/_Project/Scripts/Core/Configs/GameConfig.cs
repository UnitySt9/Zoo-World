using UnityEngine;

namespace _Project.Scripts.Core.Configs
{
    [CreateAssetMenu(menuName = "ZooWorld/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Spawning")]
        public float MinSpawnInterval = 3f;
        public float MaxSpawnInterval = 6f;
    
        [Header("Animal Settings")]
        public float FrogJumpForce = 5f;
        public float FrogJumpInterval = 2f;
        public float SnakeMoveSpeed = 3f;
        public float SnakeDirectionChangeInterval = 3f;
    }
}