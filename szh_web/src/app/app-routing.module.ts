import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TunnelsComponent } from './tunnels/tunnelsList/tunnels.component';
import { CultivationsComponent } from './cultivations/cultivations-list/cultivations.component';
import { AvrdevicesComponent } from './avrdevices/avrdevices.component';
import { CultivationViewComponent } from './cultivations/cultivation-view/cultivation-view.component';
import { TunnelDetailsComponent } from './tunnels/tunnelDetailsView/tunnel-details.component';

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
    path: 'avrdevices',
    component: AvrdevicesComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
