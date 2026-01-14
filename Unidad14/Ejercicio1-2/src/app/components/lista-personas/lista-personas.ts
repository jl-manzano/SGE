import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Persona } from '../../models/persona';

@Component({
  selector: 'app-lista-personas',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './lista-personas.html',
  styleUrls: ['./lista-personas.css'],
})
export class ListaPersonas {
  @Input() personas: Persona[] = [];
  
  eliminarPersona(index: number) {
    this.personas.splice(index, 1);  // Elimina la persona en la posición 'index'
  }
}
