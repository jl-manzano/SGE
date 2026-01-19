import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TablaPersona } from './components/tabla-persona/tabla-persona';
import { ListaPersonas } from './components/lista-personas/lista-personas';
import { FormularioPersona } from './components/formulario-persona/formulario-persona';
import { FormularioReactivo } from './components/formulario-reactivo/formulario-reactivo';
import { MaterialComponents } from './components/material-components/material-components';
import { FormularioMaterial } from './components/formulario-material/formulario-material';
import { Persona } from './models/persona';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    TablaPersona,
    ListaPersonas,
    FormularioPersona,
    FormularioReactivo,
    MaterialComponents,
    FormularioMaterial
  ],
  templateUrl: './app.html',
  styleUrls: ['./app.css'],
})
export class AppComponent {
  // Datos de las personas
  personas: Persona[] = [
    { nombre: 'José', apellidos: 'Manzano Borrego' },
    { nombre: 'Román', apellidos: 'Saborido Cobano' },
    { nombre: 'Ángel', apellidos: 'García Guillena' },
  ];

  // Control de qué vista mostrar
  vistaActual: 'tabla' | 'formulario' | 'lista' | 'reactivo' | 'material' | 'material-form' = 'tabla';

  // Mostrar vista
  mostrarTabla() {
    this.vistaActual = 'tabla';
  }

  mostrarFormulario() {
    this.vistaActual = 'formulario';
  }

  mostrarLista() {
    this.vistaActual = 'lista';
  }

  mostrarFormularioReactivo() {
    this.vistaActual = 'reactivo';
  }

  mostrarMaterialComponents() {
    this.vistaActual = 'material';
  }

  mostrarFormularioMaterial() {
    this.vistaActual = 'material-form';
  }

  // Añadir persona
  addPersona(p: Persona) {
    this.personas.push(p);
    this.vistaActual = 'tabla'; // Volver a la tabla después de añadir
  }

  // Eliminar persona
  eliminarPersona(index: number) {
    this.personas.splice(index, 1);
  }
}