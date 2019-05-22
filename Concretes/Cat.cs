using System;

namespace DiTest
{
    public class Cat : IAnimal
    {
        public Cat()
        {
        }

        public void Call()
        {
            Console.WriteLine("meow meow");
        }
    }
}