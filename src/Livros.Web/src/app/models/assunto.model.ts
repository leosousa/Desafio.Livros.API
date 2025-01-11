export interface Assunto {
  id: number;
  descricao: string;
}

export interface AssuntoListagem {
  itens: Assunto[];
  numeroPagina: number;
  tamanhoPagina: number;
  totalRegistros: number;
  totalPaginas: number;
}
