using _Project.Scripts.Animals.Base;

namespace _Project.Scripts.Core.Signals
{
    public class AnimalEatenSignal
    {
        public IAnimal Eater { get; }
        public IAnimal Eaten { get; }
    
        public AnimalEatenSignal(IAnimal eater, IAnimal eaten)
        {
            Eater = eater;
            Eaten = eaten;
        }
    }
}