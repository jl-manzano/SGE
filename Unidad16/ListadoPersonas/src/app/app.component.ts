import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TablaApiComponent } from './components/tabla-api/tabla-api.component';  // ← Cambiar esta línea

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, TablaApiComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ListadoPersonas';
}