import { Preco } from '../models/Preco';
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PrecoService {

  baseUrl = `${environment.mainUrlAPI}preco`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Preco[]> {
    return this.http.get<Preco[]>(this.baseUrl);
  }

  post(preco: Preco) {
    return this.http.post(this.baseUrl, preco);
  }

}
