import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpleadoDetalleComponent } from './empleado-detalle.component';

describe('EmpleadoDetalleComponent', () => {
  let component: EmpleadoDetalleComponent;
  let fixture: ComponentFixture<EmpleadoDetalleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmpleadoDetalleComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmpleadoDetalleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
