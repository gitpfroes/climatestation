using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using ClimateActor.Interfaces;
using Domain;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;

namespace ClimateActor
{
    /// <remarks>
    /// Esta classe representa um ator.
    /// Cada ActorID é mapeada para uma instância desta classe.
    /// O atributo StatePersistence determina persistência e replicação do estado ator:
    ///  - Persistido: o estado é gravado para o disco e replicado.
    ///  - Volátil: o estado é mantido somente na memória e replicado.
    ///  - Nenhum: o estado é mantido somente na memória e não é replicado.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    public class ClimateActor : Actor, IClimateActor
    {
        private readonly string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=testepaola;AccountKey=DUFAWmh+e4y9pQsqeV+kuIfYlRG72RKmvoTCthsHUDHMjEemr3AEqqnK8YekdWvxS9bx45c2V8tJyWXXxo01cA==;EndpointSuffix=core.windows.net";
        private CloudTable cloudTable;

        /// <summary>
        /// Inicializa uma nova instância de ClimateActor
        /// </summary>
        /// <param name="actorService">O Microsoft.ServiceFabric.Actors.Runtime.ActorService que hospedará esta instância de ator.</param>
        /// <param name="actorId">O Microsoft.ServiceFabric.Actors.ActorId para esta instância de ator.</param>
        public ClimateActor(ActorService actorService, ActorId actorId) 
            : base(actorService, actorId)
        {
        }

        public Task UploadDeviceData(SampleEntity deviceEntity)
        {
            TableOperation insertOperation = TableOperation.InsertOrReplace(deviceEntity);
            cloudTable.ExecuteAsync(insertOperation);
            return Task.FromResult(true);
        }

        public Task ActivateMe()
        {
            this.OnActivateAsync();
            return Task.FromResult(true);
        }

        /// <summary>
        /// Este método é chamado sempre que um ator é ativado.
        /// Um ator é ativado na primeira vez que algum de seus métodos é invocado.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnectionString);
            var cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            cloudTable = cloudTableClient.GetTableReference("datatable");
            cloudTable.CreateIfNotExistsAsync();
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");

            // O StateManager é o repositório de estado particular deste ator.
            // Os dados armazenados no StateManager serão replicados para alta disponibilidade para os atores que usam armazenamento de estado volátil ou persistente.
            // Qualquer objeto serializado pode ser salvo no StateManager.
            // Para obter mais informações, consulte https://aka.ms/servicefabricactorsstateserialization

            return this.StateManager.TryAddStateAsync("count", 0);
        }

        Task IClimateActor.UploadClimateData(DataEntity dataEntity)
        {
            TableOperation insertOperation = TableOperation.InsertOrReplace(dataEntity);
            cloudTable.ExecuteAsync(insertOperation);
            return Task.FromResult(true);
        }
    }
}
