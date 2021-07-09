import { Component, OnInit } from '@angular/core';
import School from 'src/app/api/escolaApi';

type escolaHeader = "escolaId"|"nomeEscola"|"enderecoEscola"|"telefoneEscola"|"actions";
type escolaData = {[x in escolaHeader]:string}
// type tableHeader = {[x in escolaHeader]:string}

@Component({
  selector: 'app-school-list',
  templateUrl: './school-list.component.html',
  styleUrls: ['./school-list.component.sass']
})
  export class SchoolListComponent implements OnInit {
  headers:escolaHeader[] = [
    "escolaId",
    "nomeEscola",
    "enderecoEscola",
    "telefoneEscola",
  ];
  // tableHeader:tableHeader = {
  //   escolaId:"ID Escola",
  //   nomeEscola:"Nome da Escola",
  //   enderecoEscola:"Telefone",
  //   telefoneEscola:"Endereço",
  //   actions: "Actions"
  // };
  listaEscolas:escolaData[] = []

  constructor() { 
  }

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    School.fetchAll().then(response => this.listaEscolas=response);
  }

  deleteSchool(id: string) {
    School.deleteById(id).then(() => {
      console.log('Post deleted')
      this.fetchData();
    })
    .catch(e => {
      console.log('Não foi possível deletar :C');
    }) 
  }

}
