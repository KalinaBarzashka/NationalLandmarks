import { NgModule } from '@angular/core';
import { CreateComponent } from './create/create.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from '../app-routing.module';
import { CommonModule } from '@angular/common';
import { ListLandmarksComponent } from './list-landmarks/list-landmarks.component';
import { DetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';

@NgModule({
  declarations: [
    CreateComponent,
    ListLandmarksComponent,
    DetailsComponent,
    EditComponent
  ],
  imports: [
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    CommonModule
  ]
})
export class LandmarkModule { }
