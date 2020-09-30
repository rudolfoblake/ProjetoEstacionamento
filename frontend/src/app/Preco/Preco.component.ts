import { PrecoService } from './../services/preco.service';
import { Preco } from './../models/Preco';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';



@Component({
  selector: 'app-preco',
  templateUrl: './preco.component.html',
  styleUrls: ['./preco.component.css']
})
export class PrecoComponent implements OnInit {

  public titulo = 'Precos';
  public precoSelecionado: Preco;
  public precoForm: FormGroup;
  public modalRef: BsModalRef;
  public modo = 'post';

  public precos = [];

  constructor(private fb: FormBuilder, private modalService: BsModalService, private precoService: PrecoService) {
    this.criarForm();
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  ngOnInit() {
    this.carregarPrecos();
  }

  carregarPrecos() {
    this.precoService.getAll().subscribe(
      (resultado: Preco[]) => {
        this.precos = resultado;
      },
      (erro: any) => {
        console.log(erro);
      }
    );
  }

  salvarPreco(preco: Preco) {
    (preco.id === 0 ? this.modo = 'post' : this.modo = 'put');
    console.log(this.modo);
    console.log(preco);
    this.precoService[this.modo](preco).subscribe(
      (resultado: Preco) => {
        console.log(resultado);
        this.precoSelecionado = resultado;
        this.carregarPrecos();
        this.voltar();
      },
      (erro: any) => {
        console.log(erro);
      }
    );
  }

  criarForm() {
    this.precoForm = this.fb.group({
      id: [''],
      vigenciaInicial: ['', Validators.required],
      vigenciaFinal: ['', Validators.required],
      valorHora: ['', Validators.required]
    });
  }

  novoPreco() {
    this.precoSelecionado = new Preco();
    this.precoForm.patchValue(this.precoSelecionado);
  }

  precoSelect(preco: Preco) {
    this.precoSelecionado = preco;
    this.precoForm.patchValue(preco);
  }

  voltar() {
    this.precoSelecionado = null;
  }

  precoSubmit() {
    this.salvarPreco(this.precoForm.value);
  }

}
