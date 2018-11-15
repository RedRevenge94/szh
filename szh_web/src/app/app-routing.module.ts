import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TunnelsComponent } from './tunnels/tunnelsList/tunnels.component';
import { CultivationsComponent } from './cultivations/cultivations-list/cultivations.component';
import { AvrdevicesComponent } from './avrdevices/avrdevices.component';
import { CultivationViewComponent } from './cultivations/cultivation-view/cultivation-view.component';
import { TunnelDetailsComponent } from './tunnels/tunnelDetailsView/tunnel-details.component';
import { PlantsViewComponent } from './plants/plantsView/plants-view/plants-view.component';
import { PlantsDetailsViewComponent } from './plants/plants-details-view/plants-details-view.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'tunnels',
    component: TunnelsComponent
  },
  {
    path: 'tunnels/:id',
    component: TunnelDetailsComponent
  },
  {
    path: 'cultivations',
    component: CultivationsComponent
  },
  {
    path: 'cultivation/:id',
    component: CultivationViewComponent
  },
  {
    path: 'plants',
    component: PlantsViewComponent
  },
  {
    path: 'plants/:id',
    component: PlantsDetailsViewComponent
  },
  {
    path: 'avrdevices',
    component: AvrdevicesComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
