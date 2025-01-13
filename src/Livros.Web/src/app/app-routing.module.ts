import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AssuntoListagemComponent } from './componentes/assunto/assunto-listagem/assunto-listagem.component';
import { HomeComponent } from './componentes/home/home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'assuntos', component: AssuntoListagemComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
