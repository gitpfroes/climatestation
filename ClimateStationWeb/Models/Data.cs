using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefrigeratingChamberWeb.Models
{
    public class Data
    {
        public string time { get; set; }
        [JsonProperty("milliseconds_since_epoch")]
        public long miliseconds { get; set; }
        [JsonProperty("date")]
        public string data { get; set; }
    }
}
