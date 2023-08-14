using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleAPI.Entities
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName ="nvarchar(max)")]
        public string Chassi { get; set; } = "";
        [Column(TypeName = "nvarchar(20)")]
        public string Model { get; set; } = "";

    }
}
