using SMED.BackEnd.Services.Interface;

namespace SMED.BackEnd.Services.Implementations
{
    public class RoleService : IRoleService
    {
        public List<string> GetAllowedModules(int? healthProfessionalTypeId, string? professionalTypeName)
        {
            var modules = new List<string>();

            // Si es admin (usuario sin tipo profesional o con ID específico)
            if (!healthProfessionalTypeId.HasValue || healthProfessionalTypeId == 0)
            {
                return new List<string> { "Personas", "Historia Clínica", "Atención Médica", "Nutrición", "Enfermería", "Psicología", "Estimulación Temprana", "Fisioterapia" };
            }

            if (string.IsNullOrWhiteSpace(professionalTypeName))
                return modules;

            var typeName = professionalTypeName.ToLower().Trim();

            // Historias Clínicas - todos por el momento
            modules.Add("Historia Clínica");
            modules.Add("Personas");

            // Reglas específicas por tipo de profesional
            switch (typeName)
            {
                case "enfermero":
                case "enfermera":
                    modules.Add("Enfermería");
                    break;

                case "médico general":
                case "medico general":
                    modules.Add("Atención Médica");
                    break;

                case "nutricionista":
                    modules.Add("Nutrición");
                    break;

                case "psicólogo":
                case "psicologo":
                case "psicólogo clínico":
                case "psicologo clinico":
                    modules.Add("Estimulación Temprana");   
                        break;

                case "fisioterapeuta":
                    modules.Add("Fisioterapia");
                    break;

                case "pediatra":
                    modules.Add("Estimulación Temprana");
                    break;
            }

            return modules.Distinct().ToList();
        }

        public bool HasAccessToModule(List<string> userModules, string moduleKey)
        {
            return userModules.Contains(moduleKey, StringComparer.OrdinalIgnoreCase);
        }
    }
}
