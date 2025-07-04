﻿namespace SMED.Shared.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public int? PersonId { get; set; }
        public string? Password { get; set; } // Esto se usará internamente para validar el login
        public bool? IsActive { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
    }
}
