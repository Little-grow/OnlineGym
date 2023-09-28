using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineGym.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfSets { get; set; }
        public int NumberOfRounds { get; set; }

        public Byte[] Image { get; set; }

        public string EquipmentName { get; set; }
    }
}
