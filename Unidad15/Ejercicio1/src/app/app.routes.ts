import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TablaPersona } from './components/tabla-persona/tabla-persona';
import { FormularioPersona } from './components/formulario-persona/formulario-persona';
import { ListaPersonas } from './components/lista-personas/lista-personas';

const routes: Routes = [
  { path: '', component: TablaPersona },  // Ruta por defecto
  { path: 'formulario', component: FormularioPersona }, // Ruta para el formulario
  { path: 'listado', component: ListaPersonas },  // Ruta para el listado
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
