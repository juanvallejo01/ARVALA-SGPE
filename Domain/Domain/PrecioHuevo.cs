using System.ComponentModel.DataAnnotations;

namespace Domain
{
    /// <summary>
    /// Precio vigente por clasificación de huevo para el catálogo de clientes.
    /// </summary>
    public class PrecioHuevo
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>AA, A o B</summary>
        [Required]
        public string Clasificacion { get; set; } = string.Empty;

        public decimal PrecioUnitario { get; set; }

        public decimal PrecioPorDocena { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public bool Disponible { get; set; } = true;

        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
    }
}
