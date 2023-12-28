import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import{EmpleadoDetalle} from '../Interfaces/empleadoDetalle';

@Injectable({
  providedIn: 'root'
})
export class EmpleadoDetalleService {
  private endpoind:string=environment.endPoint;
  private apiUrl:string=this.endpoind+"empleado/";
  constructor(private http: HttpClient) { }
    getEmpleadoDetalle(id: number): Observable<EmpleadoDetalle> {
      return this.http.get<EmpleadoDetalle>(`${this.apiUrl}/${id}`);
    }

    getDiasVacacion(id: number): Observable<number> {
      return this.http.get<number>(`${this.apiUrl}/getDiasVacacion/${id}`);
    }

    getDiasTomados(id: number): Observable<number> {
      return this.http.get<number>(`${this.apiUrl}/getDiasVacacionTomados/${id}`);
    }


}
