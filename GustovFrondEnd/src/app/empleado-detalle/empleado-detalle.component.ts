import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EmpleadoDetalleService } from '../Services/empleado-detalle.service';
import { EmpleadoDetalle } from '../Interfaces/empleadoDetalle';


@Component({
  selector: 'app-empleado-detalle',
  templateUrl: './empleado-detalle.component.html',
  styleUrls: ['./empleado-detalle.component.css']
})
export class EmpleadoDetalleComponent implements OnInit {
  idEmpleado!: number;
  empleadoDetalle!: EmpleadoDetalle;
  diasVacacion!: number;
  diasTomados!: number;
detalleEmpleado: any;
  constructor(
    private route: ActivatedRoute,
    private empleadoDetalleService: EmpleadoDetalleService
  ) { }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');

  if (idParam !== null) {
    this.idEmpleado = +idParam;
    this.obtenerEmpleadoDetalle();
    this.obtenerDiasVacacion();
    this.obtenerDiasTomados();
  } else {
    // Manejar el caso en el que 'id' sea null, por ejemplo, redirigir a una página de error o realizar otra acción adecuada.
  }
  }
  obtenerEmpleadoDetalle(): void {
    this.empleadoDetalleService.getEmpleadoDetalle(this.idEmpleado).subscribe((data) => {
      this.empleadoDetalle = data;
    });
  }

  obtenerDiasVacacion(): void {
    this.empleadoDetalleService.getDiasVacacion(this.idEmpleado).subscribe((data) => {
      this.diasVacacion = data;
    });
  }

  obtenerDiasTomados(): void {
    this.empleadoDetalleService.getDiasTomados(this.idEmpleado).subscribe((data) => {
      this.diasTomados = data;
    });
  }

}
