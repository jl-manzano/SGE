import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-formulario-reactivo',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './formulario-reactivo.html',
  styleUrls: ['./formulario-reactivo.css'],
})
export class FormularioReactivo {
  formulario: FormGroup;

  constructor(private fb: FormBuilder) {
    this.formulario = this.fb.group({
      nombre: ['', [Validators.required, Validators.minLength(4)]],
      apellidos: ['', Validators.required]
    });
  }

  saluda() {
    if (this.formulario.valid) {
      const nombre = this.formulario.get('nombre')?.value;
      alert(`¡Hola ${nombre}! 👋`);
      this.formulario.reset();
    }
  }
}