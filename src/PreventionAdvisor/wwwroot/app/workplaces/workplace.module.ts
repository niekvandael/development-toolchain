import { NgModule } from '@angular/core';
import { RouterModule} from '@angular/router';

import { WorkplaceFilterPipe } from './list/workplace-filter.pipe';
import { ChecklistItemFilterPipe } from './checklistItem/checklistitem-filter.pipe';
import { WorkplaceListComponent } from './list/workplace-list.component';
import { WorkplaceInputComponent } from './input/workplace-input.component';
import { WorkplaceDetailComponent } from './detail/workplace-detail.component';
import { CheckListItemDefaultsComponent } from './checklistItem/defaults/workplace-checklistItem-defaults.component';

import { WorkplacePrintComponent } from './print/workplace-print.component';
import { CheckListItemListComponent } from './checklistItem/list/workplace-checklistItem-list.component';

import { ModalComponent } from '../shared/modal/modal.component';

import { WorkplaceService } from './workplace.service';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild([
      { path: 'workplaces', component: WorkplaceListComponent },
      { path: 'workplace/:id', component: WorkplaceInputComponent },
      { path: 'workplace/detail/:id', component: WorkplaceDetailComponent },
      { path: 'workplace/print/:id', component: WorkplacePrintComponent },
      { path: 'workplace/workplaceDefaults', component: CheckListItemDefaultsComponent },
   ])
  ],
  declarations: [
    WorkplaceFilterPipe,
    ChecklistItemFilterPipe,
    WorkplaceListComponent,
    WorkplaceInputComponent,
    WorkplaceDetailComponent,
    WorkplacePrintComponent,
    CheckListItemListComponent,
    CheckListItemDefaultsComponent,
    ModalComponent
  ],
  providers: [
    WorkplaceService
    ]
})
export class WorkplaceModule {}
