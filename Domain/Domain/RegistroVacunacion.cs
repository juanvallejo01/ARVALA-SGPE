using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class RegistroVacunacion
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid LoteId { get; set; }
        public Lote Lote { get; set; } = null!;

        public DateTime Fecha { get; set; }

        [Required]
        public string Vacuna { get; set; } = string.Empty;

        public string MetodoAplicacion { get; set; } = string.Empty;

        public int AvesVacunadas { get; set; }

        public string Observaciones { get; set; } = string.Empty;
    }
}
