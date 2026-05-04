using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalilDeadlift.Domain
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string CreatedByName { get; set; } = Constants.SystemUser.Name;
        public Guid CreatedById { get; set; } = Constants.SystemUser.Id;
        public DateTime? Updated { get; set; }
        public string UpdatedByName { get; set; } = Constants.SystemUser.Name;
        public Guid UpdatedById { get; set; } = Constants.SystemUser.Id;
        public bool IsDeleted { get; set; } = false;
        public DateTime? Deleted { get; set; }
    }
}
