using _Project.Scripts.UI.ViewModels;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.Views
{
    public class GameUIView : MonoBehaviour
    {
        [SerializeField] private TMP_Text preyCountText;
        [SerializeField] private TMP_Text predatorCountText;
    
        private IGameUIViewModel _viewModel;
        private CompositeDisposable _disposables = new();
    
        [Inject]
        public void Construct(IGameUIViewModel viewModel)
        {
            _viewModel = viewModel;
            BindViewModel();
        }
    
        private void BindViewModel()
        {
            _viewModel.PreyCountText
                .Subscribe(text => preyCountText.text = text)
                .AddTo(_disposables);
            
            _viewModel.PredatorCountText
                .Subscribe(text => predatorCountText.text = text)
                .AddTo(_disposables);
        }
    
        private void OnDestroy()
        {
            _disposables?.Dispose();
        }
    }
}