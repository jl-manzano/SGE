import { Departamento } from '../../Domain/Entities/Departamento';

export interface DepartamentoUIModel {
  idDepartamento: number;
  nombreDepartamento: string;
  icon: string;
  color: string;
}

const colors = [
  '#6C5CE7', '#00B894', '#FDCB6E', '#E17055', '#74B9FF', 
  '#A29BFE', '#FF7675', '#FD79A8', '#55EFC4', '#81ECEC'
];

export function toDepartamentoUIModel(departamento: Departamento): DepartamentoUIModel {
  const colorIndex = departamento.idDepartamento % colors.length;
  const assignedColor = colors[colorIndex];
  
  return {
    idDepartamento: departamento.idDepartamento,
    nombreDepartamento: departamento.nombreDepartamento,
    icon: '🏢',
    color: assignedColor
  };
}