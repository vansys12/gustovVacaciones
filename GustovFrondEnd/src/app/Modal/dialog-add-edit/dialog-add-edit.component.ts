import { Component, OnInit,Inject } from '@angular/core';
import { FormBuilder,FormGroup,Validator, Validators } from '@angular/forms';
import {MatDialogRef,MAT_DIALOG_DATA} from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import * as moment from 'moment';

import { Departamento } from 'src/app/Interfaces/departamento';
import { Cargo} from 'src/app/Interfaces/cargo';
import { Empleado } from 'src/app/Interfaces/empleado';
import { DepartamentoService } from 'src/app/Services/departamento.service';
import { CargoService } from 'src/app/Services/cargo.service';
import { EmpleadoService } from 'src/app/Services/empleado.service';

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
  selector: 'app-dialog-add-edit',
  templateUrl: './dialog-add-edit.component.html',
  styleUrls: ['./dialog-add-edit.component.css'],
  providers:[
    {provide:MAT_DATE_FORMATS, useValue:MY_DATE_FORMATS}
  ]
})
export class DialogAddEditComponent implements OnInit {

  formEmpleado:FormGroup;
  tituloAccion:string="Nuevo";
  botonAccion:string="Guardar";
  listaDepartamentos:Departamento[]=[];
  listaCargos:Cargo[]=[];
  constructor(
  private dialogoReferencia:MatDialogRef<DialogAddEditComponent>,
  private fb:FormBuilder,
  private _snackBar:MatSnackBar,
  private _departamentoServicio:DepartamentoService,
  private _cargoServicio:CargoService,
  private _empleadoServicio:EmpleadoService,
  @Inject(MAT_DIALOG_DATA) public dataEmpleado:Empleado,

  ) {
    this.formEmpleado=this.fb.group({
      nombreCompleto:['',Validators.required],
      idDepartamento:['',Validators.required],
      idCargo:['',Validators.required],
      sueldo:['',Validators.required],
      estado:['',Validators.required],
      fechaContrato:['',Validators.required],
    })
    this._departamentoServicio.getList().subscribe({
      next:(data)=>{
      this.listaDepartamentos=data;
      },error:(e)=>{}

    })
    this._cargoServicio.getList().subscribe({
      next:(data)=>{
      this.listaCargos=data;
      },error:(e)=>{}

    })

  }
  mostrarAlerta(msg:string,accion:string){
    this._snackBar.open(msg,accion,{
      horizontalPosition:"end",
      verticalPosition:"top",
      duration:3000
    });
  }

  addEditEmpleado(){

    console.log(this.formEmpleado.value)
    const modelo:Empleado={
      idEmpleado:1,
      nombreCompleto:this.formEmpleado.value.nombreCompleto,
      idDepartamento:this.formEmpleado.value.idDepartamento,
      idCargo:this.formEmpleado.value.idCargo,
      sueldo:this.formEmpleado.value.sueldo,
      fechaContrato:moment(this.formEmpleado.value.fechaContrato).format("dd/MM/yyyy")
    }
    this._empleadoServicio.add(modelo).subscribe({
      next:(data)=>{
        this.mostrarAlerta("Empleado fue creado","Listo");
        this.dialogoReferencia.close("creado");
      },error:(e)=>{
        this.mostrarAlerta("No se pudo crear","Error");
      }
    })
  }

  ngOnInit(): void {
    if(this.dataEmpleado){
      this.formEmpleado.patchValue({
        nombreCompleto:this.dataEmpleado.nombreCompleto,
        idDepartamento:this.dataEmpleado.idDepartamento,
        idCargo: this.dataEmpleado.idCargo,
        sueldo:this.dataEmpleado.sueldo,
        fechaContrato:moment(this.dataEmpleado.fechaContrato,'DD/MM/YYYY')
      })
    }
  }

}
