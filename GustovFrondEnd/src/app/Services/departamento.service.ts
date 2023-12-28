import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {Observable} from 'rxjs';
import { Departamento } from '../Interfaces/departamento';

@Injectable({
  providedIn: 'root'
})
export class DepartamentoService {

  private endpoind:string=environment.endPoint;
  private apiUrl:string=this.endpoind+"departamento/";
  constructor(private http:HttpClient) { }

  getList():Observable<Departamento[]>{
    return this.http.get<Departamento[]>(`${this.apiUrl}lista`);
  }
}
