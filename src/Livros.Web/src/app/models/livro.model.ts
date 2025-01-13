export interface Livro {
  id: number;
  titulo: string;
  editora: string;
  anoPublicacao: number;
}

export interface LivroListagem {
  itens: Livro[];
  numeroPagina: number;
  tamanhoPagina: number;
  totalRegistros: number;
  totalPaginas: number;
}
