using _Project.Scripts.Animals.Factories;
using _Project.Scripts.Core.Configs;
using UnityEngine;
using Zenject;
using _Project.Scripts.Core.Signals;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.WorldBounds;
using _Project.Scripts.UI.Models;
using _Project.Scripts.UI.ViewModels;
using _Project.Scripts.UI.Views;

namespace _Project.Scripts.Core.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private AnimalRegistry animalRegistry;
        [SerializeField] private TastyTextController tastyTextController;
        [SerializeField] private GameUIView gameUIView;
    
        public override void InstallBindings()
        {
            // Конфиги
            Container.BindInstance(gameConfig);
            Container.BindInstance(animalRegistry);
            Container.BindInstance(tastyTextController);
            Container.BindInstance(gameUIView);
            // Модели
            Container.Bind<GameStatsModel>().AsSingle();
            // ViewModel
            Container.Bind<IGameUIViewModel>().To<GameUIViewModel>().AsSingle();
            // Сервисы
            Container.BindInterfacesAndSelfTo<WorldBoundsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimalSpawner>().AsSingle();
            // Фабрики
            Container.Bind<IAnimalFactory>().To<AnimalFactory>().AsSingle();
            // Обработчики
            Container.BindInterfacesAndSelfTo<GameStatsHandler>().AsSingle();
            // Сигналы
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<AnimalDiedSignal>();
            Container.DeclareSignal<AnimalEatenSignal>();
        }
    }
}