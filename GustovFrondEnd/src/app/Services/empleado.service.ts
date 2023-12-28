import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {Observable} from 'rxjs';
import { Empleado } from '../Interfaces/empleado';

@Injectable({
  providedIn: 'root'
})
export class EmpleadoService {
  private endpoind:string=environment.endPoint;
  private apiUrl:string=this.endpoind+"empleado/";
  constructor(private http:HttpClient) { }

  get(idEmpleado:number):Observable<Empleado>{
    return this.http.get<Empleado>(`${this.apiUrl}/empleado/${idEmpleado}`);
  }

  getList():Observable<Empleado[]>{
    return this.http.get<Empleado[]>(`${this.apiUrl}lista`);
  }
  add(modelo:Empleado):Observable<Empleado>{
    return this.http.post<Empleado>(`${this.apiUrl}guardar`,modelo);
  }
  update(idEmpleado:number,modelo:Empleado):Observable<Empleado>{
    return this.http.put<Empleado>(`${this.apiUrl}actualizar/${idEmpleado}`,modelo);
  }
  delete(idEmpleado:number):Observable<void>{
    return this.http.delete<void>(`${this.apiUrl}eliminar/${idEmpleado}`);
  }
}
