import { Component, OnInit } from '@angular/core';
import { CultivationsService } from '../../services/cultivations.service';
import { ISubscription } from 'rxjs/Subscription';
import { TunnelsService } from '../../services/tunnels.service';
import { PlantsService } from '../../services/plants.service';
import { Plant } from '../../models/plant.model';
import { CultivationCommentsService } from '../../services/cultivationComments.service';
import { Router } from '@angular/router';
import { VarietiesService } from '../../services/varieties.service';

@Component({
  selector: 'app-cultivations',
  templateUrl: './cultivations.component.html',
  styleUrls: ['./cultivations.component.scss']
})
export class CultivationsComponent implements OnInit {

  //getting data
  private intervalId;
  private getCultivationsSubscription: ISubscription;
  private getTunnelsSubscription: ISubscription;
  private getPlantsSubscription: ISubscription;
  private getVarietiesSubscription: ISubscription;
  private addNewCultivationSubscription: ISubscription;

  cultivationPlantForm;
  cultivationNameForm;
  cultivationVarietyForm;
  cultivationPiecesForm;
  cultivationTunnelForm;
  cultivationStartDateForm;

  plants;
  varieties;
  tunnels;
  cultivations;
  cultivationCommentsShowingState: boolean[];

  //Creating new cultivation
  messageIsShowing: boolean;
  responseIsOk: boolean;

  constructor(
    private _cultivationInfoService: CultivationsService,
    private _tunnelsService: TunnelsService,
    private _plantsService: PlantsService,
    private _varietiesService: VarietiesService,
    private router: Router
  ) { }

  ngOnInit() {
    console.log(this.router.url);
    this.getTunnels();
    this.getPlants();
    this.getCultivations();
    this.intervalId = setInterval(() => { this.getCultivations(); }, 5000);
  }

  ngOnDestroy() {
    if (this.getCultivationsSubscription) {
      this.getCultivationsSubscription.unsubscribe();
    }
    if (this.getTunnelsSubscription) {
      this.getTunnelsSubscription.unsubscribe();
    }
    if (this.getPlantsSubscription) {
      this.getPlantsSubscription.unsubscribe();
    }
    if (this.getVarietiesSubscription) {
      this.getVarietiesSubscription.unsubscribe();
    }
    if (this.addNewCultivationSubscription) {
      this.addNewCultivationSubscription.unsubscribe();
    }
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }

  getCultivations(){
    this.getCultivationsSubscription = this._cultivationInfoService.getCultivationsBasicInfo().subscribe(
      data => { this.cultivations = data },
      err => console.error('Erroren :' + err),
      () => {
        if (this.cultivations != null && this.cultivationCommentsShowingState == null) {
          this.cultivationCommentsShowingState = new Array(this.cultivations.length).fill(false);
        }
      }
    );
  }

  getTunnels(){
    this.getTunnelsSubscription = this._tunnelsService.getTunnels().subscribe(
      data => {this.tunnels = data},
      err => console.error(err),
      () => {}
    )
  }

  getPlants(){
    this.getPlantsSubscription = this._plantsService.getPlants().subscribe(
      data => {this.plants = data},
      err => console.error(err),
      () => {}
    )
  }

  showCutlivationComments(id) {
    this.router.navigate(['cultivation/' + id]);
  }

  onAddNewCultivationSubmit(cultivation){
    cultivation.plant = this.plants.find(x => x.id == cultivation.plant)
    cultivation.variety = this.varieties.find( x => x.id == cultivation.variety)
    cultivation.tunnel = this.tunnels.find(x => x.id == cultivation.tunnel)
    this.addNewCultivationSubscription = this._cultivationInfoService.createCultivation(cultivation).subscribe(
      data => { },
      err => { this.responseIsOk = false; this.messageIsShowing = true; console.log(err);},
      () => { this.responseIsOk = true; this.messageIsShowing = true; }
    );
    this.getCultivations();
  }

  onPlantSelectChange(plantId){
    this.getVarieties(plantId);
  }

  getVarieties(plantId){
    this.getVarietiesSubscription = this._varietiesService.getVarietiesForPlant(plantId).subscribe(
      data => {this.varieties = data},
      err => console.error(err),
      () => {}
    )
  }
}
