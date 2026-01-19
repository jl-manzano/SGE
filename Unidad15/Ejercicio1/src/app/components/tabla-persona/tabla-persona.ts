import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Persona } from '../../models/persona';

@Component({
  selector: 'app-tabla-personas',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './tabla-persona.html',
  styleUrls: ['./tabla-persona.css'],
})
export class TablaPersona {
  @Input() personas: Persona[] = [];
  @Output() personaEliminada = new EventEmitter<number>();
  
  eliminarPersona(index: number) {
    this.personaEliminada.emit(index);
  }
}