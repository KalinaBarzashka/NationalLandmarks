import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './layout/layout.component';
import { AppRoutingModule } from '../app-routing.module';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';



@NgModule({
  declarations: [
    LayoutComponent,
    PageNotFoundComponent
  ],
  imports: [
    CommonModule,
    AppRoutingModule
  ],
  exports: [
    LayoutComponent,
    PageNotFoundComponent
  ]
})
export class CoreModule { }
