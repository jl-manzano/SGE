import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-formulario-material',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './formulario-material.html',
  styleUrls: ['./formulario-material.css'],
})
export class FormularioMaterial {
  formulario: FormGroup;
  mensajeSaludo = '';

  constructor(private fb: FormBuilder) {
    this.formulario = this.fb.group({
      nombre: ['', [Validators.required, Validators.minLength(4)]],
      apellidos: ['', Validators.required]
    });
  }

  saluda() {
    if (this.formulario.valid) {
      const nombre = this.formulario.get('nombre')?.value;
      this.mensajeSaludo = `Hola ${nombre}`;
    }
  }
}