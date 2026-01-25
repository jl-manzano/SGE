import { Component, Input, Output, EventEmitter, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PersonaUIModel } from '../../Models/PersonaUIModel';

@Component({
  selector: 'app-persona-list-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './persona-list-item.html',
  styleUrls: ['./persona-list-item.css']
})
export class PersonaListItemComponent {
  @Input({ required: true }) persona!: PersonaUIModel;
  @Output() personaClick = new EventEmitter<void>();
  @Output() deleteClick = new EventEmitter<void>();

  private imageError = signal(false);

  // Verificar si debe mostrar la imagen
  shouldShowImage(): boolean {
    return !!this.persona.foto && 
           this.persona.foto.trim() !== '' && 
           !this.imageError();
  }

  handleImageError(): void {
    console.log('Error al cargar imagen:', this.persona.foto);
    this.imageError.set(true);
  }

  onPersonaClick(): void {
    this.personaClick.emit();
  }

  onDeleteClick(event: Event): void {
    event.stopPropagation();
    console.log('Delete button pressed for:', this.persona.nombre);
    this.deleteClick.emit();
  }
}