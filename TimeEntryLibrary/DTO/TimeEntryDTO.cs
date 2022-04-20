using Newtonsoft.Json;
using System.Collections.Generic;

namespace App.Common.DTO
{
    public class TimeEntryDTO
    {

        [JsonProperty("$schema")]
        public string Schema { get; set; }


        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }

        [JsonProperty("required")]
        public List<string> Required { get; set; }
    }

    public class Properties
    {
        public Property StartOn { get; set; }
        public Property EndOn { get; set; }
    }
    public class Property
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }
    }
}

