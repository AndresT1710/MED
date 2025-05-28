using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public int? GenderId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; }


        // Navigation properties

        [ForeignKey("GenderId")]
        [InverseProperty("Persons")]
        public virtual Gender? Gender { get; set; }

        [InverseProperty("PersonNavigation")]
        public virtual ICollection<PersonMedicalInsurance> PersonMedicalInsurances { get; set; } = new List<PersonMedicalInsurance>();

        [InverseProperty("PersonNavigation")]
        public virtual Patient? Patient { get; set; }

        [InverseProperty("PersonNavigation")]
        public virtual HealthProfessional? HealthProfessional { get; set; }

        [InverseProperty("PersonNavigation")]
        public virtual User? User { get; set; }

        [InverseProperty("PersonNavigation")]
        public virtual PersonAddress? PersonAddress { get; set; }

        [InverseProperty("PersonNavigation")]
        public virtual PersonDocument? PersonDocument { get; set; }

        [InverseProperty("PersonNavigation")]
        public virtual PersonMaritalStatus? PersonMaritalStatus { get; set; }

        [InverseProperty("PersonNavigation")]
        public virtual PersonBloodGroup? PersonBloodGroup { get; set; }

        [InverseProperty("PersonNavigation")]
        public virtual PersonEducation? PersonEducation { get; set; }

        [InverseProperty("PersonNavigation")]
        public virtual PersonLaterality? PersonLaterality { get; set; }

        [InverseProperty("PersonNavigation")]
        public virtual PersonResidence? PersonResidence { get; set; }       
                
        [InverseProperty("PersonNavigation")]
        public virtual PersonReligion? PersonReligion { get; set; }
                
        [InverseProperty("PersonNavigation")]
        public virtual PersonPhone? PersonPhone { get; set; }

        [InverseProperty("PersonNavigation")]
        public virtual ICollection<PersonLaborActivity> PersonLaborActivity { get; set; } = new List<PersonLaborActivity>();

        [InverseProperty("Person")]
        public virtual ICollection<PersonProfession> PersonProfessions { get; set; } = new List<PersonProfession>();

    }
}
