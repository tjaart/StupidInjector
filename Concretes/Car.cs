using System;

namespace DiTest
{
    public class Car : IVehicle
    {
        public void Drive()
        {
            Console.WriteLine("vrooooooooom");
        }
    }
}