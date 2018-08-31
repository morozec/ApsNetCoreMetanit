namespace HelloApp.Services
{
    public class CounterService
    {
        public ICounter Counter { get; set; }

        public CounterService(ICounter counter)
        {
            Counter = counter;
        }
    }
}