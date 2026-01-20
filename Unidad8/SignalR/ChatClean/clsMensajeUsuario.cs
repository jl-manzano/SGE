using System.Text.Json.Serialization;

namespace ChatClean.Models
{
    public class clsMensajeUsuario
    {
        [JsonPropertyName("usuario")]
        public string usuario { get; set; }

        [JsonPropertyName("mensaje")]
        public string mensaje { get; set; }

        public clsMensajeUsuario()
        {
            this.usuario = string.Empty;
            this.mensaje = string.Empty;
        }

        public clsMensajeUsuario(string usuario, string mensaje)
        {
            this.usuario = usuario;
            this.mensaje = mensaje;
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(this.mensaje);
        }
    }
}