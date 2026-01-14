import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { TablaPersona } from './components/tabla-persona/tabla-persona';
import { ListaPersonas } from './components/lista-personas/lista-personas';
import { FormularioPersona } from './components/formulario-persona/formulario-persona';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, TablaPersona, ListaPersonas, FormularioPersona],
  templateUrl: './app.html',
  styleUrls: ['./app.css'],
})
export class App {
  personas = [
    { nombre: 'José', apellidos: 'Manzano Borrego' },
    { nombre: 'Román', apellidos: 'Saborido Cobano' },
    { nombre: 'Ángel', apellidos: 'García Guillena' },
  ];

  addPersona(p: { nombre: string; apellidos: string }) {
    this.personas.push(p);
  }
}
