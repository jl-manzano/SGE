import { ApplicationConfig, provideZonelessChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { container } from '../Core/container';
import { TYPES } from '../Core/types';
import { PersonaUseCases } from '../Domain/UseCases/PersonaUseCases';
import { DepartamentoUseCases } from '../Domain/UseCases/DepartamentoUseCases';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZonelessChangeDetection(),
    
    provideRouter(routes),
    
    {
      provide: PersonaUseCases,
      useFactory: () => container.get<PersonaUseCases>(TYPES.PersonaUseCases)
    },
    {
      provide: DepartamentoUseCases,
      useFactory: () => container.get<DepartamentoUseCases>(TYPES.DepartamentoUseCases)
    }
  ]
};