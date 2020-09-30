import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EstacionamentoComponent } from './estacionamento/estacionamento.component';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { ModalModule } from 'ngx-bootstrap/modal';
import { HttpClientModule } from '@angular/common/http';
import { NavComponent } from './nav/nav.component';
import { TituloComponent } from './titulo/titulo.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { VeiculoComponent } from './veiculo/veiculo.component';
import { PrecoComponent } from './Preco/Preco.component';


@NgModule({
   declarations: [	
      AppComponent,
      EstacionamentoComponent,
      NavComponent,
      TituloComponent,
      DashboardComponent,
      VeiculoComponent,
      PrecoComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      BsDropdownModule.forRoot(),
      BrowserAnimationsModule,
      FormsModule,
      ReactiveFormsModule,
      ModalModule.forRoot(),
      HttpClientModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
