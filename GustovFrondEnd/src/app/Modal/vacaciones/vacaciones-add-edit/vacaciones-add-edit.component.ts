import { Component, OnInit } from '@angular/core';
import { FormBuilder,FormGroup,Validator, Validators } from '@angular/forms';
import {MatDialogRef} from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import * as moment from 'moment';
import { Empleado } from 'src/app/Interfaces/empleado';
import { Vacacion } from 'src/app/Interfaces/vacacion';
import { EmpleadoService } from 'src/app/Services/empleado.service';
import { VacacionService } from 'src/app/Services/vacacion.service';

export const MY_DATE_FORMATS={
  parse:{
    dateInput:'DD/MM/YYYY',
  },
  display:{
    dateInput:'DD/MM/YYYY',
    monthYearLabel:'MMMM YYYY',
    dateA11yLabel:'LL',
    monthYearA11yLabel:'MMMM YYYY'
  }
}
@Component({
  selector: 'app-vacaciones-add-edit',
  templateUrl: './vacaciones-add-edit.component.html',
  styleUrls: ['./vacaciones-add-edit.component.css'],
  providers:[
    {provide:MAT_DATE_FORMATS, useValue:MY_DATE_FORMATS}
  ]
})
export class VacacionesAddEditComponent implements OnInit {
  formVacacion:FormGroup;
  tituloAccion:string="Registrar";
  botonAccion:string="Guardar";
  Empleado:Empleado[]=[];
  constructor(
    private dialogoReferencia:MatDialogRef<VacacionesAddEditComponent>,
    private fb:FormBuilder,
    private _snackBar:MatSnackBar,
    private _empleadoServicio:EmpleadoService,
    private _vacacionServicio:VacacionService
  ) {
    this.formVacacion=this.fb.group({
      detalle:['', Validators.required],
      idEmpleado:['', Validators.required],
      fechaInicio:['', Validators.required],
      fechaFin:['', Validators.required],
      estado:['', Validators.required],
      gestion:['', Validators.required]
    })
  }
  mostrarAlerta(msg:string,accion:string){
    this._snackBar.open(msg,accion,{
      horizontalPosition:"end",
      verticalPosition:"top",
      duration:3000
    });
  }
  addEditVacacion(){
    const modelo:Vacacion={
      idVacacion: 0,
      detalle: this.formVacacion.value.detalle,
      idEmpleado: this.formVacacion.value.idEmpleado,
      fechaInicio: moment(this.formVacacion.value.fechaInicio).format("dd/MM/yyyy"),
      fechaFin: moment(this.formVacacion.value.fechaFin).format("dd/MM/yyyy"),
      estado: 'ACTIVO',
      gestion: '2023',
      NombreEmpleado: '0'
    }
    this._vacacionServicio.add(modelo).subscribe({
      next:(data)=>{
        this.mostrarAlerta("Vacacion fue creado","Listo");
        this.dialogoReferencia.close("creado");
      },error:(e)=>{
        this.mostrarAlerta("Nose pudo crear","Error");
      }
    })
  }

  ngOnInit(): void {
  }

}
