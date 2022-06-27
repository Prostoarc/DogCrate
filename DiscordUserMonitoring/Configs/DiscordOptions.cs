using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordUserMonitoring.Configs
{
    internal class DiscordOptions
    {
        public string Token { get; set; } = null!;
        public bool AlwaysDownloadUsers { get; set; }
        public int MessageCacheSize { get; set; }
        public List<ulong> ObservedUserIds { get; set; } = null!;
    }
}
