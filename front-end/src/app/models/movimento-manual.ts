import { Produto } from "./produto";
import { ProdutoCosif } from "./produto-cosif";

export interface MovimentoManual {
  id?: number;
  produtoId?: number;
  produtoCosifId?: number;
  mes?: number;
  ano?: number;
  numeroLancamento?: number;
  valor?: number;
  descricao?: string;
  produto?: Produto;
  produtoCosif?: ProdutoCosif;
}
