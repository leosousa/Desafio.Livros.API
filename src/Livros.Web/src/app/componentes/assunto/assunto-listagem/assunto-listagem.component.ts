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

  novoAssunto: Assunto = { id: 0, descricao: '' };
  assuntoEdicao: Assunto = { id: 0, descricao: '' };

  private modalCadastro: any;
  private modalEdicao: any;

  idAssuntoParaExcluir: number | null = null;
  private modalExcluir: any;

  toastVisible = false;
  toastMessage = '';
  toastType: 'success' | 'error' = 'success';
  toastTitle = 'Mensagem';

  paginas: number[] = [];

  constructor(private assuntoService: AssuntoService) { }

  ngOnInit(): void {
    this.carregarListagem();

    this.modalCadastro = new bootstrap.Modal(document.getElementById('modalCadastro'));
    this.modalEdicao = new bootstrap.Modal(document.getElementById('modalEdicao'));
    this.modalExcluir = new bootstrap.Modal(document.getElementById('modalExcluir'));
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

  //mudarPagina(novaPagina: number): void {
  //  if (novaPagina >= 1 && novaPagina <= this.listagem.totalPaginas) {
  //    this.listagem.numeroPagina = novaPagina;
  //    this.carregarListagem();
  //  }
  //}

  mudarPagina(pagina: number): void {
    if (pagina > 0 && pagina <= this.listagem.totalPaginas) {
      this.listagem.numeroPagina = pagina;
      this.carregarListagem();
    }
  }

  abrirModalCadastro(): void {
    this.novoAssunto = { id: 0, descricao: '' }; // Inicializa o objeto
    this.modalCadastro.show();
  }

  salvarCadastro(): void {
    this.assuntoService.cadastrarAssunto(this.novoAssunto).subscribe({
      next: () => {
        this.modalCadastro.hide();
        this.carregarListagem(); // Atualiza a listagem após o cadastro
        this.mostrarToast('Assunto cadastrado com sucesso!', 'success');
      },
      error: (erro) => {
        console.error('Erro ao cadastrar assunto', erro);
        this.mostrarToast('Erro ao cadastrar o assunto.', 'error');
      }
    });
  }

  abrirModalEditar(assunto: Assunto): void {
    this.assuntoEdicao = { ...assunto }; // Copia os dados para evitar alterações diretas
    this.modalEdicao.show();
  }

  salvarEdicao(): void {
    this.assuntoService.atualizarAssunto(this.assuntoEdicao).subscribe({
      next: () => {
        this.modalEdicao.hide();
        this.carregarListagem();
        this.mostrarToast('Assunto editado com sucesso!', 'success');
      },
      error: (erro) => {
        console.error('Erro ao salvar edição', erro);
        this.mostrarToast('Erro ao editar o assunto.', 'error');
      }
    });
  }

  abrirModalExcluir(id: number): void {
    this.idAssuntoParaExcluir = id;
    this.modalExcluir.show();
  }

  confirmarExclusao(): void {
    if (this.idAssuntoParaExcluir !== null) {
      this.assuntoService.excluirAssunto(this.idAssuntoParaExcluir).subscribe({
        next: () => {
          this.modalExcluir.hide();
          this.carregarListagem(); // Atualiza a listagem após a exclusão
          this.mostrarToast('Assunto excluído com sucesso!', 'success');
        },
        error: (erro) => {
          console.error('Erro ao excluir assunto', erro);
          this.mostrarToast('Erro ao excluir o assunto.', 'error');
        }
      });
    }
  }

  mostrarToast(message: string, type: 'success' | 'error'): void {
    this.toastMessage = message;
    this.toastType = type;
    this.toastTitle = type === 'success' ? 'Sucesso' : 'Erro';
    this.toastVisible = true;

    // Esconde o toast automaticamente após 3 segundos
    setTimeout(() => {
      this.toastVisible = false;
    }, 3000);
  }

  fecharToast(): void {
    this.toastVisible = false;
  }
}
