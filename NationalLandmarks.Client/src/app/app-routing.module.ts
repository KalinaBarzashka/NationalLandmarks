import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateComponent } from './landmark/create/create.component';
import { DetailsComponent } from './landmark/details/details.component';
import { EditComponent } from './landmark/edit/edit.component';
import { ListLandmarksComponent } from './landmark/list-landmarks/list-landmarks.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuardService } from './services/auth-guard.service';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'landmark/create',
    component: CreateComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'landmarks',
    component: ListLandmarksComponent,
    //canActivate: [AuthGuardService]
  },
  {
    path: 'landmarks/:id',
    component: DetailsComponent,
    //canActivate: [AuthGuardService]
  },
  {
    path: 'landmarks/:id/edit',
    component: EditComponent,
    //canActivate: [AuthGuardService]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
