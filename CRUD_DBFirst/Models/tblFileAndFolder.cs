using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_DBFirst.Models
{
    [Table("tblFileAndFolder")]
    public class tblFileAndFolder
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Modified { get; set; }
        public string ModifiedBy { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string PhotoPath { get; set; }
        public bool IsActive { get; set; }
    }
}
