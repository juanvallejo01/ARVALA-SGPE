using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Galpon
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public List<Lote> Lotes { get; set; } = new List<Lote>();
    }
}
