import { Produto } from "./produto";

export interface ProdutoCosif {
  id?: number;
  produtoId?: number;
  codigo?: string;
  classificacao?: string;
  status?: string;
  produto?: Produto;
}
