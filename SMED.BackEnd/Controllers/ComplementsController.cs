using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Implementations;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplementsController : ControllerBase
    {
        private readonly SGISContext _context;

        public ComplementsController(SGISContext context)
        {
            _context = context;
        }

        //Gener
        [HttpGet("genders")]
        public async Task<ActionResult<IEnumerable<GenderDTO>>> GetRenders()
        {
            var generos = await _context.Genders.ToListAsync();
            return generos.Select(g => new GenderDTO
            {
                Id = g.Id,
                Name = g.Name
            }).ToList();
        }


        //PROVINCES
        [HttpGet("provinces")]
        public async Task<ActionResult<IEnumerable<ProvinceDTO>>> GetProvinces()
        {
            var provincias = await _context.Provinces.ToListAsync();
            return provincias.Select(p => new ProvinceDTO
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }

        // CITY BY PROVINCE
        [HttpGet("city/{provinceId}")]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCitiesByProvince(int provinceId)
        {
            var cities = await _context.Cities
                .Where(c => c.ProvinceId == provinceId)
                .ToListAsync();

            return cities.Select(c => new CityDTO
            {
                Id = c.Id,
                Name = c.Name,
                ProvinceId = c.ProvinceId
            }).ToList();
        }

        //MARITAL STATUS
        [HttpGet("marital-status")]
        public async Task<ActionResult<IEnumerable<MaritalStatusDTO>>> GetMaritalStatus()
        {
            var estadosCiviles = await _context.MaritalStatuses.ToListAsync();
            return estadosCiviles.Select(e => new MaritalStatusDTO
            {
                Id = e.Id,
                Name = e.Name
            }).ToList();
        }

        //LATERALITY
        [HttpGet("lateralities")]
        public async Task<ActionResult<IEnumerable<LateralityDTO>>> GetLateralities()
        {
            var lateralidades = await _context.LateralityTypes.ToListAsync();
            return lateralidades.Select(l => new LateralityDTO
            {
                Id = l.Id,
                Name = l.Name
            }).ToList();
        }


        //DOCUMENTTYPES
        [HttpGet("document-types")]
        public async Task<ActionResult<IEnumerable<DocumentTypeDTO>>> GetDocumentTypes()
        {
            var tiposDocumento = await _context.DocumentTypes.ToListAsync();
            return tiposDocumento.Select(td => new DocumentTypeDTO
            {
                Id = td.Id,
                Name = td.Name
            }).ToList();
        }

        //medical insurance
        [HttpGet("medical-insurances")]
        public async Task<ActionResult<IEnumerable<MedicalInsuranceDTO>>> GetMedicalInsurances()
        {
            var seguros = await _context.MedicalInsurances.ToListAsync();
            return seguros.Select(s => new MedicalInsuranceDTO
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }

        //RELIGIONES
        [HttpGet("religions")]
        public async Task<ActionResult<IEnumerable<ReligionDTO>>> GetReligions()
        {
            var religiones = await _context.Religions.ToListAsync();
            return religiones.Select(r => new ReligionDTO
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();
        }

        //Blood Group
        [HttpGet("blood-groups")]
        public async Task<ActionResult<IEnumerable<BloodGroupDTO>>> GetBloodGroup()
        {
            var gruposSanguineos = await _context.BloodGroups.ToListAsync();
            return gruposSanguineos.Select(gs => new BloodGroupDTO
            {
                Id = gs.Id,
                Name = gs.Name
            }).ToList();
        }

        //PROFESSIONS
        [HttpGet("professions")]
        public async Task<ActionResult<IEnumerable<ProfessionDTO>>> GetProfessions()
        {
            var profesiones = await _context.Professions.ToListAsync();
            return profesiones.Select(p => new ProfessionDTO
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }

        //LABOR ACTIVITY
        [HttpGet("labor-activities")]
        public async Task<ActionResult<IEnumerable<LaborActivityDTO>>> GetLaborActivities()
        {
            var actividadesLaborales = await _context.LaborActivities.ToListAsync();
            return actividadesLaborales.Select(la => new LaborActivityDTO
            {
                Id = la.Id,
                Name = la.Name
            }).ToList();

        }

        //EDUCATION LEVELS
        [HttpGet("education-levels")]
        public async Task<ActionResult<IEnumerable<EducationLevelDTO>>> GetEducationLevels()
        {
            var nivelesEducativos = await _context.EducationLevels.ToListAsync();
            return nivelesEducativos.Select(ne => new EducationLevelDTO
            {
                Id = ne.Id,
                Name = ne.Name
            }).ToList();
        }

        //HEALTH PROFESSIONAL TYPE
        [HttpGet("health-professional-types")]
        public async Task<ActionResult<IEnumerable<HealthProfessionalTypeDTO>>> GetHealthProfessionalTypes()
        {
            var tiposProfesionalesSalud = await _context.HealthProfessionalTypes.ToListAsync();
            return tiposProfesionalesSalud.Select(tps => new HealthProfessionalTypeDTO
            {
                Id = tps.Id,
                Name = tps.Name
            }).ToList();
        }
    }
}
