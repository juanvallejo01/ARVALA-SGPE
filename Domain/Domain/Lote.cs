using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Lote
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime FechaRecepcion { get; set; }

        [Required]
        public string Raza { get; set; } = string.Empty;

        public string Proposito { get; set; } = "Postura"; // Postura o Engorde

        public string Estado { get; set; } = "Activo"; // Activo, Finalizado o Sacrificado

        public int CantidadInicial { get; set; }

        public Guid GalponId { get; set; }
        public Galpon Galpon { get; set; } = null!;

        public List<ProduccionDiaria> Producciones { get; set; } = new List<ProduccionDiaria>();
        public List<InventarioAlimento> Inventarios { get; set; } = new List<InventarioAlimento>();
        public List<RegistroVacunacion> Vacunaciones { get; set; } = new List<RegistroVacunacion>();

        public int GetMortalidadTotal()
        {
            int total = 0;
            foreach (var produccion in Producciones)
            {
                total += produccion.Mortalidad;
            }
            return total;
        }

        public int GetCantidadActual()
        {
            return CantidadInicial - GetMortalidadTotal();
        }

        public int GetTotalHuevos()
        {
            int total = 0;
            foreach (var produccion in Producciones)
            {
                total += produccion.HuevosAA + produccion.HuevosA + produccion.HuevosB;
            }
            return total;
        }
    }
}
