import { Component, Input, Output, EventEmitter } from '@angular/core';
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
  @Output() personaEliminada = new EventEmitter<number>();
  
  eliminarPersona(index: number) {
    this.personaEliminada.emit(index);
  }
}