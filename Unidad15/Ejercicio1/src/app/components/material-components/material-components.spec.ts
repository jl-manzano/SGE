import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MaterialComponents } from './material-components';

describe('MaterialComponents', () => {
  let component: MaterialComponents;
  let fixture: ComponentFixture<MaterialComponents>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaterialComponents]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MaterialComponents);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
