using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public int? GenderId { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? SecondLastName { get; set; }
        public DateTime? BirthDate { get; set; }

        public string Email { get; set; }

        //GENDER
        public GenderDTO? Gender { get; set; }

        //ADDRESS
        public List<PersonAddressDTO>? Address { get; set; }

        //PHONE
        public PersonPhoneDTO? Phone { get; set; }

        //MARITAL STATUS
        public MaritalStatusDTO MaritalStatus { get; set; }

        //MEDICAL INSURANCE
        public List<MedicalInsuranceDTO> MedicalInsurance { get; set; } = new();

        //DOCUMENTS
        public PersonDocumentDTO? Document { get; set; }

        //LATERALITY
        public PersonLateralityDTO? Laterality { get; set; }

        //RELIGION
        public ReligionDTO? Religion { get; set; }

        //RESIDENCE
        public PersonResidenceDTO? Residence { get; set; }

        //HEALTHPROFESSIONAL
        public HealthProfessionalDTO? HealthProfessional { get; set; }

        //BLOODGROUP
        public BloodGroupDTO? BloodGroup { get; set; }

        //PROFESSIONS
        public List<ProfessionDTO> Professions { get; set; }

        //LABORACTIVITY
        public List<LaborActivityDTO> LaborActivity { get; set; } = new();

        //EDUCATIONLEVEL
        public EducationLevelDTO? EducationLevel { get; set; }

        // Nueva propiedad agregada
        public ClinicalHistoryDTO? ClinicalHistory { get; set; }
    }
}
