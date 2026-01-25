import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { PersonasService } from '../../../Data/Services/PersonasService';
import { DepartamentosService } from '../../../Data/Services/DepartamentosService';
import { Persona } from '../../../Domain/Entities/Persona';

/**
 * EditarPersonaComponent
 * ViewModel integrado con signals locales + usa Services Singleton
 */
@Component({
  selector: 'app-editar-persona',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './editar-persona.html',
  styleUrls: ['./editar-persona.css']
})
export class EditarPersonaComponent implements OnInit {
  // === ViewModel integrado - Signals locales del formulario ===
  nombre = signal('');
  apellidos = signal('');
  telefono = signal('');
  direccion = signal('');
  idDepartamento = signal(0);
  foto = signal('');
  isSaving = signal(false);
  imageError = signal(false);

  // Computed
  get isEditing(): boolean {
    return this.personasService.personaSeleccionada() !== null;
  }

  get initials(): string {
    const n = this.nombre();
    const a = this.apellidos();
    return n && a ? `${n.charAt(0)}${a.charAt(0)}`.toUpperCase() : '??';
  }

  constructor(
    public personasService: PersonasService,
    public departamentosService: DepartamentosService,
    private router: Router
  ) {}

  async ngOnInit(): Promise<void> {
    // Cargar departamentos si no están cargados
    if (this.departamentosService.departamentos().length === 0) {
      await this.departamentosService.loadDepartamentos();
    }
    
    // Inicializar formulario si hay persona seleccionada
    console.log('EditarPersonaComponent - isEditing:', this.isEditing);
    console.log('EditarPersonaComponent - personaSeleccionada:', this.personasService.personaSeleccionada());
    
    const persona = this.personasService.personaSeleccionada();
    if (this.isEditing && persona) {
      this.nombre.set(persona.nombre);
      this.apellidos.set(persona.apellidos);
      this.telefono.set(persona.telefono);
      this.direccion.set(persona.direccion);
      this.idDepartamento.set(persona.idDepartamento);
      this.foto.set(persona.foto || '');
    }
  }

  // === Métodos del ViewModel ===

  handleImageError(): void {
    console.log('Error al cargar imagen preview:', this.foto());
    this.imageError.set(true);
  }

  onFotoChange(newFoto: string): void {
    this.foto.set(newFoto);
    this.imageError.set(false);
  }

  async handleGuardar(): Promise<void> {
    console.log('handleGuardar iniciado');
    
    // Validación
    if (!this.nombre() || !this.apellidos() || !this.telefono()) {
      alert('Por favor complete los campos obligatorios');
      return;
    }

    if (this.idDepartamento() === 0) {
      alert('Por favor seleccione un departamento');
      return;
    }

    this.isSaving.set(true);
    console.log('Creando objeto Persona...');

    const personaSeleccionada = this.personasService.personaSeleccionada();
    const persona = new Persona(
      this.isEditing ? personaSeleccionada!.id : 0,
      this.nombre(),
      this.apellidos(),
      this.isEditing ? personaSeleccionada!.fechaNac : new Date(),
      this.direccion(),
      this.telefono(),
      this.foto(),
      this.idDepartamento()
    );

    console.log('Persona creada:', persona);

    try {
      if (this.isEditing) {
        console.log('Actualizando persona...');
        await this.personasService.updatePersona(persona);
        console.log('Persona actualizada correctamente, navegando...');
      } else {
        console.log('Agregando persona...');
        await this.personasService.addPersona(persona);
        console.log('Persona agregada correctamente, navegando...');
      }
      
      // Limpiar selección y navegar
      this.personasService.selectPersona(null);
      this.router.navigate(['/personas']);
    } catch (error) {
      console.error('Error al guardar persona:', error);
      const errorMessage = error instanceof Error ? error.message : 'Error desconocido';
      alert(`No se pudo ${this.isEditing ? 'actualizar' : 'agregar'} la persona: ${errorMessage}`);
    } finally {
      this.isSaving.set(false);
    }
  }

  selectDepartamento(depId: number): void {
    if (!this.isSaving()) {
      this.idDepartamento.set(depId);
    }
  }

  navigateBack(): void {
    // Limpiar selección al salir
    this.personasService.selectPersona(null);
    this.router.navigate(['/personas']);
  }
}