import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TablaPersona } from './tabla-persona';

describe('TablaPersona', () => {
  let component: TablaPersona;
  let fixture: ComponentFixture<TablaPersona>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TablaPersona]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TablaPersona);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
