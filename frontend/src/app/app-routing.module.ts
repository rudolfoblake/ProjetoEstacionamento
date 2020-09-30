import { EstacionamentoComponent } from './estacionamento/estacionamento.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { VeiculoComponent } from './veiculo/veiculo.component';
import { PrecoComponent } from './Preco/Preco.component';

const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'estacionamentos', component: EstacionamentoComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'veiculos', component: VeiculoComponent },
  { path: 'precos', component: PrecoComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
