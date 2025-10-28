using UniRx;

namespace _Project.Scripts.UI.ViewModels
{
    public interface IGameUIViewModel
    {
        IReadOnlyReactiveProperty<string> PreyCountText { get; }
        IReadOnlyReactiveProperty<string> PredatorCountText { get; }
        void Dispose();
    }
}