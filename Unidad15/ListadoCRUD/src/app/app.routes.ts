import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/inicio',
    pathMatch: 'full'
  },
  {
    path: 'inicio',
    loadComponent: () => import('../UI/components/inicio/inicio').then(m => m.InicioComponent)
  },
  {
    path: 'personas',
    loadComponent: () => import('../UI/components/listado-personas/listado-personas').then(m => m.ListadoPersonasComponent)
  },
  {
    path: 'personas/editar',
    loadComponent: () => import('../UI/components/editar-persona/editar-persona').then(m => m.EditarPersonaComponent)
  },
  {
    path: 'departamentos',
    loadComponent: () => import('../UI/components/listado-departamentos/listado-departamentos').then(m => m.ListadoDepartamentosComponent)
  },
  {
    path: '**',
    redirectTo: '/inicio'
  }
];