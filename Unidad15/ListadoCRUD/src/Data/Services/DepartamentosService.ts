import { Injectable, signal, computed } from '@angular/core';
import { Departamento } from '../../Domain/Entities/Departamento';
import { DepartamentoUseCases } from '../../Domain/UseCases/DepartamentoUseCases';
import { DepartamentoUIModel, toDepartamentoUIModel } from '../../UI/Models/DepartamentoUIModel';

/**
 * DepartamentosService - Singleton (equivalente a getInstance() en React Native)
 * Contiene estado compartido y lógica de datos
 */
@Injectable({
  providedIn: 'root' // ← Singleton
})
export class DepartamentosService {
  // Estado compartido (Signals)
  private _departamentos = signal<DepartamentoUIModel[]>([]);
  private _departamentoSeleccionado = signal<DepartamentoUIModel | null>(null);
  private _isLoading = signal<boolean>(false);
  private _error = signal<string | null>(null);

  // Públicos readonly
  readonly departamentos = this._departamentos.asReadonly();
  readonly departamentoSeleccionado = this._departamentoSeleccionado.asReadonly();
  readonly isLoading = this._isLoading.asReadonly();
  readonly error = this._error.asReadonly();

  constructor(private departamentoUseCases: DepartamentoUseCases) {}

  /**
   * Carga todos los departamentos
   */
  async loadDepartamentos(): Promise<void> {
    this._isLoading.set(true);
    this._error.set(null);

    try {
      const departamentos = await this.departamentoUseCases.getDepartamentos();
      const departamentosUI = departamentos.map(dep => toDepartamentoUIModel(dep));
      this._departamentos.set(departamentosUI);
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Error al cargar los departamentos';
      this._error.set(errorMessage);
      console.error('Error al cargar departamentos:', err);
    } finally {
      this._isLoading.set(false);
    }
  }

  /**
   * Agrega un nuevo departamento
   */
  async addDepartamento(departamento: Departamento): Promise<void> {
    this._isLoading.set(true);
    this._error.set(null);

    try {
      await this.departamentoUseCases.addDepartamento(departamento);
      await this.loadDepartamentos();
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Error al agregar departamento';
      this._error.set(errorMessage);
      throw err;
    } finally {
      this._isLoading.set(false);
    }
  }

  /**
   * Actualiza un departamento existente
   */
  async updateDepartamento(departamento: Departamento): Promise<void> {
    this._isLoading.set(true);
    this._error.set(null);

    try {
      await this.departamentoUseCases.updateDepartamento(departamento);
      await this.loadDepartamentos();
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Error al actualizar departamento';
      this._error.set(errorMessage);
      throw err;
    } finally {
      this._isLoading.set(false);
    }
  }

  /**
   * Elimina un departamento por ID
   */
  async deleteDepartamento(id: number): Promise<void> {
    this._isLoading.set(true);
    this._error.set(null);

    try {
      await this.departamentoUseCases.deleteDepartamento(id);
      await this.loadDepartamentos();
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Error al eliminar departamento';
      this._error.set(errorMessage);
      throw err;
    } finally {
      this._isLoading.set(false);
    }
  }

  /**
   * Selecciona un departamento para editar
   */
  selectDepartamento(departamento: DepartamentoUIModel | null): void {
    this._departamentoSeleccionado.set(departamento);
  }
}