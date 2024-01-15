using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MVVM_DEMO.Model
{
    [Table("telefonos")]
    public partial class Telefono
    {
        [Key]
        [Column("ntelefono")]
        [StringLength(12)]
        [Unicode(false)]
        public string Ntelefono { get; set; } = null!;
        [Column("nombre")]
        [StringLength(30)]
        [Unicode(false)]
        public string? Nombre { get; set; }
        [Column("direccion")]
        [StringLength(30)]
        [Unicode(false)]
        public string? Direccion { get; set; }
        [Column("observaciones")]
        [StringLength(240)]
        [Unicode(false)]
        public string? Observaciones { get; set; }

        public Telefono()
        { }

        public Telefono(string ntelefono, string nombre, string direccion, string observaciones)
        {
            Ntelefono = ntelefono;
            Nombre = nombre;
            Direccion = direccion;
            Observaciones = observaciones;

        }


    }
}
