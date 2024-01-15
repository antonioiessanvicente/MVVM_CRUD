using Microsoft.EntityFrameworkCore;
using MVVM_DEMO.Model;
namespace MVVM_DEMO.Services
{
    public class TelefonosADO : IDisposable
    {
        // Flag: Has Dispose already been called?
        bool disposed;

        public TelefonosADO()
        {
            // Flag: Has Dispose already been called?
            disposed = false;
        }
        public IList<Telefono> Listar()
        {
            using (var context = new EEFContext())
            {
                // Return the list of data from the database
                var data = context.Telefonos.ToList();
                return data;
            }
        }
        public Telefono Listar(String ID)
        {
            using (var _context = new EEFContext())
            {
                var query = from st in _context.Telefonos
                            where st.Ntelefono == ID
                            select st;

                var telefono = query.FirstOrDefault<Telefono>();
                return telefono;
            }

        }
        public void Insertar(Telefono dato)
        {
            using (var context = new EEFContext())
            {
                context.Entry(dato).State = EntityState.Added;
                context.SaveChanges();
            }
        }
        // El Telefono modificado contiene los datos modificados, y Entry busca por la Clave Primaria
        public void Actualizar(Telefono modificado)
        {
            using (var context = new EEFContext())
            {
                context.Entry(modificado).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void Borrar(Telefono dato)
        {
            using (var context = new EEFContext())
            {
                context.Entry(dato).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //Liberar recursos no manejados como ficheros, conexiones a bd, etc.
            }

            disposed = true;
        }
    }
}
