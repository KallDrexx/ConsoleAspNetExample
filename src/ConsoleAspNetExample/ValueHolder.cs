namespace ConsoleAspNetExample
{
    public interface IValueHolder
    {
        void AddOne();
        int Get();
    }

    public class ValueHolder : IValueHolder
    {
        private int _value;

        public void AddOne()
        {
            _value++;
        }

        public int Get()
        {
            return _value;
        }
    }
}
