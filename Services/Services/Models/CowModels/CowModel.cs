

using Services.Models.MilkModels;

namespace Services.Models.CowModels
{
    public class CowModel
    {
        public Guid Id { get; set; }
        public string Race { get; set; }
        public IList<MilkModel> Milks { get; set; } = new List<MilkModel>();
        
    }
}
