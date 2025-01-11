import { Component, OnInit } from '@angular/core';
import { Assunto, AssuntoListagem } from '../../../models/assunto.model';
import { AssuntoService } from '../../../services/assunto.service';

@Component({
  selector: 'app-assunto-listagem',
  templateUrl: './assunto-listagem.component.html',
  styleUrl: './assunto-listagem.component.css'
})
export class AssuntoListagemComponent implements OnInit {
  //assuntos: Assunto[] = [];
  listagem: AssuntoListagem = {
    itens: [], // Inicialize como um array vazio
    numeroPagina: 1, // Página inicial
    tamanhoPagina: 10, // Tamanho padrão
    totalRegistros: 0, // Sem registros inicialmente
    totalPaginas: 0, // Sem páginas inicialmente
  };

  isLoading = true;
  errorMessage = '';

  termoBusca: string = ''; // Termo de busca para o campo de descrição

  constructor(private assuntoService: AssuntoService) { }

  ngOnInit(): void {
    this.carregarListagem();
  }

  carregarListagem(): void {
    this.assuntoService
      .getAssuntosPaginados(
        this.listagem.numeroPagina,
        this.listagem.tamanhoPagina,
        this.termoBusca
      )
      .subscribe(
        (dados) => {
          this.listagem = dados; // Atualiza os dados com o resultado da busca
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

  mudarPagina(novaPagina: number): void {
    if (novaPagina >= 1 && novaPagina <= this.listagem.totalPaginas) {
      this.listagem.numeroPagina = novaPagina;
      this.carregarListagem();
    }
  }
}
