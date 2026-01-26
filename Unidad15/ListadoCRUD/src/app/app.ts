import { Component, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { PersonasService } from '../Data/Services/PersonasService';
import { DepartamentosService } from '../Data/Services/DepartamentosService';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class AppComponent implements OnInit {
  title = 'RRHH';
  
  isLoading = signal(true);
  error = signal<string | null>(null);

  constructor(
    private personasService: PersonasService,
    private departamentosService: DepartamentosService
  ) {}

  async ngOnInit(): Promise<void> {
    await this.initializeApp();
  }

  private async initializeApp(): Promise<void> {
    try {
      console.log('Cargando departamentos...');
      await this.departamentosService.loadDepartamentos();
      console.log('Departamentos cargados:', this.departamentosService.departamentos());

      console.log('Cargando personas...');
      await this.personasService.loadPersonas();
      console.log('Personas cargadas:', this.personasService.personas());

      console.log('Aplicación inicializada correctamente');
      this.isLoading.set(false);
    } catch (err) {
      console.error('Error al inicializar la aplicación:', err);
      this.error.set(err instanceof Error ? err.message : 'Error desconocido');
      this.isLoading.set(false);
    }
  }
}