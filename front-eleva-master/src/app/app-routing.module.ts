import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '', redirectTo: '/school', pathMatch: 'full'
  },
  {
    path: 'school',
    loadChildren: () => import('./pages/school/school.module')
      .then((m) => m.SchoolModule)
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
