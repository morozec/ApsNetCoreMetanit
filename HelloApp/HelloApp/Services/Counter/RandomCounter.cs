using System;

namespace HelloApp.Services
{
    public class RandomCounter : ICounter
    {
        private int _value;
        static Random rnd = new Random();

        public RandomCounter()
        {
            _value = rnd.Next(0, 1000000);
        }

        public int Value
        {
            get => _value;
        }
    }
}