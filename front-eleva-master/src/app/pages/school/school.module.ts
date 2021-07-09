import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { SchoolRoutingModule } from './school-routing.module';
import { SchoolListComponent } from './school-list/school-list.component';
import { SchoolFormComponent } from './school-form/school-form.component';

@NgModule({
  imports: [
    CommonModule,
    SchoolRoutingModule,
    ReactiveFormsModule,
  ],
  declarations: [SchoolListComponent, SchoolFormComponent]
})

export class SchoolModule {}
