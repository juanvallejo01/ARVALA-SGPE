using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ProduccionDiaria
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid LoteId { get; set; }
        public Lote Lote { get; set; } = null!;

        public DateTime Fecha { get; set; }

        /// <summary>
        /// Huevos de calidad AA (doble yema o tamano extra grande).
        /// </summary>
        public int HuevosAA { get; set; }

        /// <summary>
        /// Huevos de calidad A (tamano grande, sin defectos).
        /// </summary>
        public int HuevosA { get; set; }

        /// <summary>
        /// Huevos de calidad B (tamano mediano o con leve defecto).
        /// </summary>
        public int HuevosB { get; set; }

        /// <summary>
        /// Huevos rotos o no comercializables.
        /// </summary>
        public int Rotos { get; set; }

        /// <summary>
        /// Numero de aves muertas en el dia.
        /// </summary>
        public int Mortalidad { get; set; }

        /// <summary>
        /// Alimento consumido en kg durante el dia.
        /// </summary>
        public decimal AlimentoConsumidoKg { get; set; }

        public int GetTotalHuevosComerciales()
        {
            return HuevosAA + HuevosA + HuevosB;
        }

        public int GetTotalHuevosBrutos()
        {
            return HuevosAA + HuevosA + HuevosB + Rotos;
        }
    }
}
