using System;
using System.ComponentModel.DataAnnotations;

namespace clientes{
    public class Clientes{
        private int clienteId;
        private string nombreCliente ;
        private string emailCliente;
        private string telefonoCliente;


        public int ClienteId { get => clienteId; set => clienteId = value; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }

        [EmailAddress(ErrorMessage = "Debe tener formato de correo electronico")]
        public string EmailCliente { get => emailCliente; set => emailCliente = value; }

        [Phone(ErrorMessage = "Debe tener formato de número de telefono")]
        public string TelefonoCliente { get => telefonoCliente; set => telefonoCliente = value; }
        public Clientes(int clienteId, string nombreCliente, string emailCliente, string telefonoCliente)
        {
            this.ClienteId = clienteId;
            this.NombreCliente = nombreCliente;
            this.EmailCliente = emailCliente;
            this.TelefonoCliente = telefonoCliente;
        }

        public Clientes(){}
    }
}