using Discord;
using Discord.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace YAMB
{
    public class IsPlayingModule : ModuleBase<SocketCommandContext>
    {
        [Command("vidya")]
        [Summary("Are the boys playing vidya?")]
        public Task Vidya()
        {
            Context.Guild.GetUsersAsync();
            var playerGroups = Context.Guild.Users
                .Where(u => u.Activity?.Type == ActivityType.Playing)
                .GroupBy(u => u.Activity.Name)
                .Select(grp => new
                {
                    game = grp.Key,
                    players = grp.Select(g => g.Nickname ?? g.Username)
                });

            StringBuilder builder = new StringBuilder();
            builder.Append(@"Checking if the boys are playing vidya
.
.
.");
            builder.AppendLine();

            if (playerGroups.Any())
            {
                builder.AppendLine("The boys are playing vidya!");

                foreach (var userGroup in playerGroups)
                {
                    IEnumerable<string> players = userGroup.players;
                    builder.AppendLine();
                    if (players.Count() > 1)
                    {
                        builder.AppendLine($"{string.Join(", ", players.Take(players.Count() - 1)) + (players.Count() == 1 ? "" : " and ") + players.Last()} are playing {userGroup.game} together! Why aren't you?");
                    }
                    else
                    {
                        builder.AppendLine($"{players.First()} is playing {userGroup.game} with themselves. Go play with them!");
                    }
                }
            }
            else
            {
                builder.AppendLine("Nope.");
            }

            return ReplyAsync(builder.ToString());
        }
    }
}
