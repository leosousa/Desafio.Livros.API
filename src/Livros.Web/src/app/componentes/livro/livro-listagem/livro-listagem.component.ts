import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { Livro, LivroListagem } from '../../../models/livro.model';
import { LivroService } from '../../../services/livro.service';

declare var bootstrap: any; // Para manipular o modal usando Bootstrap JS

@Component({
  selector: 'app-livro-listagem',
  templateUrl: './livro-listagem.component.html',
  styleUrl: './livro-listagem.component.css'
})
export class LivroListagemComponent {

  ngAfterViewInit() {
    // Isso garante que o ViewChild seja acessível após a inicialização do componente.
    // Você pode realizar chamadas aqui, se necessário.
  }

  listagem: LivroListagem = {
    itens: [], // Inicialize como um array vazio
    numeroPagina: 1, // Página inicial
    tamanhoPagina: 10, // Tamanho padrão
    totalRegistros: 0, // Sem registros inicialmente
    totalPaginas: 0, // Sem páginas inicialmente
  };

  isLoading = true;
  errorMessage = '';

  termoBusca: string = ''; // Termo de busca para o campo de descrição

  novoLivro: Livro = { id: 0, titulo: '', editora: '', anoPublicacao: 0 };
  livroEdicao: Livro = { id: 0, titulo: '', editora: '', anoPublicacao: 0 };

  private modalCadastro: any;
  private modalEdicao: any;

  idAssuntoParaExcluir: number | null = null;
  private modalExcluir: any;

  toastVisible = false;
  toastMessage = '';
  toastType: 'success' | 'error' = 'success';
  toastTitle = 'Mensagem';

  paginas: number[] = [];

  constructor(private livroService: LivroService) { }

  ngOnInit(): void {
    this.carregarListagem();

    this.modalCadastro = new bootstrap.Modal(document.getElementById('modalCadastro'));
    this.modalEdicao = new bootstrap.Modal(document.getElementById('modalEdicao'));
    this.modalExcluir = new bootstrap.Modal(document.getElementById('modalExcluir'));
  }

  carregarListagem(): void {
    this.livroService
      .getLivrosPaginados(
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

  salvarCadastro(): void {
    this.livroService.cadastrarLivro(this.novoLivro).subscribe({
      next: () => {
        this.modalCadastro.hide();
        this.carregarListagem(); // Atualiza a listagem após o cadastro
        this.mostrarToast('Livro cadastrado com sucesso!', 'success');
      },
      error: (erro) => {
        console.error('Erro ao cadastrar livro', erro);
        this.mostrarToast('Erro ao cadastrar o livro.', 'error');
      }
    });
  }

  abrirModalEditar(livro: Livro): void {
    this.livroEdicao = { ...livro }; // Copia os dados para evitar alterações diretas
    this.modalEdicao.show();
  }

  salvarEdicao(): void {
    this.livroService.atualizarLivro(this.livroEdicao).subscribe({
      next: () => {
        this.modalEdicao.hide();
        this.carregarListagem();
        this.mostrarToast('Livro editado com sucesso!', 'success');
      },
      error: (erro) => {
        console.error('Erro ao salvar livro', erro);
        this.mostrarToast('Erro ao editar o livro.', 'error');
      }
    });
  }

  abrirModalExcluir(id: number): void {
    this.idAssuntoParaExcluir = id;
    this.modalExcluir.show();
  }

  confirmarExclusao(): void {
    if (this.idAssuntoParaExcluir !== null) {
      this.livroService.excluirLivro(this.idAssuntoParaExcluir).subscribe({
        next: () => {
          this.modalExcluir.hide();
          this.carregarListagem(); // Atualiza a listagem após a exclusão
          this.mostrarToast('Livro excluído com sucesso!', 'success');
        },
        error: (erro) => {
          console.error('Erro ao excluir livro', erro);
          this.mostrarToast('Erro ao excluir o livro.', 'error');
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
