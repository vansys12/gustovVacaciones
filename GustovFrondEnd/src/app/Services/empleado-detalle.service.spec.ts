import { TestBed } from '@angular/core/testing';

import { EmpleadoDetalleService } from './empleado-detalle.service';

describe('EmpleadoDetalleService', () => {
  let service: EmpleadoDetalleService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpleadoDetalleService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
