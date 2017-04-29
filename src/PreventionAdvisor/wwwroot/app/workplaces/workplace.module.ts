import { NgModule } from '@angular/core';
import { RouterModule} from '@angular/router';

import { WorkplaceFilterPipe } from './list/workplace-filter.pipe';
import { ChecklistItemFilterPipe } from './detail/checklistItem-filter.pipe';
import { ChecklistItemCategoryFilterPipe } from './detail/checklistItemCategory-filter.pipe';

import { WorkplaceListComponent } from './list/workplace-list.component';
import { WorkplaceInputComponent } from './input/workplace-input.component';
import { WorkplaceDetailComponent } from './detail/workplace-detail.component';

import { WorkplaceService } from './workplace.service';

import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild([
      { path: 'workplaces', component: WorkplaceListComponent },
      { path: 'workplace/:id', component: WorkplaceInputComponent },
      { path: 'workplace/detail/:id', component: WorkplaceDetailComponent },
   ])
  ],
  declarations: [
    WorkplaceFilterPipe,
    ChecklistItemFilterPipe,
    ChecklistItemCategoryFilterPipe,
    WorkplaceListComponent,
    WorkplaceInputComponent,
    WorkplaceDetailComponent,
  ],
  providers: [
    WorkplaceService,
  ]
})
export class WorkplaceModule {}
