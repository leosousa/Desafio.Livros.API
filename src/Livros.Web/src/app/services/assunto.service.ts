import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AssuntoListagem } from '../models/assunto.model';

@Injectable({
  providedIn: 'root'
})
export class AssuntoService {

  //private apiUrl = '/api/assuntos?numeroPagina=1&tamanhoPagina=10';
  private readonly baseUrl = '/api'; // Use apenas o caminho relativo


  constructor(private http: HttpClient) { }

  //getAssuntos(): Observable<AssuntoListagem> {
  //  return this.http.get<AssuntoListagem>(`${this.baseUrl}/assuntos?numeroPagina=1&tamanhoPagina=10`);
  //}

  getAssuntosPaginados(pagina: number, tamanho: number): Observable<AssuntoListagem> {
    return this.http.get<AssuntoListagem>(`${this.baseUrl}/assuntos`, {
      params: {
        numeroPagina: pagina.toString(),
        tamanhoPagina: tamanho.toString(),
      },
    });
  }
}
