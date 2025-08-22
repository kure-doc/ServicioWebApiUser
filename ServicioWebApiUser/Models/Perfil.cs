﻿using System.ComponentModel.DataAnnotations;

namespace ServicioWebApiUser.Models
{
    public class Perfil
    {
        [Key]
        public int IdPerfil { get; set; }
        public string? IdCliente { get; set; }
        public string? Nombre { get; set; }
        public string? Contrasenia { get; set; }
    }
}
