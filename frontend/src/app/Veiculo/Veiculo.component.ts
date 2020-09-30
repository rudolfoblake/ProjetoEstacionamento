import { VeiculoService } from './../services/veiculo.service';
import { Veiculo } from './../models/Veiculo';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';



@Component({
  selector: 'app-veiculo',
  templateUrl: './Veiculo.component.html',
  styleUrls: ['./Veiculo.component.css']
})
export class VeiculoComponent implements OnInit {

  public titulo = 'Veiculos';
  public veiculoSelecionado: Veiculo;
  public veiculoForm: FormGroup;
  public modalRef: BsModalRef;
  public modo = 'post';

  public veiculos = [];

  constructor(private fb: FormBuilder, private modalService: BsModalService, private veiculoService: VeiculoService) {
    this.criarForm();
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  ngOnInit() {
    this.carregarVeiculos();
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

  salvarVeiculo(veiculo: Veiculo) {
    (veiculo.id === 0 ? this.modo = 'post' : this.modo = 'put');

    this.veiculoService[this.modo](veiculo).subscribe(
      (resultado: Veiculo) => {
        console.log(resultado);
        this.veiculoSelecionado = resultado;
        this.carregarVeiculos();
        this.voltar();
      },
      (erro: any) => {
        console.log(erro);
      }
    );
  }

  criarForm() {
    this.veiculoForm = this.fb.group({
      id: [''],
      placa: ['', Validators.required],
      marca: ['', Validators.required],
      modelo: ['', Validators.required],
      cor: ['', Validators.required]
    });
  }

  novoVeiculo() {
    this.veiculoSelecionado = new Veiculo();
    this.veiculoForm.patchValue(this.veiculoSelecionado);
  }

  veiculoSelect(veiculo: Veiculo) {
    this.veiculoSelecionado = veiculo;
    this.veiculoForm.patchValue(veiculo);
  }

  voltar() {
    this.veiculoSelecionado = null;
  }

  veiculoSubmit() {
    this.salvarVeiculo(this.veiculoForm.value);
  }

  excluirVeiculo(veiculo: Veiculo) {
    this.veiculoService.delete(veiculo.id).subscribe(
      (retorno: string) => {
        console.log(retorno);
        this.carregarVeiculos();
      },
      (erro: any) => {
        console.log(erro);
      }
    );
  }

}
