import { NgModule } from '@angular/core';
import { RouterModule} from '@angular/router';

import { OrganizationListComponent } from './organization-list.component';
import { OrganizationService } from './organization.service';

import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild([
      { path: 'Organizations', component: OrganizationListComponent },
/*      { path: 'Organization/:id',
        canActivate: [ ],
        component: OrganizationDetailComponent
      }
 */   ])
  ],
  declarations: [
    OrganizationListComponent,
//    OrganizationDetailComponent,
  ],
  providers: [
    OrganizationService,
  ]
})
export class OrganizationModule {}
