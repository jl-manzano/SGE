import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { PersonasService } from '../../../Data/Services/PersonasService';
import { PersonaListItemComponent } from '../persona-list-item/persona-list-item';
import { PersonaUIModel } from '../../Models/PersonaUIModel';

/**
 * ListadoPersonasComponent
 * ViewModel integrado en el componente + usa PersonasService (Singleton)
 */
@Component({
  selector: 'app-listado-personas',
  standalone: true,
  imports: [CommonModule, PersonaListItemComponent],
  templateUrl: './listado-personas.html',
  styleUrls: ['./listado-personas.css']
})
export class ListadoPersonasComponent implements OnInit {
  // ViewModel integrado - accede al Service Singleton
  constructor(
    public personasService: PersonasService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.personasService.loadPersonas();
  }

  // === Métodos del ViewModel (lógica de presentación) ===

  navigateBack(): void {
    this.router.navigate(['/']);
  }

  handleAddPersona(): void {
    this.personasService.selectPersona(null);
    this.router.navigate(['/personas/editar']);
  }

  handleEditPersona(persona: PersonaUIModel): void {
    this.personasService.selectPersona(persona);
    this.router.navigate(['/personas/editar']);
  }

  async handleDeletePersona(persona: PersonaUIModel): Promise<void> {
    console.log('handleDeletePersona llamado para:', persona.nombre);
    
    const confirmacion = confirm(
      `¿Está seguro que desea eliminar a ${persona.nombre} ${persona.apellidos}?`
    );
    
    if (confirmacion) {
      console.log('Usuario confirmó eliminación');
      await this.performDelete(persona.id);
    } else {
      console.log('Eliminación cancelada');
    }
  }

  private async performDelete(id: number): Promise<void> {
    console.log('performDelete iniciado para id:', id);
    try {
      await this.personasService.deletePersona(id);
      console.log('Persona eliminada exitosamente');
    } catch (error) {
      console.error('Error al eliminar persona:', error);
      const errorMessage = error instanceof Error ? error.message : 'Error desconocido';
      alert(`Error: ${errorMessage}`);
    }
  }

  handleRetry(): void {
    this.personasService.loadPersonas();
  }
}