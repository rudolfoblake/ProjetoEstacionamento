import { Estacionamento } from '../models/Estacionamento';
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EstacionamentoService {

  baseUrl = `${environment.mainUrlAPI}estacionamento`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Estacionamento[]> {
    return this.http.get<Estacionamento[]>(this.baseUrl);
  }

  put(estacionamento: Estacionamento) {
    return this.http.put(`${this.baseUrl}/${estacionamento.id}`, estacionamento);
  }

  post(estacionamento: Estacionamento) {
    return this.http.post(this.baseUrl, estacionamento);
  }

  delete(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

}
