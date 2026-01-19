import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormularioMaterial } from './formulario-material';

describe('FormularioMaterial', () => {
  let component: FormularioMaterial;
  let fixture: ComponentFixture<FormularioMaterial>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormularioMaterial]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormularioMaterial);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
