import { Component, OnInit } from '@angular/core';
import { Assunto, AssuntoListagem } from '../../../models/assunto.model';
import { AssuntoService } from '../../../services/assunto.service';

declare var bootstrap: any; // Para manipular o modal usando Bootstrap JS

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

  assuntoEdicao: Assunto = { id: 0, descricao: '' };
  private modalEdicao: any;

  constructor(private assuntoService: AssuntoService) { }

  ngOnInit(): void {
    this.carregarListagem();

    this.modalEdicao = new bootstrap.Modal(document.getElementById('modalEdicao'));
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

  //editar(id: number): void {
  //  console.log(`Editar assunto com ID: ${id}`);
  //  // Lógica de edição aqui
  //}

  abrirModalEditar(assunto: Assunto): void {
    this.assuntoEdicao = { ...assunto }; // Copia os dados para evitar alterações diretas
    this.modalEdicao.show();
  }

  salvarEdicao(): void {
    this.assuntoService.atualizarAssunto(this.assuntoEdicao).subscribe({
      next: () => {
        this.modalEdicao.hide();
        this.carregarListagem();
      },
      error: (erro) => console.error('Erro ao salvar edição', erro),
    });
  }

  excluir(id: number): void {
    if (confirm(`Tem certeza que deseja excluir o assunto com ID: ${id}?`)) {
      console.log(`Excluir assunto com ID: ${id}`);
      // Lógica de exclusão aqui
      this.carregarListagem(); // Atualiza a listagem após excluir
    }
  }
}
