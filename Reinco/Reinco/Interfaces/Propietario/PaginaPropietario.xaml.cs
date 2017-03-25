﻿using Newtonsoft.Json;
using Reinco.Gestores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Reinco.Interfaces.Propietario
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaPropietario : ContentPage
    {
        public ObservableCollection<PropietarioItem> propietarioItem { get; set; }
       // datatable usuario;
        public PaginaPropietario()
        {
            InitializeComponent();
            propietarioItem = new ObservableCollection<PropietarioItem>();
            CargarPropietarioItem();
            propietarioListView.ItemsSource = propietarioItem;
            agregarPropietario.Clicked += AgregarPropietario_Clicked;
            
        }
        private async void CargarPropietarioItem()
        {
            try
            {
                var client = new HttpClient();
                var result = await client.GetAsync("http://192.168.1.37:80/ServicioPropietario.asmx/MostrarPropietarios");
                //recoge los datos json y los almacena en la variable resultado
                var resultado = await result.Content.ReadAsStringAsync();
                //si todo es correcto, muestra la pagina que el usuario debe ver
                dynamic array = JsonConvert.DeserializeObject(resultado);

                foreach (var item in array)
                {
                    propietarioItem.Add(new PropietarioItem
                    {
                        idPropietario = item.idPropietario,
                        nombre = item.nombre,
                        fotoPerfil = "icon.png",
                    });
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }

      

        private void AgregarPropietario_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarPropietario());
        }


        // ===================// Eliminar Plantilla CRUD //====================// eliminar
        public void eliminar(object sender, EventArgs e)
        {
            var idPropietario = ((MenuItem)sender).CommandParameter;
            DisplayAlert("Eliminar", "Eliminar idPropietario = " + idPropietario, "Aceptar");
        }

        // ===================// Modificar Plantilla CRUD //====================// actualizar
        public void actualizar(object sender, EventArgs e)
        {
            var idPropietario = ((MenuItem)sender).CommandParameter;
            Navigation.PushAsync(new AgregarPropietario(idPropietario));
        }

    }
}
