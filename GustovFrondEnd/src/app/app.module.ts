import { Component, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';

import {MatTableModule} from '@angular/material/table';
import {MatPaginatorModule} from '@angular/material/paginator';

import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {MatButtonModule} from '@angular/material/button';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {MomentDateModule} from '@angular/material-moment-adapter';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatIconModule} from '@angular/material/icon';
import {MatDialogModule} from '@angular/material/dialog';
import {MatGridListModule} from '@angular/material/grid-list';
import { DialogAddEditComponent } from './Modal/dialog-add-edit/dialog-add-edit.component';
import { VacacionesAddEditComponent } from './Modal/vacaciones/vacaciones-add-edit/vacaciones-add-edit.component';
import { EmpleadoDetalleComponent } from './empleado-detalle/empleado-detalle.component';
import { AppRoutingModule } from './app-routing.module';
import { RouterModule } from '@angular/router';
import { EmpleadoComponent } from './empleado/empleado.component';


@NgModule({
  declarations: [
    AppComponent,
    DialogAddEditComponent,
    VacacionesAddEditComponent,
    EmpleadoDetalleComponent,
    EmpleadoComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatButtonModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MomentDateModule,
    MatSnackBarModule,
    MatIconModule,
    MatDialogModule,
    MatGridListModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
