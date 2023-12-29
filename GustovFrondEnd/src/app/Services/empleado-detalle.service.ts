import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import{EmpleadoDetalle} from '../Interfaces/empleadoDetalle';

import { Vacacion } from '../Interfaces/vacacion';

@Injectable({
  providedIn: 'root'
})
export class EmpleadoDetalleService {

  private endpoind:string=environment.endPoint;
  private apiUrl:string=this.endpoind;
  constructor(private http: HttpClient) { }
    getEmpleadoDetalle(id: number): Observable<EmpleadoDetalle> {
      return this.http.get<EmpleadoDetalle>(`${this.apiUrl}empleado/${id}`);
    }

    getDiasVacacion(id: number): Observable<number> {
      return this.http.get<number>(`${this.apiUrl}empleado/getDiasVacacion/${id}`);
    }

    getDiasTomados(id: number): Observable<number> {
      return this.http.get<number>(`${this.apiUrl}empleado/getDiasVacacionesTomados/${id}`);
    }
    getEmpleadoVacacion(id: number): Observable<Vacacion> {
      return this.http.get<Vacacion>(`${this.apiUrl}vacacion/${id}`);
    }


}
