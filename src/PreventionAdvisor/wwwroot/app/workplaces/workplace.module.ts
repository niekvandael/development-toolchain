import { NgModule } from '@angular/core';
import { RouterModule} from '@angular/router';

import { WorkplaceFilterPipe } from './list/workplace-filter.pipe';
import { WorkplaceListComponent } from './list/workplace-list.component';
import { WorkplaceInputComponent } from './input/workplace-input.component';

import { WorkplaceService } from './workplace.service';

import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild([
      { path: 'workplaces', component: WorkplaceListComponent },
      { path: 'workplace/:id', component: WorkplaceInputComponent },
   ])
  ],
  declarations: [
    WorkplaceFilterPipe,
    WorkplaceListComponent,
    WorkplaceInputComponent,
  ],
  providers: [
    WorkplaceService,
  ]
})
export class WorkplaceModule {}
