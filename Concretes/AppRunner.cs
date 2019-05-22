namespace DiTest
{
    public class AppRunner : IAppRunner
    {
        private readonly IAnimal _animal;
        private readonly IVehicle _vehicle;

        public AppRunner(IAnimal animal, IVehicle vehicle)
        {
            _animal = animal;
            _vehicle = vehicle;
        }

        public void Run()
        {
            _animal.Call();
            _vehicle.Drive();
        }
    }
}