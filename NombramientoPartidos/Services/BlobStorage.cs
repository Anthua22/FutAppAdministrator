using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using NombramientoPartidos.Utilidades.ClasesPojos;

namespace NombramientoPartidos.Services
{
    public static class BlobStorage
    {
        private static readonly StorageCredentials credentials = new StorageCredentials(Properties.Settings.Default.AccountName, Properties.Settings.Default.KeyValue);
        private static readonly CloudStorageAccount storageacc = new CloudStorageAccount(credentials, true);
        private static readonly CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();

        private static readonly CloudBlobContainer containerArbitros = blobClient.GetContainerReference(Properties.Settings.Default.ContainerArbitros);
        private static readonly CloudBlobContainer containerJugadores = blobClient.GetContainerReference(Properties.Settings.Default.ContainerJugadores);
        private static readonly CloudBlobContainer containerEquipos = blobClient.GetContainerReference(Properties.Settings.Default.ContainerEquipos);
        private static readonly CloudBlobContainer containerStaffs = blobClient.GetContainerReference(Properties.Settings.Default.ContainerStaffs);
        private static readonly CloudBlobContainer containerAdministradores = blobClient.GetContainerReference(Properties.Settings.Default.ContainerAdministradores);


        public static string GuardarImagen(string filename, string blobReference, object ob)
        {
            if(ob is Arbitro)
            {
                CloudBlockBlob blockBlob = containerArbitros.GetBlockBlobReference(blobReference);
                blockBlob.UploadFromFile(filename);
                return blockBlob.Uri.AbsoluteUri;
            }else if (ob is Jugador)
            {
                CloudBlockBlob blockBlob = containerJugadores.GetBlockBlobReference(blobReference);
                blockBlob.UploadFromFile(filename);
                return blockBlob.Uri.AbsoluteUri;
            }
            else if(ob is Equipo)
            {
                CloudBlockBlob blockBlob = containerEquipos.GetBlockBlobReference(blobReference);
                blockBlob.UploadFromFile(filename);
                return blockBlob.Uri.AbsoluteUri;
            }else if(ob is Staff)
            {
                CloudBlockBlob blockBlob = containerStaffs.GetBlockBlobReference(blobReference);
                blockBlob.UploadFromFile(filename);
                return blockBlob.Uri.AbsoluteUri;
            }else if(ob is Administrador)
            {
                CloudBlockBlob blockBlob = containerAdministradores.GetBlockBlobReference(blobReference);
                blockBlob.UploadFromFile(filename);
                return blockBlob.Uri.AbsoluteUri;
            }
            return null;
        }

        public static void EliminarImagen(string blobReference, object ob)
        {
            if(ob is Arbitro)
            {
                CloudBlockBlob blockBlob = containerArbitros.GetBlockBlobReference(blobReference);

                if (blockBlob.Exists())
                {
                    blockBlob.Delete();
                }
            }
            else if (ob is Jugador)
            {
                CloudBlockBlob blockBlob = containerJugadores.GetBlockBlobReference(blobReference);

                if (blockBlob.Exists())
                {
                    blockBlob.Delete();
                }
            }
            else if (ob is Equipo)
            {
                CloudBlockBlob blockBlob = containerEquipos.GetBlockBlobReference(blobReference);

                if (blockBlob.Exists())
                {
                    blockBlob.Delete();
                }
            }
            else if (ob is Staff)
            {
                CloudBlockBlob blockBlob = containerStaffs.GetBlockBlobReference(blobReference);

                if (blockBlob.Exists())
                {
                    blockBlob.Delete();
                }
            }
            else if (ob is Administrador)
            {
                CloudBlockBlob blockBlob = containerAdministradores.GetBlockBlobReference(blobReference);

                if (blockBlob.Exists())
                {
                    blockBlob.Delete();
                }
            }

        }
    }
}
