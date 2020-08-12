

using System.Threading.Tasks;

namespace Leaderboard
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var game = new Game();
            await game.Start();
        }
    }
}
