import { NgModule } from '@angular/core';
import { RouterModule} from '@angular/router';

import { WorkplaceFilterPipe } from './list/workplace-filter.pipe';
import { WorkplaceListComponent } from './list/workplace-list.component';

import { WorkplaceService } from './workplace.service';

import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild([
      { path: 'workplaces', component: WorkplaceListComponent },
   ])
  ],
  declarations: [
    WorkplaceFilterPipe,
    WorkplaceListComponent,
  ],
  providers: [
    WorkplaceService,
  ]
})
export class WorkplaceModule {}
