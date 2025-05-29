using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PersonRepository : IRepository<PersonDTO, int>
    {
        private readonly SGISContext _context;

        public PersonRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PersonDTO>> GetAllAsync()
        {
            var persons = await _context.Persons
                .Include(p => p.PersonAddress)
                .Include(p => p.PersonPhone)
                .Include(p => p.PersonMedicalInsurances)
                    .ThenInclude(psm => psm.MedicalInsuranceNavigation)
                .Include(p => p.PersonMaritalStatus)
                    .ThenInclude(pec => pec.MaritalStatusNavigation)
                .Include(p => p.PersonDocument)
                    .ThenInclude(pd => pd.DocumentTypeNavigation)
                .Include(p => p.PersonLaterality)
                    .ThenInclude(pl => pl.LateralityNavigation)
                .Include(p => p.PersonReligion)
                    .ThenInclude(pr => pr.ReligionNavigation)
                .Include(p => p.PersonResidence)
                    .ThenInclude(pr => pr.CityNavigation)
                    .ThenInclude(pr => pr.ProvinceNavigation)
                .Include (p => p.HealthProfessional)
                    .ThenInclude(ps => ps.HealthProfessionalTypeNavigation)
                .Include(p => p.PersonBloodGroup)
                    .ThenInclude(pg => pg.BloodGroupNavigation)
                .Include(p => p.PersonProfessions)
                    .ThenInclude(pp => pp.ProfessionNavigation)
                .Include(p => p.PersonLaborActivity)
                    .ThenInclude(pa => pa.LaborActivityNavigation)
                .Include(p => p.PersonEducation)
                    .ThenInclude(pi => pi.EducationLevelNavigation)

                    .ToListAsync();

            return persons.Select(p => MapToDTO(p)).ToList();


        }

        public async Task<PersonDTO?> GetByIdAsync(int id)
        {
            var person = await _context.Persons
                .Include(p => p.PersonAddress)
                .Include(p => p.PersonPhone)
                .Include(p => p.PersonMedicalInsurances)
                    .ThenInclude(psm => psm.MedicalInsuranceNavigation)
                .Include(p => p.PersonMaritalStatus)
                    .ThenInclude(pec => pec.MaritalStatusNavigation)
                .Include(p => p.PersonDocument)
                    .ThenInclude(pd => pd.DocumentTypeNavigation)
                .Include(p => p.PersonLaterality)
                    .ThenInclude(pl => pl.LateralityNavigation)
                .Include(p => p.PersonReligion)
                    .ThenInclude(pr => pr.ReligionNavigation)
                .Include(p => p.PersonResidence)
                    .ThenInclude(pr => pr.CityNavigation)
                    .ThenInclude(pr => pr.ProvinceNavigation)
                .Include(p => p.HealthProfessional)
                    .ThenInclude(ps => ps.HealthProfessionalTypeNavigation)
                .Include(p => p.PersonBloodGroup)
                    .ThenInclude(pg => pg.BloodGroupNavigation)
                .Include(p => p.PersonProfessions)
                    .ThenInclude(pp => pp.ProfessionNavigation)
                .Include(p => p.PersonLaborActivity)
                    .ThenInclude(pa => pa.LaborActivityNavigation)
                .Include(p => p.PersonEducation)
                    .ThenInclude(pi => pi.EducationLevelNavigation)
                
                    .FirstOrDefaultAsync(p => p.Id == id);
            return person == null ? null : MapToDTO(person);
        }

        public async Task<PersonDTO> AddAsync(PersonDTO dto)
        {
            // 1. Creat a new Person entity
            var person = new Person
            {
                GenderId = dto.GenderId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                SecondLastName = dto.SecondLastName,
                BirthDate = dto.BirthDate,
                Email = dto.Email,
            };

            // 2. Save person to get ID
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            // 3. Create and associate relationships using the generated ID
            if (dto.Address?.FirstOrDefault() is PersonAddressDTO dir)
            {
                var personaddress = new PersonAddress
                {
                    PersonId = person.Id,
                    MainStreet = dir.MainStreet,
                    SecondaryStreet1 = dir.SecondaryStreet1,
                    SecondaryStreet2 = dir.SecondaryStreet2,
                    HouseNumber = dir.HouseNumber,
                    Reference = dir.Reference
                };
                _context.PersonAddresses.Add(personaddress);
            }

            if (dto.Phone is PersonPhoneDTO tel)
            {
                var personaTelefono = new PersonPhone
                {
                    PersonId = person.Id,
                    Landline = tel.Landline,
                    Mobile = tel.Mobile
                };
                _context.PersonPhones.Add(personaTelefono);
            }

            if (dto.MaritalStatus is MaritalStatusDTO maritalStatus)
            {
                var personMaritalStatus = new PersonMaritalStatus
                {
                    PersonId = person.Id,
                    MaritalStatusId = maritalStatus.Id
                };
                _context.PersonMaritalStatuses.Add(personMaritalStatus);
            }


            if(dto.MedicalInsurance != null && dto.MedicalInsurance.Any())
            {
                var insurances = dto.MedicalInsurance.Select(s => new PersonMedicalInsurance
                {
                    PersonId = person.Id,
                    MedicalInsuranceId = s.Id
                }).ToList();
                _context.PersonMedicalInsurances.AddRange(insurances);
            }

            if (dto.Document is PersonDocumentDTO doc)
            {
                var personDocument = new PersonDocument
                {
                    PersonId = person.Id,
                    DocumentTypeId = doc.DocumentTypeId,
                    DocumentNumber = doc.DocumentNumber
                };
                _context.PersonDocuments.Add(personDocument);
            }

            if (dto.Laterality is PersonLateralityDTO lat)
            {
                var personLaterality = new PersonLaterality
                {
                    PersonId = person.Id,
                    LateralityId = lat.LateralityId
                };
                _context.PersonLateralities.Add(personLaterality);
            }

            if(dto.Religion is ReligionDTO religion)
            {
                var personReligion = new PersonReligion
                {
                    PersonId = person.Id,
                    ReligionId = religion.Id
                };
                _context.PersonReligions.Add(personReligion);
            }

            if (dto.Residence is PersonResidenceDTO residence)
            {
                var personResidence = new PersonResidence
                {
                    PersonId = person.Id,
                    CityId = residence.CityId
                };
                _context.PersonResidences.Add(personResidence);
            }

            if (dto.HealthProfessional is HealthProfessionalDTO prof)
            {
                var healthProfessional = new HealthProfessional
                {
                    HealthProfessionalId = person.Id,
                    HealthProfessionalTypeId = prof.HealthProfessionalTypeId,
                    RegistrationNumber = prof.RegistrationNumber
                };
                _context.HealthProfessionals.Add(healthProfessional);
            }

            if (dto.BloodGroup != null)
            {
                var personBloodGroup = new PersonBloodGroup
                {
                    PersonId = person.Id,
                    BloodGroupId = dto.BloodGroup.Id
                };
                _context.PersonBloodGroups.Add(personBloodGroup);
            }

            if (dto.Professions != null && dto.Professions.Any())
            {
                foreach (var ProfessionDTO in dto.Professions)
                {
                    if (ProfessionDTO.Id != 0 && await _context.Professions.AnyAsync(p => p.Id == ProfessionDTO.Id))
                    {
                        var personProfessions = new PersonProfession
                        {
                            PersonId = person.Id,
                            ProfessionId = ProfessionDTO.Id
                        };
                        _context.PersonProfessions.Add(personProfessions);
                    }
                    {

                    }
                }
            }

            if (dto.LaborActivity != null && dto.LaborActivity.Any())
            {
                foreach (var activityDTO in dto.LaborActivity)
                {
                    if (activityDTO.Id != 0 && await _context.LaborActivities.AnyAsync(a => a.Id == activityDTO.Id))
                    {
                        var personaActividad = new PersonLaborActivity
                        {
                            PersonId = person.Id,
                            LaborActivityId = activityDTO.Id
                        };
                        _context.PersonLaborActivities.Add(personaActividad);
                    }
                }
            }

            if (dto.EducationLevel != null)
            {
                var personEducation = new PersonEducation
                {
                    PersonId = person.Id,
                    EducationLevelId = dto.EducationLevel.Id
                };
                _context.PersonEducations.Add(personEducation);
            }

            var Patient = new Patient
            {
                PersonId = person.Id
            };

            _context.Patients.Add(Patient);

            //4. Save all changes
            await _context.SaveChangesAsync();

            //5.Consult the person again with their relationships
            var personWithRelationShips = await _context.Persons
                .Include(p => p.PersonAddress)
                .Include(p => p.PersonPhone)
                .Include(p => p.PersonMedicalInsurances)
                    .ThenInclude(psm => psm.MedicalInsuranceNavigation)
                .Include(p => p.PersonMaritalStatus)
                    .ThenInclude(pec => pec.MaritalStatusNavigation)
                .Include(p => p.PersonDocument)
                    .ThenInclude(pd => pd.DocumentTypeNavigation)
                .Include(p => p.PersonLaterality)
                    .ThenInclude(pl => pl.LateralityNavigation)
                .Include(p => p.PersonReligion)
                    .ThenInclude(pr => pr.ReligionNavigation)
                .Include(p => p.PersonResidence)
                    .ThenInclude(pr => pr.CityNavigation)
                    .ThenInclude(pr => pr.ProvinceNavigation)
                .Include(p => p.HealthProfessional)
                    .ThenInclude(ps => ps.HealthProfessionalTypeNavigation)
                .Include(p => p.PersonBloodGroup)
                    .ThenInclude(pg => pg.BloodGroupNavigation)
                .Include(p => p.PersonProfessions)
                    .ThenInclude(pp => pp.ProfessionNavigation)
                .Include(p => p.PersonLaborActivity)
                    .ThenInclude(pa => pa.LaborActivityNavigation)
                .Include(p => p.PersonEducation)
                    .ThenInclude(pi => pi.EducationLevelNavigation)

                    .FirstOrDefaultAsync(p => p.Id == person.Id);

            // 6. Return DTO
            return MapToDTO(personWithRelationShips!);
        }

        public async Task<PersonDTO> UpdateAsync(PersonDTO dto)
        {
            if (dto.Id == null) return null;

            var person = await _context.Persons
                .Include(p => p.PersonAddress)
                .Include(p => p.PersonPhone)
                .Include(p => p.PersonMedicalInsurances)
                    .ThenInclude(psm => psm.MedicalInsuranceNavigation)
                .Include(p => p.PersonMaritalStatus)
                    .ThenInclude(pec => pec.MaritalStatusNavigation)
                .Include(p => p.PersonDocument)
                    .ThenInclude(pd => pd.DocumentTypeNavigation)
                .Include(p => p.PersonLaterality)
                    .ThenInclude(pl => pl.LateralityNavigation)
                .Include(p => p.PersonReligion)
                    .ThenInclude(pr => pr.ReligionNavigation)
                .Include(p => p.PersonResidence)
                    .ThenInclude(pr => pr.CityNavigation)
                    .ThenInclude(pr => pr.ProvinceNavigation)
                .Include(p => p.HealthProfessional)
                    .ThenInclude(ps => ps.HealthProfessionalTypeNavigation)
                .Include(p => p.PersonBloodGroup)
                    .ThenInclude(pg => pg.BloodGroupNavigation)
                .Include(p => p.PersonProfessions)
                    .ThenInclude(pp => pp.ProfessionNavigation)
                .Include(p => p.PersonLaborActivity)
                    .ThenInclude(pa => pa.LaborActivityNavigation)
                .Include(p => p.PersonEducation)
                    .ThenInclude(pi => pi.EducationLevelNavigation)
                    .FirstOrDefaultAsync(p => p.Id == dto.Id);

            if (person == null) return null;

            // Update the person properties
            person.GenderId = dto.GenderId;
            person.FirstName = dto.FirstName;
            person.LastName = dto.LastName;
            person.MiddleName = dto.MiddleName;
            person.SecondLastName = dto.SecondLastName;
            person.BirthDate = dto.BirthDate;
            person.Email = dto.Email;

            // Update the address
            if (dto.Address != null && dto.Address.Any())
            {
                var dirDto = dto.Address.First();

                if (person.PersonAddress == null)
                {
                    person.PersonAddress = new PersonAddress();
                }
                person.PersonAddress.MainStreet = dirDto.MainStreet;
                person.PersonAddress.SecondaryStreet1 = dirDto.SecondaryStreet1;
                person.PersonAddress.SecondaryStreet2 = dirDto.SecondaryStreet2;
                person.PersonAddress.HouseNumber = dirDto.HouseNumber;
                person.PersonAddress.Reference = dirDto.Reference;

                person.PersonAddress.PersonId = person.Id;
            }
            // Update the phone
            if (dto.Phone != null)
            {
                if (person.PersonPhone == null)
                {
                    person.PersonPhone = new PersonPhone
                    {
                        Mobile = dto.Phone.Mobile,
                        Landline = dto.Phone.Landline,
                        PersonId = person.Id
                    };
                }
                else
                {
                    person.PersonPhone.Mobile = dto.Phone.Mobile;
                    person.PersonPhone.Landline = dto.Phone.Landline;
                }
            }

            // Update Marital Status
            if (dto.MaritalStatus != null)
            {
                if (person.PersonMaritalStatus == null)
                {
                    person.PersonMaritalStatus = new PersonMaritalStatus
                    {
                        PersonId = person.Id,
                        MaritalStatusId = dto.MaritalStatus.Id
                    };
                }
                else
                {
                    person.PersonMaritalStatus.MaritalStatusId = dto.MaritalStatus.Id;
                }
            }

            //Update Professions
            if (dto.Professions != null && dto.Professions.Any())
            {
                //Delete professions in BD
                _context.PersonProfessions.RemoveRange(person.PersonProfessions);

                //Clear the collection
                person.PersonProfessions.Clear();

                //Add
                foreach (var professionDTO in dto.Professions)
                {
                    if (professionDTO.Id != 0 && await _context.Professions.AnyAsync(p => p.Id == professionDTO.Id))
                    {
                        var personProfession = new PersonProfession
                        {
                            PersonId = person.Id,
                            ProfessionId = professionDTO.Id
                        };
                        person.PersonProfessions.Add(personProfession);
                    }
                }
            }


            //Update Medical Insurance
            if (dto.MedicalInsurance != null)
            {
                //Delete medical insurances in BD
                _context.PersonMedicalInsurances.RemoveRange(person.PersonMedicalInsurances);

                person.PersonMedicalInsurances.Clear();

                foreach (var siDto in dto.MedicalInsurance) 
                {
                    var si = new PersonMedicalInsurance
                    {
                        PersonId = person.Id,
                        MedicalInsuranceId = siDto.Id
                    };
                    person.PersonMedicalInsurances.Add(si);
                }
            }

            // Update Document
            if (dto.Document != null)
            {
                if (person.PersonDocument == null)
                {
                    person.PersonDocument = new PersonDocument
                    {
                        PersonId = person.Id,
                        DocumentTypeId = dto.Document.DocumentTypeId,
                        DocumentNumber = dto.Document.DocumentNumber
                    };
                }
                else
                {
                    person.PersonDocument.DocumentTypeId = dto.Document.DocumentTypeId;
                    person.PersonDocument.DocumentNumber = dto.Document.DocumentNumber;
                }
            }

            //Update Laterality
            if (dto.Laterality != null)
            {
                if (person.PersonLaterality == null)
                {
                    person.PersonLaterality = new PersonLaterality
                    {
                        PersonId = person.Id,
                        LateralityId = dto.Laterality.LateralityId
                    };
                }
                else
                {
                    person.PersonLaterality.LateralityId = dto.Laterality.LateralityId;
                }
            }

            //Update Religion
            if (dto.Religion != null)
            {
                if (person.PersonReligion == null)
                {
                    person.PersonReligion = new PersonReligion
                    {
                        PersonId = person.Id,
                        ReligionId = dto.Religion.Id
                    };
                }
                else
                {
                    person.PersonReligion.ReligionId = dto.Religion.Id;
                }
            }

            //Update Residence
            if (dto.Residence != null)
            {
                if (person.PersonResidence == null)
                {
                    person.PersonResidence = new PersonResidence
                    {
                        PersonId = person.Id,
                        CityId = dto.Residence.CityId
                    };
                }
                else
                {
                    person.PersonResidence.CityId = dto.Residence.CityId;
                }
            }

            //Update Health Professional
            if (dto.HealthProfessional != null)
            {
                if (person.HealthProfessional == null)
                {
                    person.HealthProfessional = new HealthProfessional
                    {
                        HealthProfessionalId = person.Id,
                        HealthProfessionalTypeId = dto.HealthProfessional.HealthProfessionalTypeId,
                        RegistrationNumber = dto.HealthProfessional.RegistrationNumber
                    };
                }
                else
                {
                    person.HealthProfessional.HealthProfessionalTypeId = dto.HealthProfessional.HealthProfessionalTypeId;
                    person.HealthProfessional.RegistrationNumber = dto.HealthProfessional.RegistrationNumber;
                }
            }

            //Update Blood Group
            if (dto.BloodGroup != null)
            {
                if (person.PersonBloodGroup == null)
                {
                    person.PersonBloodGroup = new PersonBloodGroup
                    {
                        PersonId = person.Id,
                        BloodGroupId = dto.BloodGroup.Id
                    };
                }
                else
                {
                    person.PersonBloodGroup.BloodGroupId = dto.BloodGroup.Id;
                }
            }

            //Update Labor Activity
            if (dto.LaborActivity != null)
            {
                //Delete labor activities in BD
                _context.PersonLaborActivities.RemoveRange(person.PersonLaborActivity);

                person.PersonLaborActivity.Clear();
                foreach (var activityDTO in dto.LaborActivity)
                {
                    var activity = new PersonLaborActivity
                    {
                        PersonId = person.Id,
                        LaborActivityId = activityDTO.Id
                    };
                    person.PersonLaborActivity.Add(activity);
                }
            }

            //Update Education Level
            if (dto.EducationLevel != null)
            {
                if (person.PersonEducation == null)
                {
                    person.PersonEducation = new PersonEducation
                    {
                        PersonId = person.Id,
                        EducationLevelId = dto.EducationLevel.Id
                    };
                }
                else
                {
                    person.PersonEducation.EducationLevelId = dto.EducationLevel.Id;
                }
            }

            await _context.SaveChangesAsync();

            return MapToDTO(person);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            
            if (person == null) return false;

            // Remove related entities
            _context.Patients.RemoveRange(_context.Patients.Where(p => p.PersonId == id));
            _context.PersonLaborActivities.RemoveRange(_context.PersonLaborActivities.Where(pa => pa.PersonId == id));
            _context.PersonAddresses.RemoveRange(_context.PersonAddresses.Where(pa => pa.PersonId == id));
            _context.PersonDocuments.RemoveRange(_context.PersonDocuments.Where(pd => pd.PersonId == id));
            _context.PersonMaritalStatuses.RemoveRange(_context.PersonMaritalStatuses.Where(pms => pms.PersonId == id));
            _context.PersonBloodGroups.RemoveRange(_context.PersonBloodGroups.Where(pbg => pbg.PersonId == id));
            _context.PersonEducations.RemoveRange(_context.PersonEducations.Where(pe => pe.PersonId == id));
            _context.PersonLateralities.RemoveRange(_context.PersonLateralities.Where(pl => pl.PersonId == id));
            _context.PersonResidences.RemoveRange(_context.PersonResidences.Where(pr => pr.PersonId == id));
            _context.PersonReligions.RemoveRange(_context.PersonReligions.Where(pr => pr.PersonId == id));
            _context.PersonMedicalInsurances.RemoveRange(_context.PersonMedicalInsurances.Where(pmi => pmi.PersonId == id));
            _context.PersonPhones.RemoveRange(_context.PersonPhones.Where(pp => pp.PersonId == id));
            _context.HealthProfessionals.RemoveRange(_context.HealthProfessionals.Where(hp => hp.HealthProfessionalId == id));
            _context.PersonProfessions.RemoveRange(_context.PersonProfessions.Where(pp => pp.PersonId == id));
            _context.Users.RemoveRange(_context.Users.Where(u => u.PersonId == id));
            
            // Remove the person
            _context.Persons.Remove(person);
            // Save changes
            await _context.SaveChangesAsync();
            return true;
        }


        private PersonDTO MapToDTO(Person person)
        {
            return new PersonDTO
            {
                Id = person.Id,
                GenderId = person.GenderId,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName,
                SecondLastName = person.SecondLastName,
                BirthDate = person.BirthDate,
                Email = person.Email,

                Address = person.PersonAddress != null
                ? new List<PersonAddressDTO>
                {
                    new PersonAddressDTO
                    {
                        MainStreet = person.PersonAddress.MainStreet,
                        SecondaryStreet1 = person.PersonAddress.SecondaryStreet1,
                        SecondaryStreet2 = person.PersonAddress.SecondaryStreet2,
                        HouseNumber = person.PersonAddress.HouseNumber,
                        Reference = person.PersonAddress.Reference
                    }
                } : null,

                Phone = person.PersonPhone != null
                ? new PersonPhoneDTO
                {
                    
                        Mobile = person.PersonPhone.Mobile,
                        Landline = person.PersonPhone.Landline
                } : null,

                MaritalStatus = person.PersonMaritalStatus == null || person.PersonMaritalStatus.MaritalStatusNavigation == null
                ? null
                : new MaritalStatusDTO
                {
                    Id = person.PersonMaritalStatus.MaritalStatusNavigation.Id,
                    Name = person.PersonMaritalStatus.MaritalStatusNavigation.Name
                },

                MedicalInsurance = person.PersonMedicalInsurances?
                .Where(pmi => pmi.MedicalInsuranceNavigation != null)
                .Select(pmi => new MedicalInsuranceDTO
                {
                    Id = pmi.MedicalInsuranceNavigation.Id,
                    Name = pmi.MedicalInsuranceNavigation.Name,
                }).ToList(),

                Document = person.PersonDocument == null ? null : new PersonDocumentDTO
                {
                    PersonId = person.PersonDocument.PersonId,
                    DocumentTypeId = person.PersonDocument.DocumentTypeId,
                    DocumentTypeName = person.PersonDocument.DocumentTypeNavigation?.Name,
                    DocumentNumber = person.PersonDocument.DocumentNumber
                },

                Laterality = person.PersonLaterality == null ? null : new PersonLateralityDTO
                {
                    PersonId = person.PersonLaterality.PersonId,
                    LateralityId = person.PersonLaterality.LateralityId,
                    NameLaterality = person.PersonLaterality.LateralityNavigation?.Name
                },

                Religion = person.PersonReligion == null ? null : new ReligionDTO
                {
                    Id = person.PersonReligion.ReligionNavigation?.Id ?? 0,
                    Name = person.PersonReligion.ReligionNavigation?.Name
                },

                Residence = person.PersonResidence != null
                ? new PersonResidenceDTO
                {
                    CityId = person.PersonResidence.CityId,
                    CityName = person.PersonResidence.CityNavigation?.Name,
                    ProvinceId = person.PersonResidence.CityNavigation?.ProvinceNavigation?.Id,
                    ProvinceName = person.PersonResidence.CityNavigation?.ProvinceNavigation?.Name
                } : null,

                HealthProfessional = person.HealthProfessional != null
                ? new HealthProfessionalDTO
                {
                    HealthProfessionalId = person.HealthProfessional.HealthProfessionalId,
                    HealthProfessionalTypeId = person.HealthProfessional.HealthProfessionalTypeId,
                    RegistrationNumber = person.HealthProfessional.RegistrationNumber,
                    NameTypeProfessional = person.HealthProfessional.HealthProfessionalTypeNavigation?.Name,
                } : null,

                BloodGroup = person.PersonBloodGroup != null && person.PersonBloodGroup.BloodGroupNavigation != null
                ? new BloodGroupDTO
                {
                    Id = person.PersonBloodGroup.BloodGroupNavigation.Id,
                    Name = person.PersonBloodGroup.BloodGroupNavigation?.Name
                }: null,

                Professions = person.PersonProfessions?
                    .Where(pp => pp.ProfessionNavigation != null)
                    .Select(pp => new ProfessionDTO
                    {
                        Id = pp.ProfessionNavigation.Id,
                        Name = pp.ProfessionNavigation.Name
                    }).ToList() ?? new List<ProfessionDTO>(),

                LaborActivity = person.PersonLaborActivity ?
                    .Where(pa => pa.LaborActivityNavigation != null)
                    .Select(pa => new LaborActivityDTO
                    {
                        Id = pa.LaborActivityNavigation.Id,
                        Name = pa.LaborActivityNavigation.Name
                    }).ToList() ?? new List<LaborActivityDTO>(),

                EducationLevel = person.PersonEducation != null && person.PersonEducation.EducationLevelNavigation != null
                ? new EducationLevelDTO
                {
                    Id = person.PersonEducation.EducationLevelNavigation.Id,
                    Name = person.PersonEducation.EducationLevelNavigation.Name
                } : null
            };
        }

    }
}
