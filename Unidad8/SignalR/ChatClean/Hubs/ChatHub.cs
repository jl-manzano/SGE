using Microsoft.AspNetCore.SignalR;
using ChatClean.Models;
using System.Text.Json;

namespace ChatClean.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(clsMensajeUsuario mensajeUsuario)
        {
            try
            {
                // 🔍 LOG: Ver qué estamos recibiendo
                Console.WriteLine("==========================================");
                Console.WriteLine($"📥 Recibido en SendMessage:");
                Console.WriteLine($"   - mensajeUsuario es null? {mensajeUsuario == null}");

                if (mensajeUsuario != null)
                {
                    Console.WriteLine($"   - usuario: '{mensajeUsuario.usuario}'");
                    Console.WriteLine($"   - mensaje: '{mensajeUsuario.mensaje}'");
                    Console.WriteLine($"   - IsValid: {mensajeUsuario.IsValid()}");
                }
                Console.WriteLine("==========================================");

                // Validar mensaje
                if (mensajeUsuario == null)
                {
                    Console.WriteLine("❌ ERROR: mensajeUsuario es null");
                    return;
                }

                if (string.IsNullOrWhiteSpace(mensajeUsuario.mensaje))
                {
                    Console.WriteLine("❌ ERROR: mensaje está vacío");
                    return;
                }

                // Si no tiene usuario, asignar "Anónimo"
                if (string.IsNullOrWhiteSpace(mensajeUsuario.usuario))
                {
                    mensajeUsuario.usuario = "Anónimo";
                }

                // Enviar a todos los clientes
                await Clients.All.SendAsync("ReceiveMessage", mensajeUsuario);
                Console.WriteLine("✅ Mensaje enviado exitosamente a todos los clientes");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ EXCEPCIÓN en SendMessage: {ex.Message}");
                Console.WriteLine($"   StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            Console.WriteLine($"✅ Cliente conectado: {Context.ConnectionId}");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
            Console.WriteLine($"🔌 Cliente desconectado: {Context.ConnectionId}");
        }
    }
}