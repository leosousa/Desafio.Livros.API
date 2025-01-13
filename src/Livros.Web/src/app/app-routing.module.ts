import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AssuntoListagemComponent } from './componentes/assunto/assunto-listagem/assunto-listagem.component';
import { HomeComponent } from './componentes/home/home/home.component';
import { AutorListagemComponent } from './componentes/autor/autor-listagem/autor-listagem.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'assuntos', component: AssuntoListagemComponent },
  { path: 'autores', component: AutorListagemComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
