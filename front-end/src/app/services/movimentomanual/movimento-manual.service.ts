import { Injectable } from '@angular/core';
import { MethodResponse } from '../../models/method-response';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { MovimentoManual } from '../../models/movimento-manual';

@Injectable({
  providedIn: 'root'
})
export class MovimentoManualService {

  private methodResponse : MethodResponse = { success: false, statusCode: 0, message: '', response: null };
  public callBack(methodResponse : MethodResponse){};
  private apiUrl = environment.webapi + '/MovimentoManual';
  constructor(private http : HttpClient) { }
  GetByAll()
  {
    const url = this.apiUrl + '?Ano=' +  new Date().getFullYear();
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
  GetByModel(movimentoManual: MovimentoManual){
    let url = this.apiUrl + '?';
    if(movimentoManual.id != undefined && movimentoManual.id != 0)
      url += 'id=' + movimentoManual.id +'&';
    if(movimentoManual.produtoId != undefined  && movimentoManual.produtoId != 0)
      url += 'produtoid=' + movimentoManual.produtoId +'&';
    if(movimentoManual.produtoCosifId != undefined  && movimentoManual.produtoCosifId != 0)
      url += 'produtoCosifId=' + movimentoManual.produtoCosifId +'&';
    if(movimentoManual.mes != undefined && movimentoManual.mes != 0)
      url += 'mes=' + movimentoManual.mes +'&';
    if(movimentoManual.ano != undefined && movimentoManual.ano != 0)
      url += 'ano=' + movimentoManual.ano +'&';
    if(movimentoManual.descricao != undefined && movimentoManual.descricao.length !=0)
      url += 'descricao=' + movimentoManual.descricao +'&';
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
  Create(movimentoManual: MovimentoManual){
    const val: MovimentoManual = movimentoManual;
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
  Update(movimentoManual: MovimentoManual){
    const val: MovimentoManual = movimentoManual;
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
