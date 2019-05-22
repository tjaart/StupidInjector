using System;
using DiTest;

namespace StupidInjector
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new StupidContainer();

            container.Register<IAnimal, Cat>();
            container.Register<IVehicle, Car>();
            container.Register<IAppRunner, AppRunner>();

            var appRunner = container.GetInstance<IAppRunner>();

            appRunner.Run();
        }
    }
}
