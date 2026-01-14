import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { TablaPersona } from './components/tabla-persona/tabla-persona';
import { ListaPersonas } from './components/lista-personas/lista-personas';
import { FormularioPersona } from './components/formulario-persona/formulario-persona';
import { Persona } from './models/persona';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, TablaPersona, ListaPersonas, FormularioPersona],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class App {
  personas: Persona[] = [
    { nombre: 'José', apellidos: 'Manzano Borrego' },
    { nombre: 'Román', apellidos: 'Saborido Cobano' },
    { nombre: 'Ángel', apellidos: 'García Guillena' },
  ];

  addPersona(p: Persona) {
    this.personas.push(p);
  }

  eliminarPersona(index: number) {
    this.personas.splice(index, 1); // Elimina la persona en la posición 'index'
  }
}
