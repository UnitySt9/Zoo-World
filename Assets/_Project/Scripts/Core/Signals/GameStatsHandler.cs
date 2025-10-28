using Zenject;
using _Project.Scripts.Animals.Base;
using _Project.Scripts.UI.Models;

namespace _Project.Scripts.Core.Signals
{
    public class GameStatsHandler : IInitializable
    {
        private readonly GameStatsModel _statsModel;
        private readonly SignalBus _signalBus;
    
        public GameStatsHandler(GameStatsModel statsModel, SignalBus signalBus)
        {
            _statsModel = statsModel;
            _signalBus = signalBus;
        }
    
        public void Initialize()
        {
            _signalBus.Subscribe<AnimalDiedSignal>(OnAnimalDied);
        }
    
        private void OnAnimalDied(AnimalDiedSignal signal)
        {
            if (signal.Animal.Diet == AnimalDiet.Prey)
            {
                _statsModel.DeadPreyCount.Value++;
            }
            else if (signal.Animal.Diet == AnimalDiet.Predator)
            {
                _statsModel.DeadPredatorCount.Value++;
            }
        }
    }
}