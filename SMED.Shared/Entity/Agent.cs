using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public class Agent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AgentId { get; set; }

        public int IdentificationNumber { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? SecondLastName { get; set; }

        public string? PhoneNumber { get; set; }
        public string? CellphoneNumber { get; set; }

        public int? DocumentType { get; set; }
        [ForeignKey("DocumentType")]
        public virtual DocumentType? DocumentTypeNavigation { get; set; }

        public int? GenderId { get; set; }
        [ForeignKey("GenderId")]
        public virtual Gender? Gender { get; set; }

        public int? MaritalStatusId { get; set; }
        [ForeignKey("MaritalStatusId")]
        public virtual MaritalStatus? MaritalStatus { get; set; }

        [InverseProperty("Agent")]
        public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
    }
}