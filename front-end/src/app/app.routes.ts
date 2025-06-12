import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { ProdutoComponent } from './pages/produto/produto.component';
import { ProdutoCosifComponent } from './pages/produto-cosif/produto-cosif.component';
import { MovimentoManualComponent } from './pages/movimento-manual/movimento-manual.component';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'home'
  },
  {
    path:'home',
    component:HomeComponent
  },
  {
    path:'produto',
    component:ProdutoComponent
  },
  {
    path:'produtocosif',
    component:ProdutoCosifComponent
  },
  {
    path:'movimentomanual',
    component:MovimentoManualComponent
  },
  {
    path: '***',
    redirectTo:'home'
  }
];
