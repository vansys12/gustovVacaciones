import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {Observable} from 'rxjs';
import { Vacacion } from '../Interfaces/vacacion';

@Injectable({
  providedIn: 'root'
})
export class VacacionService {
  private endpoind:string=environment.endPoint;
  private apiUrl:string=this.endpoind+"vacacion/";
  constructor(private http:HttpClient) { }

  getList():Observable<Vacacion>{
    return this.http.get<Vacacion>(`${this.apiUrl}lista`);
  }

  add(modelo:Vacacion):Observable<Vacacion>{
    return this.http.post<Vacacion>(`${this.apiUrl}guardar`,modelo);
  }
  update(idVacacion:number,modelo:Vacacion):Observable<Vacacion>{
    return this.http.put<Vacacion>(`${this.apiUrl}actualizar/${idVacacion}`,modelo);
  }
  delete(idVacacion:number):Observable<void>{
    return this.http.delete<void>(`${this.apiUrl}eliminar/${idVacacion}`);
  }

}
