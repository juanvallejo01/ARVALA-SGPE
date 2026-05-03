using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InventarioAlimento
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string TipoAlimento { get; set; } = string.Empty;

        public decimal CantidadKg { get; set; }

        public Guid LoteId { get; set; }
        public Lote Lote { get; set; } = null!;

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        public void Consumir(decimal kg)
        {
            if (kg > CantidadKg)
            {
                throw new InvalidOperationException("La cantidad a consumir supera el inventario disponible.");
            }
            CantidadKg -= kg;
        }

        public void Reponer(decimal kg)
        {
            CantidadKg += kg;
        }
    }
}
