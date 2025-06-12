import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Produto } from '../../models/produto';
import { environment } from '../../../environments/environment';
import { MethodResponse } from '../../models/method-response';

@Injectable({
  providedIn: 'root'
})
export class ProdutoService {
  private methodResponse : MethodResponse = { success: false, statusCode: 0, message: '', response: null };
  public callBack(methodResponse : MethodResponse){};
  private apiUrl = environment.webapi + '/Produto';
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
  GetByModel(produto:Produto){
    let url = this.apiUrl + '?';
    if(produto.id != undefined)
      url += 'id=' + produto.id +'&';
    if(produto.descricao != undefined)
      url += 'descricao=' + produto.descricao +'&';
    if(produto.status != undefined)
      url += 'status=' + produto.status +'&';
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
  Create(produto:Produto){
    const val: Produto = produto;
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
  Update(produto:Produto){
    const val: Produto = produto;
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
