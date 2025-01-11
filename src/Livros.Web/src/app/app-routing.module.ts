import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AssuntoListagemComponent } from './componentes/assunto/assunto-listagem/assunto-listagem.component';

const routes: Routes = [
  { path: 'assuntos', component: AssuntoListagemComponent },
  { path: '', redirectTo: '/assuntos', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
