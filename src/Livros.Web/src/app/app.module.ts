import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AssuntoListagemComponent } from './componentes/assunto/assunto-listagem/assunto-listagem.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './componentes/home/home/home.component';
import { AutorListagemComponent } from './componentes/autor/autor-listagem/autor-listagem.component';
import { LivroListagemComponent } from './componentes/livro/livro-listagem/livro-listagem.component';

@NgModule({
  declarations: [
    AppComponent,
    AssuntoListagemComponent,
    HomeComponent,
    AutorListagemComponent,
    LivroListagemComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
