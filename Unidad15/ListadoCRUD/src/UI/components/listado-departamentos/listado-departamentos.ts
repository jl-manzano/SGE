import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { DepartamentosService } from '../../../Data/Services/DepartamentosService';
import { DepartamentoListItemComponent } from '../departamento-list-item/departamento-list-item';
import { DepartamentoUIModel } from '../../Models/DepartamentoUIModel';

/**
 * ListadoDepartamentosComponent
 * ViewModel integrado en el componente + usa DepartamentosService (Singleton)
 */
@Component({
  selector: 'app-listado-departamentos',
  standalone: true,
  imports: [CommonModule, DepartamentoListItemComponent],
  templateUrl: './listado-departamentos.html',
  styleUrls: ['./listado-departamentos.css']
})
export class ListadoDepartamentosComponent implements OnInit {
  constructor(
    public departamentosService: DepartamentosService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.departamentosService.loadDepartamentos();
  }

  // === Métodos del ViewModel (lógica de presentación) ===

  navigateBack(): void {
    this.router.navigate(['/']);
  }

  handleEditDepartamento(departamento: DepartamentoUIModel): void {
    this.departamentosService.selectDepartamento(departamento);
    this.router.navigate(['/departamentos/editar']);
  }

  async handleDeleteDepartamento(departamento: DepartamentoUIModel): Promise<void> {
    console.log('handleDeleteDepartamento llamado para:', departamento.nombreDepartamento);
    
    const confirmacion = confirm(
      `¿Está seguro que desea eliminar el departamento "${departamento.nombreDepartamento}"?`
    );
    
    if (confirmacion) {
      console.log('Usuario confirmó eliminación');
      await this.performDelete(departamento.idDepartamento);
    } else {
      console.log('Eliminación cancelada');
    }
  }

  private async performDelete(id: number): Promise<void> {
    console.log('performDelete iniciado para id:', id);
    try {
      await this.departamentosService.deleteDepartamento(id);
      console.log('Departamento eliminado exitosamente');
    } catch (error) {
      console.error('Error al eliminar departamento:', error);
      const errorMessage = error instanceof Error ? error.message : 'Error desconocido';
      alert(`Error: ${errorMessage}`);
    }
  }

  handleRetry(): void {
    this.departamentosService.loadDepartamentos();
  }
}