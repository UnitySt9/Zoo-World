using UniRx;

namespace _Project.Scripts.UI.Models
{
    public class GameStatsModel
    {
        public ReactiveProperty<int> DeadPreyCount { get; } = new(0);
        public ReactiveProperty<int> DeadPredatorCount { get; } = new(0);
    }
}