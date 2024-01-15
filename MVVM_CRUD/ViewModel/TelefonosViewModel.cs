using MVVM_DEMO.Model;
using MVVM_DEMO.ViewModel.Base;
using MVVM_DEMO.Services;
using System.Windows.Input;
using System.Printing.IndexedProperties;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Windows;

namespace MVVM_DEMO.ViewModel
{
    public class TelefonosViewModel : BaseViewModel
    {
        private string ntelefono;
        private string nombre;
        private string direccion;
        private string observaciones;
        private ObservableCollection<Telefono> telefonos;
        private Telefono selected;
        private Telefono telefonoOperar;
        public string Ntelefono
        {
            get { return ntelefono; }
            set {
                ntelefono = value;
                // Zona de validación
                ClearErrors("Ntelefono");
                if (value.Length > 9)
                {
                    AddError("Ntelefono", "El número de teléfono no puede tener más de 9 dígitos");
                }
                OnPropertyChanged();

            }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; OnPropertyChanged(); }
        }
        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; OnPropertyChanged(); }
        }
        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Telefono> Telefonos
        {
            get { return telefonos; }
            set { telefonos = value; OnPropertyChanged(); }
        }

        public Telefono Selected
        {
            get { return selected; }
            set { selected = value; OnPropertyChanged(); }
        }

        public Telefono TelefonoOperar
        {
            get { return selected; }
            set { selected = value; OnPropertyChanged(); }
        }

        public ICommand GuardarCommand { get; }
        public ICommand BorrarCommand { get; }
        public ICommand ModificarCommand { get; }
        public ICommand BuscarCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand LimpiarCommand { get; }
        public ICommand SelectedItemChangedCommand { get; }

        public TelefonosViewModel()
        {  
            GuardarCommand = new RelayCommand2(PerformInsertarTelefonos, CanExecuteSaveTelefonos);
            BorrarCommand = new RelayCommand(PerfoBorrarTelefonos);
            ModificarCommand = new RelayCommand(PerformModificarTelefonos);
            BuscarCommand = new RelayCommand(PerformBuscarTelefonos);
            LimpiarCommand = new RelayCommand(PerformLimpiarTelefonos);
            LoadCommand = new RelayCommand(PerformCargarTelefonos);
            SelectedItemChangedCommand = new RelayCommand(PerformSelectedItemChangedCommand);
        }

        private bool CanExecuteSaveTelefonos(object? parameter)
        {
            Debug.WriteLine("CanExecuteSaveTelefonos");
            return !string.IsNullOrEmpty(Ntelefono);
        }
        private void PerformInsertarTelefonos(object? parameter)
        {
            TelefonosService.TelefonosServiceInsertar(new Telefono(ntelefono, nombre, direccion, observaciones));
            MessageBox.Show(String.Format("Registro {0} insertado correctamente", Ntelefono), "Confirmación inserción", MessageBoxButton.OK, MessageBoxImage.Information);
            PerformCargarTelefonos();
            PerformLimpiarTelefonos();
        }

        private void PerfoBorrarTelefonos(object? parameter = null)
        {
            // Mejora posible: Mediante Inyección de dependencias (DI) y un servicio de diálogo
            // https://stackoverflow.com/questions/45325941/mvvm-show-confirmation-messagebox-before-command-attached-to-a-button-is-execute
            // https://www.c-sharpcorner.com/article/dialogs-in-wpf-mvvm/
            var result = MessageBox.Show("¿Desea realmente borrar este registro?", "Confirmación borrado", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                TelefonosService.TelefonosServiceBorrar(new Telefono(ntelefono, nombre, direccion, observaciones));
                MessageBox.Show(String.Format("Registro {0} borrado correctamente",Ntelefono), "Confirmación borrado", MessageBoxButton.OK, MessageBoxImage.Information);
                PerformCargarTelefonos();
                PerformLimpiarTelefonos();
            }          
        }

        private void PerformModificarTelefonos(object? parameter = null)
        {
            // Mejora posible: Mediante Inyección de dependencias (DI) y un servicio de diálogo
            // https://stackoverflow.com/questions/45325941/mvvm-show-confirmation-messagebox-before-command-attached-to-a-button-is-execute
            // https://www.c-sharpcorner.com/article/dialogs-in-wpf-mvvm/
            var result = MessageBox.Show("¿Desea realmente modificar este registro?", "Confirmación modificación", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                TelefonosService.TelefonosServiceActualizar(new Telefono(ntelefono, nombre, direccion, observaciones));
                MessageBox.Show(String.Format("Registro {0} modificado correctamente", Ntelefono), "Confirmación modificación", MessageBoxButton.OK, MessageBoxImage.Information);
                PerformCargarTelefonos();
                PerformLimpiarTelefonos();
            }
        }

        private void PerformBuscarTelefonos(object? parameter = null)
        {
            TelefonosService.TelefonosServiceInsertar(new Telefono(ntelefono, nombre, direccion, observaciones));
        }
        private void PerformLimpiarTelefonos(object? parameter = null)
        {
            Ntelefono = String.Empty;
            Nombre = String.Empty; 
            Direccion = String.Empty;
            Observaciones = String.Empty;   
        }

        private void PerformCargarTelefonos(object? parameter = null)
        {
            Debug.WriteLine("Cargando teléfonos");
            Telefonos = TelefonosService.TelefonosServiceCargar();
        }

        private void PerformSelectedItemChangedCommand(object? parameter = null)
        {
            if (Selected != null)
            {
                Debug.WriteLine("PerformSelectedItemChangedCommand:" + "Selected:" + Selected.Ntelefono.ToString());

                Ntelefono = Selected.Ntelefono;
                Nombre = Selected.Nombre;
                Direccion = Selected.Direccion;
                Observaciones = Selected.Observaciones;
            }
        }

    }
}
