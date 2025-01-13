import { Component, OnInit } from '@angular/core';
import { AutorListagem } from '../../../models/autor.model';
import { AutorService } from '../../../services/autor.service';

@Component({
  selector: 'app-autor-listagem',
  templateUrl: './autor-listagem.component.html',
  styleUrl: './autor-listagem.component.css'
})
export class AutorListagemComponent implements OnInit {
  listagem: AutorListagem = {
    itens: [], // Inicialize como um array vazio
    numeroPagina: 1, // Página inicial
    tamanhoPagina: 10, // Tamanho padrão
    totalRegistros: 0, // Sem registros inicialmente
    totalPaginas: 0, // Sem páginas inicialmente
  };

  isLoading = true;
  errorMessage = '';

  termoBusca: string = ''; // Termo de busca para o campo de descrição

  paginas: number[] = [];

  constructor(private autorService: AutorService) { }

  ngOnInit(): void {
    this.carregarListagem();
  }

  carregarListagem(): void {
    this.autorService
      .getAutoresPaginados(
        this.listagem.numeroPagina,
        this.listagem.tamanhoPagina,
        this.termoBusca
      )
      .subscribe(
        (dados) => {
          this.listagem = dados; // Atualiza os dados com o resultado da busca
          this.atualizarPaginas();
        },
        (erro) => {
          console.error('Erro ao carregar a listagem:', erro);
        }
      );
  }

  buscar(): void {
    this.listagem.numeroPagina = 1; // Reinicie na página 1 para nova busca
    this.carregarListagem();
  }

  atualizarPaginas(): void {
    this.paginas = [];
    for (let i = 1; i <= this.listagem.totalPaginas; i++) {
      this.paginas.push(i);
    }
  }

  mudarPagina(pagina: number): void {
    if (pagina > 0 && pagina <= this.listagem.totalPaginas) {
      this.listagem.numeroPagina = pagina;
      this.carregarListagem();
    }
  }
}
