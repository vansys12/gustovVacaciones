import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { EmpleadoDetalleComponent } from './empleado-detalle/empleado-detalle.component';

const routes: Routes = [
  { path: '', component: AppComponent },
  { path: 'detalle-empleado/:id', component: EmpleadoDetalleComponent }, // Ruta para mostrar detalles de empleados con un parámetro 'id'

];



@NgModule({

  exports: [RouterModule],
  imports: [RouterModule.forRoot(routes)]

})
export class AppRoutingModule { }
