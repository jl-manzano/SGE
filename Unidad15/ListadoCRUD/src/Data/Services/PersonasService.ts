import { Injectable, signal, computed } from '@angular/core';
import { Persona } from '../../Domain/Entities/Persona';
import { PersonaUseCases } from '../../Domain/UseCases/PersonaUseCases';
import { PersonaUIModel, toPersonaUIModel } from '../../UI/Models/PersonaUIModel';
import { DepartamentosService } from './DepartamentosService';

/**
 * PersonasService - Singleton (equivalente a getInstance() en React Native)
 * Contiene estado compartido y lógica de datos
 */
@Injectable({
  providedIn: 'root'
})
export class PersonasService {
  private _personas = signal<PersonaUIModel[]>([]);
  private _personaSeleccionada = signal<PersonaUIModel | null>(null);
  private _isLoading = signal<boolean>(false);
  private _error = signal<string | null>(null);

  readonly personas = this._personas.asReadonly();
  readonly personaSeleccionada = this._personaSeleccionada.asReadonly();
  readonly isLoading = this._isLoading.asReadonly();
  readonly error = this._error.asReadonly();

  constructor(
    private personaUseCases: PersonaUseCases,
    private departamentosService: DepartamentosService
  ) {}

  /**
   * Carga todas las personas
   */
  async loadPersonas(): Promise<void> {
    this._isLoading.set(true);
    this._error.set(null);

    try {
      const personasDTO = await this.personaUseCases.getPersonas();
      const personasUI = personasDTO.map(dto => toPersonaUIModel(dto, this.departamentosService));
      this._personas.set(personasUI);
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Error al cargar las personas';
      this._error.set(errorMessage);
      console.error('Error al cargar personas:', err);
    } finally {
      this._isLoading.set(false);
    }
  }

  /**
   * Agrega una nueva persona
   */
  async addPersona(persona: Persona): Promise<void> {
    this._isLoading.set(true);
    this._error.set(null);

    try {
      await this.personaUseCases.addPersona(persona);
      await this.loadPersonas();
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Error al agregar persona';
      this._error.set(errorMessage);
      throw err;
    } finally {
      this._isLoading.set(false);
    }
  }

  /**
   * Actualiza una persona existente
   */
  async updatePersona(persona: Persona): Promise<void> {
    this._isLoading.set(true);
    this._error.set(null);

    try {
      await this.personaUseCases.updatePersona(persona);
      await this.loadPersonas();
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Error al actualizar persona';
      this._error.set(errorMessage);
      throw err;
    } finally {
      this._isLoading.set(false);
    }
  }

  /**
   * Elimina una persona por ID
   */
  async deletePersona(id: number): Promise<void> {
    this._isLoading.set(true);
    this._error.set(null);

    try {
      await this.personaUseCases.deletePersona(id);
      await this.loadPersonas();
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Error al eliminar persona';
      this._error.set(errorMessage);
      throw err;
    } finally {
      this._isLoading.set(false);
    }
  }

  /**
   * Selecciona una persona para editar
   */
  selectPersona(persona: PersonaUIModel | null): void {
    this._personaSeleccionada.set(persona);
  }
}