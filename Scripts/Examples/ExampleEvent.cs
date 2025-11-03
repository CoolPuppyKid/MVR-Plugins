namespace MVRPlugins.Examples
{
    [Event]
    public class ExampleEvent
    {
        public int number { get; }
        public ExampleEvent(int num)
        {
            number = num;
        }
    }
}