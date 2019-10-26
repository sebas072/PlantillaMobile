using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using appPrueba.Models;
using Xamarin.Forms;
using appPrueba.Services;
using SignaturePad.Forms;
using System.IO;

namespace appPrueba.ViewModels
{
    public class ClientDetailViewModel : BaseViewModel
    {
        public Cliente Item { get; set; }
        public Command SaveClient { get; set; }
        public INavigation Navigation { get; internal set; }
        public SignaturePadView signaturePadView { get; set; }
        public IDataStore<Cliente> DataStore => DependencyService.Get<IDataStore<Cliente>>() ?? new ClientsDataStore();

        public ClientDetailViewModel(Cliente c, SignaturePadView f = null)
        {
            Item = c!= null ? c : new Cliente();
            SaveClient = new Command(async () => await ExecuteGuardarCommand());
        }

        private async Task ExecuteGuardarCommand()
        {
            //Item.FirmaCliente = await getFirma();

            await DataStore.AddItemAsync(Item);
        }
        public async Task<byte[]> getFirma()
        {
            return ReadFully(await signaturePadView.GetImageStreamAsync(SignatureImageFormat.Png));
        }
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
