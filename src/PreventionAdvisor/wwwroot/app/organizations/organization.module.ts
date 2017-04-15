import { NgModule } from '@angular/core';
import { RouterModule} from '@angular/router';

import { OrganizationFilterPipe } from './list/organization-filter.pipe';
import { OrganizationListComponent } from './list/organization-list.component';
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
        OrganizationFilterPipe

//    OrganizationDetailComponent,
  ],
  providers: [
    OrganizationService,
  ]
})
export class OrganizationModule {}
