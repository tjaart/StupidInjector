using System;

namespace DiTest
{
    public class Dog : IAnimal
    {
        public void Call()
        {
            Console.WriteLine("bark bark");
        }
    }
}