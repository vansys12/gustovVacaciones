import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { EmpleadoDetalleComponent } from './empleado-detalle/empleado-detalle.component';
import {EmpleadoComponent} from './empleado/empleado.component';

const routes: Routes = [
  { path: '', component: EmpleadoComponent },
  { path: 'detalle-empleado/:id', component: EmpleadoDetalleComponent }, // Ruta para mostrar detalles de empleados con un parámetro 'id'

];



@NgModule({


  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]

})
export class AppRoutingModule { }
