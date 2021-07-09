import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import School from '../../../api/escolaApi';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { SchoolModel } from '../shared/school.model'
@Component({
  selector: 'app-school-form',
  templateUrl: './school-form.component.html',
  styleUrls: ['./school-form.component.sass']
})

export class SchoolFormComponent implements OnInit {
  currentAction!: string;
  schoolForm!: FormGroup;
  pageTitle!: string;
  school: SchoolModel = new SchoolModel();
  submittingForm: boolean = false;
  escolaId: string;

  constructor(private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.escolaId = this.route.snapshot.params.id;
  }

  ngOnInit(): void {
    this.setCurrentAction();
    this.buildSchoolForm();
    this.loadSchool();
  }

  ngAfterContentChecked() {
    this.setPageTitle();
  }

  private setCurrentAction() {
    if (this.route.snapshot.url[0].path === 'new') {
      this.currentAction = 'new';
    } else {
      this.currentAction = 'edit';
    }
  }

  private setPageTitle() {
    if (this.currentAction === 'new') {
      this.pageTitle = 'Cadastro de Nova Escola';
    } else {
      const schoolName = this.school.nomeEscola || '';
      this.pageTitle = `Editando Escola: ${schoolName}`;
    }
  }

  private loadSchool() {
    if (this.currentAction === 'edit') {
      School.fetchById(this.escolaId).then((response) => {
          console.log(response)
          this.school = response;
          this.schoolForm.patchValue(response); // binds loaded category data to CategoryForm
        })
        .catch((e) => {
          console.log(e)
          alert('Ocorreu um erro no servidor, tente mais tarde')
        })
    }
  }

  submitForm() {
    this.submittingForm = true;

    if (this.currentAction === 'new') {
      this.createSchool();
    } else {
      this.updateSchool();
    }
  }

  private createSchool() {
    const schoolM: SchoolModel = Object.assign(
      new SchoolModel(),
      this.schoolForm.value
    );
    delete schoolM.escolaId;

    School.criaEscola(schoolM)
      .then(() => {
        console.log('Criado com sucesso')
        window.history.back()
      })
      .catch((err: any) => console.log(`ERRO ${err}`))
  }

  private updateSchool() {
    const schoolM: SchoolModel = Object.assign(
      new SchoolModel(),
      this.schoolForm.value
    );
    School.update(this.escolaId, schoolM).then(() => window.history.back())
    .catch((e) => console.log(e))
  }
  
  private buildSchoolForm() {
    this.schoolForm = this.formBuilder.group({
      escolaId: [null],
      nomeEscola: [null],
      enderecoEscola: [null],
      telefoneEscola: [null],
    });
  }
  onSelectVoltar() {
    window.history.back();
  }
}
