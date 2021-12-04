using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using CryptoBotnet.Services;

namespace CryptoBotnet.Modules
{
    // Modules must be public and inherit from an IModuleBase
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        // Dependency Injection will fill this value in for us
        public CoinGeckoService CoinGeckoService { get; set; }

        [Command("ping")]
        [Alias("pong", "hello")]
        public Task PingAsync()
            => ReplyAsync("pong!");

        [Command("list")]
        public async Task ListCoinsAsync()
        {
            var coins = CoinGeckoService.ListCoins();
            foreach (var (coin, history) in coins)
            {
                return Context.Channel.Send(coin + ": " + history);
            }
        }

        // [Remainder] takes the rest of the command's arguments as one argument, rather than splitting every space
        [Command("check")]
        public async Task CheckAsync([Remainder] string text)
        {
            // Get a stream containing an image of a cat
            var stream = await CoinGeckoService.GetCryptocurrencyInformation(text);
            // Streams must be seeked to their beginning before being uploaded!
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "cat.png");
        }
    }
}
