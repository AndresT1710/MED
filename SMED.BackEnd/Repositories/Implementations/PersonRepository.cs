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
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Crear nueva entidad Person
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

                _context.Persons.Add(person);
                await _context.SaveChangesAsync();

                // 2. Crear relaciones usando el ID generado
                await CreatePersonRelationships(person.Id, dto);

                // 3. Crear Patient
                var patient = new Patient
                {
                    PersonId = person.Id
                };
                _context.Patients.Add(patient);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // 4. Consultar persona con relaciones
                return await GetByIdAsync(person.Id) ?? throw new Exception("Error al crear la persona");
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<PersonDTO> UpdateAsync(PersonDTO dto)
        {
            if (dto.Id == null) return null;

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var person = await _context.Persons
                    .Include(p => p.PersonAddress)
                    .Include(p => p.PersonPhone)
                    .Include(p => p.PersonMedicalInsurances)
                    .Include(p => p.PersonMaritalStatus)
                    .Include(p => p.PersonDocument)
                    .Include(p => p.PersonLaterality)
                    .Include(p => p.PersonReligion)
                    .Include(p => p.PersonResidence)
                    .Include(p => p.HealthProfessional)
                    .Include(p => p.PersonBloodGroup)
                    .Include(p => p.PersonProfessions)
                    .Include(p => p.PersonLaborActivity)
                    .Include(p => p.PersonEducation)
                    .FirstOrDefaultAsync(p => p.Id == dto.Id);

                if (person == null) return null;

                // Actualizar propiedades básicas
                person.GenderId = dto.GenderId;
                person.FirstName = dto.FirstName;
                person.LastName = dto.LastName;
                person.MiddleName = dto.MiddleName;
                person.SecondLastName = dto.SecondLastName;
                person.BirthDate = dto.BirthDate;
                person.Email = dto.Email;

                // Actualizar relaciones simples
                await UpdateSimpleRelationships(person, dto);

                // Actualizar relaciones muchos-a-muchos
                await UpdateManyToManyRelationships(person, dto);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await GetByIdAsync(person.Id);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
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
        private async Task CreatePersonRelationships(int personId, PersonDTO dto)
        {
            // Dirección
            if (dto.Address?.FirstOrDefault() is PersonAddressDTO dir)
            {
                var personAddress = new PersonAddress
                {
                    PersonId = personId,
                    MainStreet = dir.MainStreet,
                    SecondaryStreet1 = dir.SecondaryStreet1,
                    SecondaryStreet2 = dir.SecondaryStreet2,
                    HouseNumber = dir.HouseNumber,
                    Reference = dir.Reference
                };
                _context.PersonAddresses.Add(personAddress);
            }

            // Teléfono
            if (dto.Phone is PersonPhoneDTO tel)
            {
                var personPhone = new PersonPhone
                {
                    PersonId = personId,
                    Landline = tel.Landline,
                    Mobile = tel.Mobile
                };
                _context.PersonPhones.Add(personPhone);
            }

            // Estado Civil
            if (dto.MaritalStatus is MaritalStatusDTO maritalStatus)
            {
                var personMaritalStatus = new PersonMaritalStatus
                {
                    PersonId = personId,
                    MaritalStatusId = maritalStatus.Id
                };
                _context.PersonMaritalStatuses.Add(personMaritalStatus);
            }

            // Documento
            if (dto.Document is PersonDocumentDTO doc)
            {
                var personDocument = new PersonDocument
                {
                    PersonId = personId,
                    DocumentTypeId = doc.DocumentTypeId,
                    DocumentNumber = doc.DocumentNumber
                };
                _context.PersonDocuments.Add(personDocument);
            }

            // Lateralidad
            if (dto.Laterality is PersonLateralityDTO lat)
            {
                var personLaterality = new PersonLaterality
                {
                    PersonId = personId,
                    LateralityId = lat.LateralityId
                };
                _context.PersonLateralities.Add(personLaterality);
            }

            // Religión
            if (dto.Religion is ReligionDTO religion)
            {
                var personReligion = new PersonReligion
                {
                    PersonId = personId,
                    ReligionId = religion.Id
                };
                _context.PersonReligions.Add(personReligion);
            }

            // Residencia
            if (dto.Residence is PersonResidenceDTO residence)
            {
                var personResidence = new PersonResidence
                {
                    PersonId = personId,
                    CityId = residence.CityId
                };
                _context.PersonResidences.Add(personResidence);
            }

            // Profesional de Salud
            if (dto.HealthProfessional is HealthProfessionalDTO prof)
            {
                var healthProfessional = new HealthProfessional
                {
                    HealthProfessionalId = personId,
                    HealthProfessionalTypeId = prof.HealthProfessionalTypeId,
                    RegistrationNumber = prof.RegistrationNumber
                };
                _context.HealthProfessionals.Add(healthProfessional);
            }

            // Grupo Sanguíneo
            if (dto.BloodGroup != null)
            {
                var personBloodGroup = new PersonBloodGroup
                {
                    PersonId = personId,
                    BloodGroupId = dto.BloodGroup.Id
                };
                _context.PersonBloodGroups.Add(personBloodGroup);
            }

            // Nivel de Educación
            if (dto.EducationLevel != null)
            {
                var personEducation = new PersonEducation
                {
                    PersonId = personId,
                    EducationLevelId = dto.EducationLevel.Id
                };
                _context.PersonEducations.Add(personEducation);
            }

            // RELACIONES MUCHOS-A-MUCHOS

            // Seguros Médicos
            if (dto.MedicalInsurance != null && dto.MedicalInsurance.Any())
            {
                var validInsurances = dto.MedicalInsurance
                    .Where(s => s.Id > 0)
                    .Select(s => new PersonMedicalInsurance
                    {
                        PersonId = personId,
                        MedicalInsuranceId = s.Id
                    }).ToList();

                if (validInsurances.Any())
                {
                    _context.PersonMedicalInsurances.AddRange(validInsurances);
                }
            }

            // Profesiones
            if (dto.Professions != null && dto.Professions.Any())
            {
                var validProfessions = dto.Professions
                    .Where(p => p.Id > 0)
                    .Select(p => new PersonProfession
                    {
                        PersonId = personId,
                        ProfessionId = p.Id
                    }).ToList();

                if (validProfessions.Any())
                {
                    _context.PersonProfessions.AddRange(validProfessions);
                }
            }

            // Actividades Laborales
            if (dto.LaborActivity != null && dto.LaborActivity.Any())
            {
                var validActivities = dto.LaborActivity
                    .Where(a => a.Id > 0)
                    .Select(a => new PersonLaborActivity
                    {
                        PersonId = personId,
                        LaborActivityId = a.Id
                    }).ToList();

                if (validActivities.Any())
                {
                    _context.PersonLaborActivities.AddRange(validActivities);
                }
            }
        }
        private async Task UpdateSimpleRelationships(Person person, PersonDTO dto)
        {
            // Actualizar dirección
            if (dto.Address != null && dto.Address.Any())
            {
                var dirDto = dto.Address.First();
                if (person.PersonAddress == null)
                {
                    person.PersonAddress = new PersonAddress { PersonId = person.Id };
                }
                person.PersonAddress.MainStreet = dirDto.MainStreet;
                person.PersonAddress.SecondaryStreet1 = dirDto.SecondaryStreet1;
                person.PersonAddress.SecondaryStreet2 = dirDto.SecondaryStreet2;
                person.PersonAddress.HouseNumber = dirDto.HouseNumber;
                person.PersonAddress.Reference = dirDto.Reference;
            }

            // Actualizar teléfono
            if (dto.Phone != null)
            {
                if (person.PersonPhone == null)
                {
                    person.PersonPhone = new PersonPhone { PersonId = person.Id };
                }
                person.PersonPhone.Mobile = dto.Phone.Mobile;
                person.PersonPhone.Landline = dto.Phone.Landline;
            }

            // Actualizar estado civil
            if (dto.MaritalStatus != null)
            {
                if (person.PersonMaritalStatus == null)
                {
                    person.PersonMaritalStatus = new PersonMaritalStatus { PersonId = person.Id };
                }
                person.PersonMaritalStatus.MaritalStatusId = dto.MaritalStatus.Id;
            }

            // Actualizar documento
            if (dto.Document != null)
            {
                if (person.PersonDocument == null)
                {
                    person.PersonDocument = new PersonDocument { PersonId = person.Id };
                }
                person.PersonDocument.DocumentTypeId = dto.Document.DocumentTypeId;
                person.PersonDocument.DocumentNumber = dto.Document.DocumentNumber;
            }

            // Actualizar lateralidad
            if (dto.Laterality != null)
            {
                if (person.PersonLaterality == null)
                {
                    person.PersonLaterality = new PersonLaterality { PersonId = person.Id };
                }
                person.PersonLaterality.LateralityId = dto.Laterality.LateralityId;
            }

            // Actualizar religión
            if (dto.Religion != null)
            {
                if (person.PersonReligion == null)
                {
                    person.PersonReligion = new PersonReligion { PersonId = person.Id };
                }
                person.PersonReligion.ReligionId = dto.Religion.Id;
            }

            // Actualizar residencia
            if (dto.Residence != null)
            {
                if (person.PersonResidence == null)
                {
                    person.PersonResidence = new PersonResidence { PersonId = person.Id };
                }
                person.PersonResidence.CityId = dto.Residence.CityId;
            }

            // Actualizar profesional de salud
            if (dto.HealthProfessional != null)
            {
                if (person.HealthProfessional == null)
                {
                    person.HealthProfessional = new HealthProfessional { HealthProfessionalId = person.Id };
                }
                person.HealthProfessional.HealthProfessionalTypeId = dto.HealthProfessional.HealthProfessionalTypeId;
                person.HealthProfessional.RegistrationNumber = dto.HealthProfessional.RegistrationNumber;
            }

            // Actualizar grupo sanguíneo
            if (dto.BloodGroup != null)
            {
                if (person.PersonBloodGroup == null)
                {
                    person.PersonBloodGroup = new PersonBloodGroup { PersonId = person.Id };
                }
                person.PersonBloodGroup.BloodGroupId = dto.BloodGroup.Id;
            }

            // Actualizar nivel de educación
            if (dto.EducationLevel != null)
            {
                if (person.PersonEducation == null)
                {
                    person.PersonEducation = new PersonEducation { PersonId = person.Id };
                }
                person.PersonEducation.EducationLevelId = dto.EducationLevel.Id;
            }
        }

        private async Task UpdateManyToManyRelationships(Person person, PersonDTO dto)
        {
            // Actualizar Seguros Médicos
            if (dto.MedicalInsurance != null)
            {
                // Eliminar existentes
                _context.PersonMedicalInsurances.RemoveRange(person.PersonMedicalInsurances);
                person.PersonMedicalInsurances.Clear();

                // Agregar nuevos
                var validInsurances = dto.MedicalInsurance
                    .Where(s => s.Id > 0)
                    .Select(s => new PersonMedicalInsurance
                    {
                        PersonId = person.Id,
                        MedicalInsuranceId = s.Id
                    }).ToList();

                if (validInsurances.Any())
                {
                    person.PersonMedicalInsurances = validInsurances;
                    _context.PersonMedicalInsurances.AddRange(validInsurances);
                }
            }

            // Actualizar Profesiones
            if (dto.Professions != null)
            {
                // Eliminar existentes
                _context.PersonProfessions.RemoveRange(person.PersonProfessions);
                person.PersonProfessions.Clear();

                // Agregar nuevas
                var validProfessions = dto.Professions
                    .Where(p => p.Id > 0)
                    .Select(p => new PersonProfession
                    {
                        PersonId = person.Id,
                        ProfessionId = p.Id
                    }).ToList();

                if (validProfessions.Any())
                {
                    person.PersonProfessions = validProfessions;
                    _context.PersonProfessions.AddRange(validProfessions);
                }
            }

            // Actualizar Actividades Laborales
            if (dto.LaborActivity != null)
            {
                // Eliminar existentes
                _context.PersonLaborActivities.RemoveRange(person.PersonLaborActivity);
                person.PersonLaborActivity.Clear();

                // Agregar nuevas
                var validActivities = dto.LaborActivity
                    .Where(a => a.Id > 0)
                    .Select(a => new PersonLaborActivity
                    {
                        PersonId = person.Id,
                        LaborActivityId = a.Id
                    }).ToList();

                if (validActivities.Any())
                {
                    person.PersonLaborActivity = validActivities;
                    _context.PersonLaborActivities.AddRange(validActivities);
                }
            }
        }



    }
}
