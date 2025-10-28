using UniRx;
using _Project.Scripts.UI.Models;

namespace _Project.Scripts.UI.ViewModels
{
    public class GameUIViewModel : IGameUIViewModel
    {
        public IReadOnlyReactiveProperty<string> PreyCountText => _preyCountText;
        public IReadOnlyReactiveProperty<string> PredatorCountText => _predatorCountText;
        
        private readonly ReactiveProperty<string> _preyCountText = new("Prey: 0");
        private readonly ReactiveProperty<string> _predatorCountText = new("Predators: 0");
        private readonly GameStatsModel _statsModel;
        private readonly CompositeDisposable _disposables = new();
    
        public GameUIViewModel(GameStatsModel statsModel)
        {
            _statsModel = statsModel;
            
            _statsModel.DeadPreyCount
                .Subscribe(count => _preyCountText.Value = $"Prey: {count}")
                .AddTo(_disposables);
            
            _statsModel.DeadPredatorCount
                .Subscribe(count => _predatorCountText.Value = $"Predators: {count}")
                .AddTo(_disposables);
        }
        
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}