import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Persona } from '../../models/persona';

@Component({
  selector: 'app-formulario-persona',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './formulario-persona.html',
  styleUrls: ['./formulario-persona.css'],
})
export class FormularioPersona {
  nombre = '';
  apellidos = '';

  @Output() personaCreada = new EventEmitter<Persona>();

  guardar() {
    const n = this.nombre.trim();
    const a = this.apellidos.trim();
    if (!n || !a) return;

    this.personaCreada.emit({ nombre: n, apellidos: a });

    // limpiar
    this.nombre = '';
    this.apellidos = '';
  }
}