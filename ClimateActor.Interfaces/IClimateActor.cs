using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Remoting.FabricTransport;
using Microsoft.ServiceFabric.Services.Remoting;

[assembly: FabricTransportActorRemotingProvider(RemotingListener = RemotingListener.V2Listener, RemotingClient = RemotingClient.V2Client)]
namespace ClimateActor.Interfaces
{
    /// <summary>
    /// Essa interface define os métodos expostos por um ator.
    /// Os clientes usam esta interface para interagir com o ator que a implementa.
    /// </summary>
    public interface IClimateActor : IActor
    {
        Task ActivateMe();
        /// <summary>
        /// Atualiza os dados referente a data
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        Task UploadClimateData(DataEntity dataEntity);
        Task UploadDeviceData(SampleEntity deviceEntity);
    }
}
