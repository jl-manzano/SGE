import { PersonaDTO } from '../../Domain/DTOs/PersonaDTO';
import { DepartamentosService } from '../../Data/Services/DepartamentosService';
import { inject } from '@angular/core';

export interface PersonaUIModel extends PersonaDTO {
  color?: string;
  initials?: string;
}

export const toPersonaUIModel = (dto: PersonaDTO, departamentosService?: DepartamentosService): PersonaUIModel => {
  const colors = ['#6C5CE7', '#00B894', '#FDCB6E', '#E17055', '#74B9FF', '#A29BFE', '#FF7675', '#FD79A8', '#55EFC4', '#81ECEC'];
  
  const colorIndex = dto.idDepartamento % colors.length;
  const assignedColor = colors[colorIndex];
  
  const initials = `${dto.nombre.charAt(0)}${dto.apellidos.charAt(0)}`.toUpperCase();
  
  let nombreDepartamento = dto.nombreDepartamento;
  if ((!nombreDepartamento || nombreDepartamento.trim() === '') && departamentosService) {
    const departamento = departamentosService.departamentos().find(d => d.idDepartamento === dto.idDepartamento);
    nombreDepartamento = departamento ? departamento.nombreDepartamento : 'Sin departamento';
  }
  
  return {
    ...dto,
    nombreDepartamento: nombreDepartamento || 'Sin departamento',
    color: assignedColor,
    initials,
  };
};