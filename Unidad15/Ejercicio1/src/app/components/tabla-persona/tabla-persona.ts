import { Component, Input } from '@angular/core';
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
  
  eliminarPersona(index: number) {
    this.personas.splice(index, 1);  // Elimina la persona en la posición 'index'
  }
}
