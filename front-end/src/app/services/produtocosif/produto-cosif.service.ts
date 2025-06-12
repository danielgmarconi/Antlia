import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { MethodResponse } from '../../models/method-response';
import { HttpClient } from '@angular/common/http';
import { ProdutoCosif } from '../../models/produto-cosif';

@Injectable({
  providedIn: 'root'
})
export class ProdutoCosifService {
  private methodResponse : MethodResponse = { success: false, statusCode: 0, message: '', response: null };
  public callBack(methodResponse : MethodResponse){};
  private apiUrl = environment.webapi + '/ProdutoCosifCosif';
  constructor(private http : HttpClient) { }
  GetByAll()
  {
    const url = this.apiUrl + '?Status=A';
    this.http.get<MethodResponse>(url).subscribe({
      next: result => { this.methodResponse = result ;},
      error: err => {
        this.methodResponse.statusCode = 500;
        this.methodResponse.message = 'Erro';
        if (err.status === 0)
          this.methodResponse.response = 'Erro de conexão com o servidor. Verifique se a API está online.';
        else
          this.methodResponse = err.error;
         this.callBack(this.methodResponse);
      },
      complete: () => {
          this.callBack(this.methodResponse);
      }});
  }
  GetByModel(produtoCosif: ProdutoCosif){

    let url = this.apiUrl + '?';
    if(produtoCosif.id != undefined && produtoCosif.id != 0)
      url += 'id=' + produtoCosif.id +'&';
    if(produtoCosif.produtoId != undefined  && produtoCosif.produtoId != 0)
      url += 'produtoid=' + produtoCosif.produtoId +'&';
    if(produtoCosif.codigo != undefined && produtoCosif.codigo.length != 0)
      url += 'codigo=' + produtoCosif.codigo +'&';
    if(produtoCosif.classificacao != undefined && produtoCosif.classificacao.length != 0)
      url += 'classificacao=' + produtoCosif.classificacao +'&';
    if(produtoCosif.status != undefined && produtoCosif.status.length != 0)
      url += 'status=' + produtoCosif.status +'&';
    url = url.substring(0, url.length - 1);
    this.http.get<MethodResponse>(url).subscribe({
      next: result => { this.methodResponse = result ;},
      error: err => {
        this.methodResponse.statusCode = 500;
        this.methodResponse.message = 'Erro';
        if (err.status === 0)
          this.methodResponse.response = 'Erro de conexão com o servidor. Verifique se a API está online.';
        else
          this.methodResponse = err.error;
         this.callBack(this.methodResponse);
      },
      complete: () => {
          this.callBack(this.methodResponse);
      }});
  }
  Create(produtoCosif: ProdutoCosif){
    const val: ProdutoCosif = produtoCosif;
    this.http.post<MethodResponse>(this.apiUrl, val).subscribe({
      next: result => { this.methodResponse = result ;},
      error: err => {
        this.methodResponse.statusCode = 500;
        this.methodResponse.message = 'Erro';
        if (err.status === 0)
          this.methodResponse.response = 'Erro de conexão com o servidor. Verifique se a API está online.';
        else
          this.methodResponse = err.error;
         this.callBack(this.methodResponse);
      },
      complete: () => {
          this.callBack(this.methodResponse);
      }});
  }
  Update(produtoCosif: ProdutoCosif){
    const val: ProdutoCosif = produtoCosif;
    this.http.put<MethodResponse>(this.apiUrl, val).subscribe({
      next: result => { this.methodResponse = result ;},
      error: err => {
        this.methodResponse.statusCode = 500;
        this.methodResponse.message = 'Erro';
        if (err.status === 0)
          this.methodResponse.response = 'Erro de conexão com o servidor. Verifique se a API está online.';
        else
          this.methodResponse = err.error;
         this.callBack(this.methodResponse);
      },
      complete: () => {
          this.callBack(this.methodResponse);
      }});
  }
  Remove(id:number){
    let url = this.apiUrl + '/' + id;
    this.http.delete<MethodResponse>(url).subscribe({
      next: result => { this.methodResponse = result ;},
      error: err => {
        this.methodResponse.statusCode = 500;
        this.methodResponse.message = 'Erro';
        if (err.status === 0)
          this.methodResponse.response = 'Erro de conexão com o servidor. Verifique se a API está online.';
        else
          this.methodResponse = err.error;
         this.callBack(this.methodResponse);
      },
      complete: () => {
          this.callBack(this.methodResponse);
      }});
  }
}
