import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AssuntoListagemComponent } from './componentes/assunto/assunto-listagem/assunto-listagem.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from './componentes/home/home/home.component';
import { AutorListagemComponent } from './componentes/autor/autor-listagem/autor-listagem.component';

@NgModule({
  declarations: [
    AppComponent,
    AssuntoListagemComponent,
    HomeComponent,
    AutorListagemComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
