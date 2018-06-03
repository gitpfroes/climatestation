using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Domain
{
    public class DataEntity : TableEntity
    {
        public const string ClimaPartition = "DadosClima";

        public DataEntity(string data, string time, long mili)
        {
            PartitionKey = ClimaPartition;
            RowKey = data+time;

            this.time = time;
            this.data = data;
            this.miliseconds = mili;
        }

        public string time { get; set; }
        public long miliseconds { get; set; }
        public string data { get; set; }
    }
}
