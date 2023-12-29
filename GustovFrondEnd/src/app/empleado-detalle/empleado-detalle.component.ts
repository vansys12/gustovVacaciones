import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EmpleadoDetalleService } from '../Services/empleado-detalle.service';
import { EmpleadoDetalle } from '../Interfaces/empleadoDetalle';
import {Vacacion} from '../Interfaces/vacacion';
import {VacacionService} from '../Services/vacacion.service';
import { MatTableDataSource } from '@angular/material/table';
import { Location } from '@angular/common';
import { VacacionesAddEditComponent } from '../Modal/vacaciones/vacaciones-add-edit/vacaciones-add-edit.component';
import { MatDialog } from '@angular/material/dialog';
@Component({
  selector: 'app-empleado-detalle',
  templateUrl: './empleado-detalle.component.html',
  styleUrls: ['./empleado-detalle.component.css']
})
export class EmpleadoDetalleComponent implements OnInit {
  displayedColumns: string[] = ['detalle','fechaInicio','fechaFin', 'gestion','acciones'];
  dataSource = new MatTableDataSource<Vacacion>();
  idEmpleado!: number;
  empleadoDetalle!: EmpleadoDetalle;
  vacacion!:any[];
  diasVacacion!: number;
  diasTomados!: number;
detalleEmpleado: any;
vacaciones!:any[];
  constructor(
    private route: ActivatedRoute,
    private empleadoDetalleService: EmpleadoDetalleService,
    private vacacionService:VacacionService,
    private location: Location,
    public dialog:MatDialog,
  ) { }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');

  if (idParam !== null) {
    this.idEmpleado = +idParam;
    this.obtenerEmpleadoDetalle();
    //this.obtenerDiasVacacion();
    this.obtenerDiasTomados();
    this.vacacionEmpleado();

  } else {
    // Manejar el caso en el que 'id' sea null
  }
  }
  vacacionEmpleado(){
    this.empleadoDetalleService.getEmpleadoVacacion(this.idEmpleado).subscribe({
      next:(dataResponse)=>{
        console.log(dataResponse)
        this.dataSource.data = [dataResponse];
        this.dataSource.data = this.empleadoDetalle.vacaciones;
      },error:(e)=>{}
    })
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

  agregarVacacion(){
    this.dialog.open(VacacionesAddEditComponent,{disableClose:true,
      width:"400px"}).afterClosed().subscribe(resultado=>{
        if(resultado==="creado"){
          this.vacacionEmpleado();
        }
      });
  }
  editarVacacion(){

  }
  eliminarVacacion(){

  }
  goBack(): void {
    this.location.back();
  }

}
