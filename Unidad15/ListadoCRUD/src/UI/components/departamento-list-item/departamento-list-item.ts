import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DepartamentoUIModel } from '../../Models/DepartamentoUIModel';

@Component({
  selector: 'app-departamento-list-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './departamento-list-item.html',
  styleUrls: ['./departamento-list-item.css']
})
export class DepartamentoListItemComponent {
  @Input({ required: true }) departamento!: DepartamentoUIModel;
  @Output() departamentoClick = new EventEmitter<void>();
  @Output() deleteClick = new EventEmitter<void>();

  onDepartamentoClick(): void {
    this.departamentoClick.emit();
  }

  onDeleteClick(event: Event): void {
    event.stopPropagation();
    console.log('Delete button pressed for:', this.departamento.nombreDepartamento);
    this.deleteClick.emit();
  }
}