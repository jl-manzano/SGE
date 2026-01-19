import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatRadioModule } from '@angular/material/radio';
import { MatSliderModule } from '@angular/material/slider';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-material-components',
  standalone: true,
  imports: [
    CommonModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatSliderModule,
    MatCardModule,
    MatButtonModule,
    FormsModule
  ],
  templateUrl: './material-components.html',
  styleUrls: ['./material-components.css'],
})
export class MaterialComponents {
  favoriteColor = 'azul';
  sliderValue = 50;
  showSpinner = false;

  toggleSpinner() {
    this.showSpinner = !this.showSpinner;
  }
}