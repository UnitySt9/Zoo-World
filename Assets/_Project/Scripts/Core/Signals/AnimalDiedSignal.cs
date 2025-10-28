using _Project.Scripts.Animals.Base;

namespace _Project.Scripts.Core.Signals
{
    public class AnimalDiedSignal
    {
        public IAnimal Animal { get; }
    
        public AnimalDiedSignal(IAnimal animal)
        {
            Animal = animal;
        }
    }
}