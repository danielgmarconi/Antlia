import { MovimentoManual } from './../../models/movimento-manual';
import { Component, OnInit } from '@angular/core';
import { ProdutoService } from '../../services/produto/produto.service';
import { ProdutoCosifService } from '../../services/produtocosif/produto-cosif.service';
import { MessageboxService } from '../../shared/messagebox/messagebox.service';
import { ModalContentService } from '../../services/ModalContent/modal-content-service.service';
import { FormsModule } from '@angular/forms';
import { ProdutoCosif } from '../../models/produto-cosif';
import { Produto } from '../../models/produto';
import { MethodResponse } from '../../models/method-response';
import { IconType } from '../../shared/messagebox/icon-type';
import { MovimentoManualService } from '../../services/movimentomanual/movimento-manual.service';

@Component({
  selector: 'app-movimento-manual',
  imports: [FormsModule],
  templateUrl: './movimento-manual.component.html',
  styleUrl: './movimento-manual.component.scss'
})
export class MovimentoManualComponent implements OnInit {
  produtos: Produto[] = [];
  produtoCosif: ProdutoCosif[] = [];
  pesquisaProdutoCosifs: ProdutoCosif[] = [];
  cadProdutoCosifs: ProdutoCosif[] = [];
  movimentoManuals: MovimentoManual[] = [];
  pesquisaMovimentoManual : MovimentoManual = {id: undefined,
                                               produtoId: 0,
                                               produtoCosifId: 0,
                                               mes: 0,
                                               ano: new Date().getFullYear(),
                                               numeroLancamento:undefined,
                                               valor: undefined,
                                               descricao:''};
  cadMovimentoManual : MovimentoManual = {id: 0,
                                           produtoId: 0,
                                           produtoCosifId: 0,
                                           mes: new Date().getMonth(),
                                           ano: new Date().getFullYear(),
                                           numeroLancamento:undefined,
                                           valor: 0,
                                           descricao:''};
  constructor(private produtoService:ProdutoService,
              private produtoCosifService:ProdutoCosifService,
              private movimentoManualService : MovimentoManualService,
              private messageboxService: MessageboxService,
              private modalcontentService : ModalContentService){}
  validate() : boolean
  {
    if(this.cadMovimentoManual.produtoId != undefined && this.cadMovimentoManual.produtoId == 0)
    {
      this.messageboxService.openModal('Atenção', 'Favor preencher o campo Produto.', IconType.info);
      return false;
    }
    if(this.cadMovimentoManual.produtoCosifId != undefined && this.cadMovimentoManual.produtoCosifId == 0)
    {
      this.messageboxService.openModal('Atenção', 'Favor preencher o campo Produto Cosif.', IconType.info);
      return false;
    }
    if(this.cadMovimentoManual.mes != undefined && this.cadMovimentoManual.mes == 0)
    {
      this.messageboxService.openModal('Atenção', 'Favor preencher o campo Mês.', IconType.info);
      return false;
    }
    if(this.cadMovimentoManual.ano == undefined  || this.cadMovimentoManual.ano <= 0)
    {
      this.messageboxService.openModal('Atenção', 'Favor preencher o campo Ano.', IconType.info);
      return false;
    }
    if(this.cadMovimentoManual.valor == undefined || (this.cadMovimentoManual.valor??0) <= 0)
    {
      this.messageboxService.openModal('Atenção', 'Favor preencher o campo Valor.', IconType.info);
      return false;
    }
    if(this.cadMovimentoManual.descricao == undefined || (this.cadMovimentoManual.descricao??'').length < 5)
    {
      this.messageboxService.openModal('Atenção', 'Favor preencher o campo Descrição com no minimo 5 caracteres.', IconType.info);
      return false;
    }
    return true;
  }
  loadGrid(methodResponse : MethodResponse): void {
    if(methodResponse.success)
      this.movimentoManuals = methodResponse.response as MovimentoManual[];
    else
      this.messageboxService.openModal(methodResponse.message??'', (methodResponse.response == undefined ? '' : methodResponse.response)  , IconType.danger);
  }
  onChangeProdutoPesquisa(event: Event): void {
    const valor = (event.target as HTMLSelectElement).value;
    if(valor != "0")
    {
      const busca : ProdutoCosif = {produtoId: parseInt(valor)};
      this.produtoCosifService.callBack = (methodResponse : MethodResponse)=> this.loadProdutoCosifPesquisaSelect(methodResponse);
      this.produtoCosifService.GetByModel(busca);
    }
    else
      this.pesquisaProdutoCosifs = [];
  }
  actionSave(methodResponse : MethodResponse)
  {
    if(methodResponse.success)
    {
      this.closeModal('CadastroModal');
      this.messageboxService.openModal('Atenção',  'Registro salvo com sucesso', IconType.info);
      this.movimentoManualService.callBack = (methodResponse : MethodResponse)=> this.loadGrid(methodResponse);
      this.movimentoManualService.GetByAll();
    }
    else
      this.messageboxService.openModal(methodResponse.message??'', (methodResponse.response == undefined ? '' : methodResponse.response)  , IconType.info);

  }
  onChangeProdutoCad(event: Event): void {
    const valor = (event.target as HTMLSelectElement).value;
    if(valor != "0")
    {
      const busca : ProdutoCosif = {produtoId: parseInt(valor)};
      this.produtoCosifService.callBack = (methodResponse : MethodResponse)=> this.loadProdutoCosifCadSelect(methodResponse);
      this.produtoCosifService.GetByModel(busca);
    }
    else
      this.pesquisaProdutoCosifs = [];
  }
  ngOnInit(): void {
    this.produtoService.callBack = (methodResponse : MethodResponse)=> this.loadProdutoSelect(methodResponse);
    this.produtoService.GetByAll();
    this.movimentoManualService.callBack = (methodResponse : MethodResponse)=> this.loadGrid(methodResponse);
    this.movimentoManualService.GetByAll();
  }
  loadProdutoSelect(methodResponse : MethodResponse): void {
      if(methodResponse.success)
        this.produtos = methodResponse.response as Produto[];
      else
        this.messageboxService.openModal(methodResponse.message??'', (methodResponse.response == undefined ? '' : methodResponse.response)  , IconType.danger);
  }
  loadProdutoCosifPesquisaSelect(methodResponse : MethodResponse): void {
      if(methodResponse.success)
        this.pesquisaProdutoCosifs = methodResponse.response as ProdutoCosif[];
      else
        this.messageboxService.openModal(methodResponse.message??'', (methodResponse.response == undefined ? '' : methodResponse.response)  , IconType.danger);
  }
  loadProdutoCosifCadSelect(methodResponse : MethodResponse): void {
      if(methodResponse.success)
        this.cadProdutoCosifs = methodResponse.response as ProdutoCosif[];
      else
        this.messageboxService.openModal(methodResponse.message??'', (methodResponse.response == undefined ? '' : methodResponse.response)  , IconType.danger);
  }
  actionRemove(methodResponse : MethodResponse)
  {
    if(methodResponse.success)
    {
      this.messageboxService.openModal('Atenção',  'Registro removido com sucesso', IconType.warning);
      this.movimentoManualService.callBack = (methodResponse : MethodResponse)=> this.loadGrid(methodResponse);
      this.movimentoManualService.GetByAll();
    }
    else
      this.messageboxService.openModal(methodResponse.message??'', (methodResponse.response == undefined ? '' : methodResponse.response)  , IconType.info);

  }
  novo(): void {
    this.cadMovimentoManual = {id: 0,
                               produtoId: 0,
                               produtoCosifId: 0,
                               mes: new Date().getMonth(),
                               ano: new Date().getFullYear(),
                               numeroLancamento:undefined,
                               valor: 0,
                               descricao:''};
    this.modalcontentService.openModal('CadastroModal');
  }
  pesquisar()
  {
    if(this.pesquisaMovimentoManual.id == undefined &&
      this.pesquisaMovimentoManual.produtoId == 0 &&
      this.pesquisaMovimentoManual.produtoCosifId == 0 &&
      this.pesquisaMovimentoManual.mes == 0 &&
      this.pesquisaMovimentoManual.ano == undefined)
    {
      this.messageboxService.openModal('Atenção', 'Favor selecionar alguns dos filtros.', IconType.info);
      return;
    }
    this.movimentoManualService.callBack = (methodResponse : MethodResponse)=> this.loadGrid(methodResponse);
    this.movimentoManualService.GetByModel(this.pesquisaMovimentoManual);
  }
  aditar(movimentoManual: MovimentoManual) : void {
    this.cadMovimentoManual = Object.assign({}, movimentoManual);
    this.modalcontentService.openModal('CadastroModal');
  }
  removeItem(id:number)
  {
    this.movimentoManualService.callBack = (methodResponse : MethodResponse)=> this.actionRemove(methodResponse);
    this.movimentoManualService.Remove(id);
  }
  salvar() :  void {
    if(!this.validate())
      return;
    this.cadMovimentoManual.numeroLancamento = parseInt((this.cadMovimentoManual.mes??0).toString() + (this.cadMovimentoManual.ano??0).toString())
    console.log(this.cadMovimentoManual);
    this.movimentoManualService.callBack = (methodResponse : MethodResponse)=> this.actionSave(methodResponse);
    if(this.cadMovimentoManual.id == 0)
    {
      this.cadMovimentoManual.id = undefined;
      this.movimentoManualService.Create(this.cadMovimentoManual);
    }
    else
      this.movimentoManualService.Update(this.cadMovimentoManual);
  }
    closeModal(id:string): void {
    this.modalcontentService. closeModal(id);
  }
}
