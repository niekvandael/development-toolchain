import { NgModule } from '@angular/core';
import { RouterModule} from '@angular/router';

import { OrganizationFilterPipe } from './list/organization-filter.pipe';
import { OrganizationListComponent } from './list/organization-list.component';
import { OrganizationInputComponent } from './input/organization-input.component';

import { OrganizationService } from './organization.service';

import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild([
      { path: 'organizations', component: OrganizationListComponent },
      { path: 'organization/:id', component: OrganizationInputComponent },
   ])
  ],
  declarations: [
    OrganizationFilterPipe,
    OrganizationListComponent,
    OrganizationInputComponent,
  ],
  providers: [
    OrganizationService,
  ]
})
export class OrganizationModule {}
