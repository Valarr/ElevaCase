import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SchoolFormComponent } from './school-form/school-form.component';
import { SchoolListComponent } from './school-list/school-list.component';

const routes: Routes = [
  { path: '', component: SchoolListComponent },
  { path: 'new', component: SchoolFormComponent },
  { path: ':id/edit', component: SchoolFormComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class SchoolRoutingModule {}
