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

  constructor(private assuntoService: AssuntoService) { }

  //ngOnInit(): void {
  //  this.assuntoService.getAssuntos().subscribe({
  //    next: (data) => {
  //      this.assuntoListagem = data;
  //      this.isLoading = false;
  //    },
  //    error: (error) => {
  //      console.error('Erro ao carregar os assuntos:', error);
  //      this.errorMessage = 'Erro ao carregar os assuntos.';
  //      this.isLoading = false;
  //    },
  //  });
  //}

  ngOnInit(): void {
    this.carregarListagem();
  }

  carregarListagem(): void {
    this.assuntoService
      .getAssuntosPaginados(this.listagem.numeroPagina, this.listagem.tamanhoPagina)
      .subscribe(
        (dados) => {
          this.listagem = dados; // Atualize os dados da página
        },
        (erro) => {
          console.error('Erro ao carregar a listagem:', erro);
        }
      );
  }

  mudarPagina(novaPagina: number): void {
    if (novaPagina >= 1 && novaPagina <= this.listagem.totalPaginas) {
      this.listagem.numeroPagina = novaPagina;
      this.carregarListagem(); // Recarregar os dados da nova página
    }
  }
}
