import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Assunto, AssuntoListagem } from '../models/assunto.model';

@Injectable({
  providedIn: 'root'
})
export class AssuntoService {

  private readonly baseUrl = '/api'; // Use apenas o caminho relativo

  constructor(private http: HttpClient) { }

  getAssuntosPaginados(pagina: number, tamanho: number, busca: string = ''): Observable<AssuntoListagem> {
    const params: any = {
      numeroPagina: pagina.toString(),
      tamanhoPagina: tamanho.toString(),
    };
    if (busca) {
      params.descricao = busca; // Inclui o parâmetro de busca, se fornecido
    }

    return this.http.get<AssuntoListagem>(`${this.baseUrl}/assuntos`, { params });
  }

  cadastrarAssunto(assunto: Assunto): Observable<Assunto> {
    return this.http.post<Assunto>(`${this.baseUrl}/assuntos`, assunto);
  }

  atualizarAssunto(assunto: Assunto): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/assuntos/${assunto.id}`, assunto);
  }

  excluirAssunto(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/assuntos/${id}`);
  }
}
