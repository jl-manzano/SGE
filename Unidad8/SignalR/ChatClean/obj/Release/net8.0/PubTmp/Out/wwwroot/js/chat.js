"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

// Deshabilitar el botón hasta que se establezca la conexión
document.getElementById("sendButton").disabled = true;

// Recibir mensajes del servidor
connection.on("ReceiveMessage", function (mensajeJSON) {
    // Crear instancia de clsMensajeUsuario desde el JSON recibido
    var mensajeUsuario = clsMensajeUsuario.fromJSON(mensajeJSON);

    // Crear elemento de lista
    var li = document.createElement("li");
    li.textContent = `${mensajeUsuario.usuario} dice: ${mensajeUsuario.mensaje}`;

    // Añadir a la lista
    document.getElementById("messagesList").appendChild(li);

    // Auto-scroll al final
    var chatBody = document.querySelector('.chat-body');
    chatBody.scrollTop = chatBody.scrollHeight;
});

// Iniciar conexión
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    console.log("✅ Conectado a SignalR");
}).catch(function (err) {
    console.error("❌ Error de conexión:", err.toString());
});

// Enviar mensaje
document.getElementById("sendButton").addEventListener("click", function (event) {
    enviarMensaje();
    event.preventDefault();
});

// Permitir enviar con Enter
document.getElementById("messageInput").addEventListener("keypress", function (event) {
    if (event.key === "Enter") {
        enviarMensaje();
        event.preventDefault();
    }
});

function enviarMensaje() {
    var user = document.getElementById("userInput").value.trim();
    var message = document.getElementById("messageInput").value.trim();

    // Validar que el mensaje no esté vacío
    if (message === '') {
        alert('El mensaje no puede estar vacío');
        return;
    }

    // Crear instancia de clsMensajeUsuario
    var mensajeUsuario = new clsMensajeUsuario(
        user || 'Anónimo',
        message
    );

    // Validar usando el método de la clase
    if (!mensajeUsuario.isValid()) {
        alert('El mensaje no es válido');
        return;
    }

    // Enviar al servidor usando toJSON()
    connection.invoke("SendMessage", mensajeUsuario.toJSON())
        .then(function () {
            // Limpiar el input del mensaje después de enviar
            document.getElementById("messageInput").value = '';
        })
        .catch(function (err) {
            console.error("❌ Error al enviar:", err.toString());
            alert('Error al enviar el mensaje');
        });
}