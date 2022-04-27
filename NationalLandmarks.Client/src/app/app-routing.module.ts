import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PageNotFoundComponent } from './core/page-not-found/page-not-found.component';
import { CreateComponent } from './landmark/create/create.component';
import { DetailsComponent } from './landmark/details/details.component';
import { EditComponent } from './landmark/edit/edit.component';
import { ListLandmarksComponent } from './landmark/list-landmarks/list-landmarks.component';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { AuthGuardService } from './services/auth-guard.service';
import { VisitComponent } from './user/visit/visit.component';
import { ProfileComponent } from './user/profile/profile.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: ListLandmarksComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'profile',
    component: ProfileComponent
  },
  {
    path: 'landmark/create',
    component: CreateComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'landmarks/:id',
    component: ListLandmarksComponent,
    //canActivate: [AuthGuardService]
  },
  {
    path: 'landmarks/details/:id',
    component: DetailsComponent,
    //canActivate: [AuthGuardService]
  },
  {
    path: 'landmarks/:id/edit',
    component: EditComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'visits',
    component: VisitComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: '**',
    pathMatch: 'full',
    component: PageNotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
