using Newtonsoft.Json;

namespace Tictactoe.Models
{
    public class Model
    {
        //public int Id { get; set; }

        [JsonProperty("map")]
        public int[] Map { get; set; }

        [JsonProperty("isCros")]
        public bool IsCros { get; set; }

        [JsonProperty("isFinish")]
        public bool IsFinish { get; set; }

        [JsonProperty("value")]
        public string? Value { get; set; }

        [JsonProperty("idxCel")]
        public int IdxCel { get; set; }
    }
}
