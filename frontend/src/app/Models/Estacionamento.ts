
export class Estacionamento {
  
constructor() {
    this.id = 0;
    this.dataEntrada = null;
    this.dataSaida = null;
    this.valorTotal = 0;
    this.veiculoId = 0;
    this.precoId = 0;
  }

  id?: number;
  dataEntrada: Date;
  dataSaida?: Date;
  valorTotal?: number;
  veiculoId: number;
  precoId: number;
}
