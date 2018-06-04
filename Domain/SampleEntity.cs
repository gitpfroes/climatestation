using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class SampleEntity : TableEntity
    {
        public const string ClimaPartition = "DadosDispositivo";

        public SampleEntity(int userId, int id, string title, string body)
        {
            PartitionKey = ClimaPartition;
            RowKey = id.ToString();

            this.userId = userId;
            this.title = title;
            this.body = body;
        }

        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }
}
