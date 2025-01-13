import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Livro, LivroListagem } from '../models/livro.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LivroService {

  private readonly baseUrl = '/api'; // Use apenas o caminho relativo

  constructor(private http: HttpClient) { }

  getLivrosPaginados(pagina: number, tamanho: number, busca: string = ''): Observable<LivroListagem> {
    const params: any = {
      numeroPagina: pagina.toString(),
      tamanhoPagina: tamanho.toString(),
    };
    if (busca) {
      params.titulo = busca; // Inclui o parâmetro de busca, se fornecido
    }

    return this.http.get<LivroListagem>(`${this.baseUrl}/livros`, { params });
  }

  cadastrarLivro(livro: Livro): Observable<Livro> {
    return this.http.post<Livro>(`${this.baseUrl}/livros`, livro);
  }

  atualizarLivro(livro: Livro): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/livros/${livro.id}`, livro);
  }

  excluirLivro(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/livros/${id}`);
  }
}
