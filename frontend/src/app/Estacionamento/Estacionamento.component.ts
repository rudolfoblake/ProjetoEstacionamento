import { EstacionamentoService } from './../services/estacionamento.service';
import { Estacionamento } from './../models/Estacionamento';
import { Veiculo } from './../models/Veiculo';
import { VeiculoService } from './../services/veiculo.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-estacionamento',
  templateUrl: './estacionamento.component.html',
  styleUrls: ['./estacionamento.component.css']
})
export class EstacionamentoComponent implements OnInit {


  public titulo = 'Estacionamento';
  public estacionamentoSelecionado: Estacionamento;
  public estacionamentoForm: FormGroup;
  public modalRef: BsModalRef;
  public modo = 'post';


  public estacionamentos = [];

  public veiculos = [];

  constructor(private fb: FormBuilder, private modalService: BsModalService, private estacionamentoService: EstacionamentoService, private veiculoService: VeiculoService) {
    this.criarForm();
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  ngOnInit() {
    this.carregarEstacionamentos();
    this.carregarVeiculos();
  }

  carregarEstacionamentos() {
    this.estacionamentoService.getAll().subscribe(
      (resultado: Estacionamento[]) => {
        this.estacionamentos = resultado;
      },
      (erro: any) => {
        console.log(erro);
      }
    );
  }

  carregarVeiculos() {
    this.veiculoService.getAll().subscribe(
      (resultado: Veiculo[]) => {
        this.veiculos = resultado;
      },
      (erro: any) => {
        console.log(erro);
      }
    );
  }

  salvarEstacionamento(estacionamento: Estacionamento) {
    (estacionamento.id === 0 ? this.modo = 'post' : this.modo = 'put');
    console.log(this.modo);
    console.log(estacionamento);
    this.estacionamentoService[this.modo](estacionamento).subscribe(
      (resultado: Estacionamento) => {
        console.log(resultado);
        this.estacionamentoSelecionado = resultado;
        this.carregarEstacionamentos();
        this.voltar();
      },
      (erro: any) => {
        console.log(erro);
      }
    );
  }

  criarForm() {
    this.estacionamentoForm = this.fb.group({
      id: [''],
      dataEntrada: ['', Validators.required],
      veiculoId: ['', Validators.required]
    });
  }
  
  novoEstacionamento() {
    this.estacionamentoSelecionado = new Estacionamento();
    this.estacionamentoForm.patchValue(this.estacionamentoSelecionado);
  }

  estacionamentoSelect(estacionamento: Estacionamento) {
    this.estacionamentoSelecionado = estacionamento;
    this.estacionamentoForm.patchValue(estacionamento);
  }

  voltar() {
    this.estacionamentoSelecionado = null;
  }

  estacionamentoSubmit() {
    this.salvarEstacionamento(this.estacionamentoForm.value);
  }

  excluirEstacionamento(estacionamento: Estacionamento) {
    this.estacionamentoService.delete(estacionamento.id).subscribe(
      (retorno: string) => {
        console.log(retorno);
        this.carregarEstacionamentos
        ();
      },
      (erro: any) => {
        console.log(erro);
      }
    );
  }

}
