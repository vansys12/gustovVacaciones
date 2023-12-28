import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {Observable} from 'rxjs';
import { Cargo } from '../Interfaces/cargo';

@Injectable({
  providedIn: 'root'
})
export class CargoService {
  private endpoind:string=environment.endPoint;
  private apiUrl:string=this.endpoind+"cargo/";
  constructor(private http:HttpClient) { }

  getList():Observable<Cargo[]>{
    return this.http.get<Cargo[]>(`${this.apiUrl}lista`);
  }
}
