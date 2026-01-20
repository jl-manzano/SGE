/**
 * Clase clsMensajeUsuario en JavaScript
 */
class clsMensajeUsuario {
    constructor(usuario = '', mensaje = '') {
        this.usuario = usuario;
        this.mensaje = mensaje;
    }

    static fromJSON(json) {
        return new clsMensajeUsuario(
            json.usuario || '',
            json.mensaje || ''
        );
    }

    toJSON() {
        return {
            usuario: this.usuario,
            mensaje: this.mensaje
        };
    }

    isValid() {
        return this.mensaje.trim().length > 0;
    }
}

// Log para verificar que se cargó
console.log('✅ clsMensajeUsuario.js cargado correctamente');