using servicecontrolhub.config;

namespace servicecontrolhub
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = Settings.getInstance().config;
        }
    }
}