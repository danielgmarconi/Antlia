import { ProdutoCosif } from './../../models/produto-cosif';
import { Component, OnInit } from '@angular/core';
import { Produto } from '../../models/produto';
import { ProdutoService } from '../../services/produto/produto.service';
import { MessageboxService } from '../../shared/messagebox/messagebox.service';
import { ModalContentService } from '../../services/ModalContent/modal-content-service.service';
import { MethodResponse } from '../../models/method-response';
import { IconType } from '../../shared/messagebox/icon-type';
import { ProdutoCosifService } from '../../services/produtocosif/produto-cosif.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-produto-cosif',
  imports: [FormsModule],
  templateUrl: './produto-cosif.component.html',
  styleUrl: './produto-cosif.component.scss'
})
export class ProdutoCosifComponent implements OnInit {
  produtos: Produto[] = [];
  produtoCosifs: ProdutoCosif[] = [];
  cadProdutoCosif: ProdutoCosif = {id: undefined,
                                    produtoId: undefined,
                                    codigo: undefined,
                                    classificacao: undefined,
                                    status: undefined};
  pesquisaProdutoCosif: ProdutoCosif = {id: undefined,
                                        produtoId: 0,
                                        codigo: '',
                                        classificacao: '',
                                        status: ''};
  constructor(private produtoService:ProdutoService,
              private produtoCosifService:ProdutoCosifService,
              private messageboxService: MessageboxService,
              private modalcontentService : ModalContentService){}
  loadGrid(methodResponse : MethodResponse): void {
    if(methodResponse.success)
      this.produtoCosifs = methodResponse.response as ProdutoCosif[];
    else
      this.messageboxService.openModal(methodResponse.message??'', (methodResponse.response == undefined ? '' : methodResponse.response)  , IconType.danger);
  }
  validate() : boolean
  {
    if(this.cadProdutoCosif.status != undefined && this.cadProdutoCosif.status.length == 0)
    {
      this.messageboxService.openModal('Atenção', 'Favor preencher o campo Status.', IconType.info);
      return false;
    }
    if(this.cadProdutoCosif.produtoId != undefined && this.cadProdutoCosif.produtoId == 0)
    {
      this.messageboxService.openModal('Atenção', 'Favor preencher o campo Produto.', IconType.info);
      return false;
    }
    if(this.cadProdutoCosif.codigo != undefined && this.cadProdutoCosif.codigo.length  == 0)
    {
      this.messageboxService.openModal('Atenção', 'Favor preencher o campo Codigo.', IconType.info);
      return false;
    }
    return true;
  }
  actionSave(methodResponse : MethodResponse)
  {
    if(methodResponse.success)
    {
      this.closeModal('CadastroModal');
      this.messageboxService.openModal('Atenção',  'Registro salvo com sucesso', IconType.info);
      this.produtoCosifService.callBack = (methodResponse : MethodResponse)=> this.loadGrid(methodResponse);
      this.produtoCosifService.GetByAll();
    }
    else
      this.messageboxService.openModal(methodResponse.message??'', (methodResponse.response == undefined ? '' : methodResponse.response)  , IconType.info);

  }
  loadProdutoSelect(methodResponse : MethodResponse): void {
      if(methodResponse.success)
        this.produtos = methodResponse.response as Produto[];
      else
        this.messageboxService.openModal(methodResponse.message??'', (methodResponse.response == undefined ? '' : methodResponse.response)  , IconType.danger);
  }
  ngOnInit(): void {
    this.produtoService.callBack = (methodResponse : MethodResponse)=> this.loadProdutoSelect(methodResponse);
    this.produtoService.GetByAll();
    this.produtoCosifService.callBack = (methodResponse : MethodResponse)=> this.loadGrid(methodResponse);
    this.produtoCosifService.GetByAll();
  }
  novo(): void {
    this.modalcontentService.openModal('CadastroModal');
    this.cadProdutoCosif = {id: 0,
                             codigo: '',
                             classificacao: '',
                             status: ''};
  }
  pesquisar()
  {
    if(this.pesquisaProdutoCosif.id == undefined &&
      this.pesquisaProdutoCosif.produtoId == 0 &&
      this.pesquisaProdutoCosif.codigo?.length == 0 &&
      this.pesquisaProdutoCosif.classificacao?.length == 0 &&
      this.pesquisaProdutoCosif.status?.length ==0)
    {
      this.messageboxService.openModal('Atenção', 'Favor selecionar alguns dos filtros.', IconType.info);
      return;
    }
    this.produtoCosifService.callBack = (methodResponse : MethodResponse)=> this.loadGrid(methodResponse);
    this.produtoCosifService.GetByModel(this.pesquisaProdutoCosif);
  }
  actionRemove(methodResponse : MethodResponse)
  {
    if(methodResponse.success)
    {
      this.messageboxService.openModal('Atenção',  'Registro removido com sucesso', IconType.warning);
      this.produtoCosifService.callBack = (methodResponse : MethodResponse)=> this.loadGrid(methodResponse);
      this.produtoCosifService.GetByAll();
    }
    else
      this.messageboxService.openModal(methodResponse.message??'', (methodResponse.response == undefined ? '' : methodResponse.response)  , IconType.info);

  }
  removeItem(id:number)
  {
    this.produtoCosifService.callBack = (methodResponse : MethodResponse)=> this.actionRemove(methodResponse);
    this.produtoCosifService.Remove(id);
  }
  closeModal(id:string): void {
    this.modalcontentService. closeModal(id);
  }
  salvar() :  void {
    if(!this.validate())
      return;
    if(this.cadProdutoCosif.classificacao?.length == 0)
      this.cadProdutoCosif.classificacao = undefined
    this.produtoCosifService.callBack = (methodResponse : MethodResponse)=> this.actionSave(methodResponse);
    if(this.cadProdutoCosif.id == 0)
    {
      this.cadProdutoCosif.id = undefined;
      this.produtoCosifService.Create(this.cadProdutoCosif);
    }
    else
      this.produtoCosifService.Update(this.cadProdutoCosif);
  }
  aditar(produto: Produto) : void {
    this.cadProdutoCosif = Object.assign({}, produto);
    this.modalcontentService.openModal('CadastroModal');
  }
}
