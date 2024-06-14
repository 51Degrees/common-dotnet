﻿using Blobject.AzureBlob;
using FiftyOne.Common.CloudStorage.Concepts;
using FiftyOne.Common.CloudStorage.Factory;
using FiftyOne.Common.CloudStorage.Imps.Adapters;

namespace FiftyOne.Common.CloudStorage.Imps
{
    public class AzureStorageSettings: IBlobClientBuilder
    {
        public string AccountName { get; private set; }
        public string AccountKey { get; private set; }
        public string BlobEndpoint { get; private set; }
        public string ContainerName { get; private set; }

        public AzureStorageSettings(
            string ContainerName,
            string AccountName,
            string AccountKey,
            string BlobEndpoint)
        {
            this.AccountName = AccountName;
            this.AccountKey = AccountKey;
            this.BlobEndpoint = BlobEndpoint;
            this.ContainerName = ContainerName;
        }
        public AzureStorageSettings(
            string ContainerName,
            string AccountName,
            string AccountKey,
            string DefaultEndpointsProtocol,
            string EndpointSuffix)
            : this(
                  ContainerName, 
                  AccountName, 
                  AccountKey,
                  $"{DefaultEndpointsProtocol}://{AccountName}.{EndpointSuffix}/")
        { }


        public IBlobClient Build()
        {
            return
                new BlobClientAdapter(
                    new AzureBlobClient(
                        new AzureBlobSettings(AccountName, AccountKey, BlobEndpoint, ContainerName)
                    )
                );
        }
    }
}
