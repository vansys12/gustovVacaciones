import {AfterViewInit, Component, ViewChild, OnInit, Inject} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';

import { Empleado } from '../Interfaces/empleado';
import { EmpleadoService } from '../Services/empleado.service';

import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { DialogAddEditComponent } from '../Modal/dialog-add-edit/dialog-add-edit.component';
import { VacacionesAddEditComponent } from '../Modal/vacaciones/vacaciones-add-edit/vacaciones-add-edit.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-empleado',
  templateUrl: './empleado.component.html',
  styleUrls: ['./empleado.component.css']
})
export class EmpleadoComponent implements OnInit {

  displayedColumns: string[] = ['NombreCompleto', 'Departamento','Cargo', 'Sueldo','Estado', 'FechaContrato','Acciones'];
  dataSource = new MatTableDataSource<Empleado>();
  constructor(
    private _empleadoServicio: EmpleadoService,
    public dialog:MatDialog,
    private router: Router
    ){

    }

    ngOnInit(): void {
        this.mostrarEmpleados();
    }

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  mostrarEmpleados(){
    this._empleadoServicio.getList().subscribe({
      next:(dataResponse)=>{
        console.log(dataResponse)
        this.dataSource.data = dataResponse;
      },error:(e)=>{}
    })
  }

  openDialog() {
    this.dialog.open(DialogAddEditComponent,{disableClose:true,
    width:"400px"}).afterClosed().subscribe(resultado=>{
      if(resultado==="creado"){
        this.mostrarEmpleados();
      }
    });
  }
  dialogoEditarEmpleado(dataEmpleado:Empleado) {
    this.dialog.open(DialogAddEditComponent,{disableClose:true,
    width:"400px",
    data:dataEmpleado
  }).afterClosed().subscribe(resultado=>{
      if(resultado==="editado"){
        this.mostrarEmpleados();
      }
    });
  }

  openVacacion() {
    this.dialog.open(VacacionesAddEditComponent,{disableClose:true,
    width:"400px"}).afterClosed().subscribe(resultado=>{
      if(resultado==="creado"){
        this.mostrarEmpleados();
      }
    });
  }
  navegarAEmpleadoDetalle(elementId: number): void {
    this.router.navigate(['detalle-empleado', elementId]);
  }
  mostrarElementEnConsola(elementId: number) {
    console.log(elementId);
  }

}
