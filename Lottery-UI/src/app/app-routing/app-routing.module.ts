import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { WinnersListComponent } from '../winners-list/winners-list.component';
import { SubmitCodeComponent } from '../submit-code/submit-code.component';

const routes: Routes = [ // defining the routes for the app
  { path: 'submit-code', component: SubmitCodeComponent },
  { path: 'winners', component: WinnersListComponent },
  { path: '', component: WinnersListComponent } // for the default, empty, route, usually declared last
]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(routes) // more complex routes with .forChildRoutes
  ],
  exports: [
    RouterModule // exporting the RouterModule from this module, so that other modules can use it
  ],
  declarations: []
})
export class AppRoutingModule { }
