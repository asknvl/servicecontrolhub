using servicecontrolhub.config;
using servicecontrolhub.hub;
using servicecontrolhub.monitors;

namespace servicecontrolhub
{
    internal class Program
    {
        static void Main(string[] args)
        {  

            var config = Settings.getInstance().config;

            IHub hub = new Hub(config);

            while (true)
            {
                Console.ReadLine();
            }

        }
    }
}