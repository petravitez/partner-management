import { Routes } from '@angular/router';
import { PartnerListComponent } from './component/partner-list/partner-list.component';
import { PartnerCreateComponent } from './component/partner-create/partner-create.component';

export const routes: Routes = [
    { path: '', component: PartnerListComponent },
    { path: 'partners', component: PartnerListComponent },
    { path: 'partners/create', component: PartnerCreateComponent },
  ];
  