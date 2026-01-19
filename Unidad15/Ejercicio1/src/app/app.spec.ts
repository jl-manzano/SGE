import { TestBed } from '@angular/core/testing';
import { AppComponent } from './app';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';

describe('AppComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AppComponent],
      providers: [provideRouter(routes)]
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it('should have personas array', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.personas.length).toBeGreaterThan(0);
  });

  it('should add a persona', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    const initialLength = app.personas.length;
    
    app.addPersona({ nombre: 'Test', apellidos: 'Usuario' });
    
    expect(app.personas.length).toBe(initialLength + 1);
  });

  it('should delete a persona', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    const initialLength = app.personas.length;
    
    app.eliminarPersona(0);
    
    expect(app.personas.length).toBe(initialLength - 1);
  });
});