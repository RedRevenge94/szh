import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { HttpClientModule} from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { TunnelsComponent } from './tunnels/tunnelsList/tunnels.component';
import { TunnelsService } from './services/tunnels.service';
import { CultivationsComponent } from './cultivations/cultivations-list/cultivations.component';
import { CultivationsService } from './services/cultivations.service';
import { AvrdevicesComponent } from './avrdevices/avrdevices.component';
import { AvrDevicesService } from './services/avrDevices.service';
import { PlantsService } from './services/plants.service';
import { CultivationCommentsService } from './services/cultivationComments.service';
import { CultivationViewComponent } from './cultivations/cultivation-view/cultivation-view.component';
import { VarietiesService } from './services/varieties.service';
import { TunnelDetailsComponent } from './tunnels/tunnelDetailsView/tunnel-details.component';
import { PlantsViewComponent } from './plants/plantsView/plants-view/plants-view.component';
import { PlantsDetailsViewComponent } from './plants/plants-details-view/plants-details-view.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    TunnelsComponent,
    CultivationsComponent,
    AvrdevicesComponent,
    CultivationViewComponent,
    TunnelDetailsComponent,
    PlantsViewComponent,
    PlantsDetailsViewComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    HttpClientModule
  ],
  providers: [TunnelsService,CultivationsService, AvrDevicesService, PlantsService, CultivationCommentsService,
    VarietiesService],
  bootstrap: [AppComponent]
})
export class AppModule { }
