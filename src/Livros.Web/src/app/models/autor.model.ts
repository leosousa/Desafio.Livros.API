export interface Autor {
  id: number;
  nome: string;
}

export interface AutorItem {
  id: number;
  nome: string;
  quantidadeLivros: boolean;
}

export interface AutorListagem {
  itens: AutorItem[];
  numeroPagina: number;
  tamanhoPagina: number;
  totalRegistros: number;
  totalPaginas: number;
}
