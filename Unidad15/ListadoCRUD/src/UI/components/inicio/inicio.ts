import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-inicio',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './inicio.html',
  styleUrls: ['./inicio.css']
})
export class InicioComponent {
  constructor(private router: Router) {}

  navigateToPersonas(): void {
    this.router.navigate(['/personas']);
  }

  navigateToDepartamentos(): void {
    this.router.navigate(['/departamentos']);
  }
}