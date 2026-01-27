import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Persona } from '../../interfaces/persona';
import { PersonasService } from '../../services/personas.service';

@Component({
  selector: 'app-tabla-api',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './tabla-api.component.html',
  styleUrl: './tabla-api.component.css'
})
export class TablaApiComponent implements OnInit {
  listadoPersonas: Persona[] = [];
  cargando: boolean = true;
  error: string = '';

  constructor(private personasServicio: PersonasService) { }

  ngOnInit(): void {
    this.obtenerPersonas();
  }

  obtenerPersonas(): void {
    this.cargando = true;
    this.error = '';
    
    this.personasServicio.getPersonas().subscribe({
      next: (response) => {
        console.log('Datos recibidos:', response);
        this.listadoPersonas = response;
        this.cargando = false;
        console.log('Cargando:', this.cargando);
      },
      error: (error) => {
        console.error('Error completo:', error);
        this.error = "Ha ocurrido un error al obtener los datos del servidor";
        this.cargando = false;
      },
      complete: () => {
        console.log('Petición completada');
        this.cargando = false;
      }
    });
  }

  calcularEdad(fechaNac: string): number {
    const hoy = new Date();
    const nacimiento = new Date(fechaNac);
    let edad = hoy.getFullYear() - nacimiento.getFullYear();
    const mes = hoy.getMonth() - nacimiento.getMonth();
    
    if (mes < 0 || (mes === 0 && hoy.getDate() < nacimiento.getDate())) {
      edad--;
    }
    
    return edad;
  }
}