import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AutorListagem } from '../models/autor.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AutorService {

  private readonly baseUrl = '/api'; // Use apenas o caminho relativo

  constructor(private http: HttpClient) { }

  getAutoresPaginados(pagina: number, tamanho: number, busca: string = ''): Observable<AutorListagem> {
    const params: any = {
      numeroPagina: pagina.toString(),
      tamanhoPagina: tamanho.toString(),
    };
    if (busca) {
      params.nome = busca; // Inclui o parâmetro de busca, se fornecido
    }

    return this.http.get<AutorListagem>(`${this.baseUrl}/autores`, { params });
  }
}
