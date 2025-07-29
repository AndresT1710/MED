namespace SMED.BackEnd.Services.Interface
{
        public interface IRoleService
        {
            List<string> GetAllowedModules(int? healthProfessionalTypeId, string? professionalTypeName);
            bool HasAccessToModule(List<string> userModules, string moduleKey);
        }

}
