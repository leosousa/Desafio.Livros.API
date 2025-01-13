export interface Assunto {
  id: number;
  descricao: string;
}

export interface AssuntoItem {
  id: number;
  descricao: string;
  possuiLivrosAssociados: boolean;
}

export interface AssuntoListagem {
  itens: AssuntoItem[];
  numeroPagina: number;
  tamanhoPagina: number;
  totalRegistros: number;
  totalPaginas: number;
}
