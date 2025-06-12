import { Produto } from './../../models/produto';
import { Component, OnInit } from '@angular/core';
import { ProdutoService } from '../../services/produto/produto.service';
import { MethodResponse } from '../../models/method-response';
import { MessageboxService } from '../../shared/messagebox/messagebox.service';
import { IconType } from '../../shared/messagebox/icon-type';
import { FormsModule } from '@angular/forms';
import { ModalContentService } from '../../services/ModalContent/modal-content-service.service';

@Component({
  selector: 'app-produto',
  imports: [FormsModule],
  templateUrl: './produto.component.html',
  styleUrl: './produto.component.scss'
})
export class ProdutoComponent implements OnInit {
  pesquisaId?:number = undefined;
  pesquisaDescricao:string ='';
  pesquisaStatus:string = '';
  produtos: Produto[] = [];
  cadProduto: Produto = {id: undefined,
                         descricao: undefined,
                         status: undefined}
  constructor(private produtoService:ProdutoService, private messageboxService: MessageboxService,  private modalcontentService : ModalContentService){}
  onNumberInputMaxlength(event: Event, maxlength: number): void {
    const input = event.target as HTMLInputElement;
    if (input.value.length > maxlength) {
      input.value = input.value.slice(0, maxlength);
    }
  }
  validate() : boolean
  {
    if(this.cadProduto.status != undefined && this.cadProduto.status.length == 0)
    {
      this.messageboxService.openModal('Atenção', 'Favor preencher o campo Status.', IconType.info);
      return false;
    }
    if(this.cadProduto.descricao != undefined && this.cadProduto.descricao.length == 0)
    {
      this.messageboxService.openModal('Atenção', 'Favor preencher o campo Descrição.', IconType.info);
      return false;
    }
        if(this.cadProduto.descricao != undefined && this.cadProduto.descricao.length < 5)
    {
      this.messageboxService.openModal('Atenção', 'Descrição deve ter no minimos 5 caracteres.', IconType.info);
      return false;
    }
    return true;
  }
  loadGrid(methodResponse : MethodResponse): void {
    if(methodResponse.success)
      this.produtos = methodResponse.response as Produto[];
    else
      this.messageboxService.openModal(methodResponse.message??'', (methodResponse.response == undefined ? '' : methodResponse.response)  , IconType.danger);
  }
  actionSave(methodResponse : MethodResponse)
  {
    if(methodResponse.success)
    {
      this.closeModal('CadastroModal');
      this.messageboxService.openModal('Atenção',  'Registro salvo com sucesso', IconType.info);
      this.produtoService.callBack = (methodResponse : MethodResponse)=> this.loadGrid(methodResponse);
      this.produtoService.GetByAll();
    }
    else
      this.messageboxService.openModal(methodResponse.message??'', (methodResponse.response == undefined ? '' : methodResponse.response)  , IconType.info);

  }
  actionRemove(methodResponse : MethodResponse)
  {
    if(methodResponse.success)
    {
      this.messageboxService.openModal('Atenção',  'Registro removido com sucesso', IconType.warning);
      this.produtoService.callBack = (methodResponse : MethodResponse)=> this.loadGrid(methodResponse);
      this.produtoService.GetByAll();
    }
    else
      this.messageboxService.openModal(methodResponse.message??'', (methodResponse.response == undefined ? '' : methodResponse.response)  , IconType.info);

  }
  closeModal(id:string): void {
    this.modalcontentService. closeModal(id);
  }
  ngOnInit(): void {
    this.produtoService.callBack = (methodResponse : MethodResponse)=> this.loadGrid(methodResponse);
    this.produtoService.GetByAll();
  }
  removeItem(id:number)
  {
    this.produtoService.callBack = (methodResponse : MethodResponse)=> this.actionRemove(methodResponse);
    this.produtoService.Remove(id);
  }
  pesquisar()
  {

    const produto: Produto = {id: undefined,
                              descricao: undefined,
                              status: undefined};

    if(this.pesquisaId == undefined && this.pesquisaDescricao.length == 0 && this.pesquisaStatus.length == 0)
    {
      this.messageboxService.openModal('Atenção', 'Favor selecionar alguns dos filtros.', IconType.info);
      return;
    }
    if(this.pesquisaId != undefined)
      produto.id = this.pesquisaId;
    if(this.pesquisaDescricao.length > 0)
      produto.descricao = this.pesquisaDescricao;
    if(this.pesquisaStatus.length > 0)
      produto.status = this.pesquisaStatus;
    this.produtoService.callBack = (methodResponse : MethodResponse)=> this.loadGrid(methodResponse);
    this.produtoService.GetByModel(produto);
  }
  novo(): void {
    this.modalcontentService.openModal('CadastroModal');
    this.cadProduto = {id: 0,
                        descricao: '',
                        status: ''};
  }
  aditar(produto: Produto) : void {
    this.cadProduto = Object.assign({}, produto);
    this.modalcontentService.openModal('CadastroModal');
  }
  salvar() :  void {
    if(!this.validate())
      return;
    this.produtoService.callBack = (methodResponse : MethodResponse)=> this.actionSave(methodResponse);
    if(this.cadProduto.id == 0)
    {
      this.cadProduto.id = undefined;
      this.produtoService.Create(this.cadProduto);
    }
    else
      this.produtoService.Update(this.cadProduto);
  }
}
