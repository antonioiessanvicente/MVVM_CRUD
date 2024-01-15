using MVVM_DEMO.Model;
using System.Collections.ObjectModel;

namespace MVVM_DEMO.Services
{
    public class TelefonosService
    {
        static public ObservableCollection<Telefono> TelefonosServiceCargar()
        {
            ObservableCollection<Telefono> telefonos = new ObservableCollection<Telefono>();

            using (TelefonosADO tado = new TelefonosADO())
            {
                foreach (var item in tado.Listar())
                {
                    telefonos.Add(item);
                }
            }
            return telefonos;
        }
        static public Telefono TelefonosServiceCargar(string id)
        {
            Telefono telefono = new Telefono();

            using (TelefonosADO tado = new TelefonosADO())
            {
                telefono = tado.Listar(id);
            }
            return telefono;
        }
        static public void TelefonosServiceInsertar(Telefono t)
        {
            using (TelefonosADO tado = new TelefonosADO())
            {
                tado.Insertar(t);
            }
        }

        static public void TelefonosServiceActualizar(Telefono t)
        {
            using (TelefonosADO tado = new TelefonosADO())
            {
                tado.Actualizar(t);
            }
        }
        static public void TelefonosServiceBorrar(Telefono t)
        {
            using (TelefonosADO tado = new TelefonosADO())
            {
                tado.Borrar(t);
            }
        }

    }
}
