import { Component, OnInit } from '@angular/core';
import { CultivationsService } from '../../services/cultivations.service';
import { ISubscription } from 'rxjs/Subscription';
import { TunnelsService } from '../../services/tunnels.service';
import { PlantsService } from '../../services/plants.service';
import { Router } from '@angular/router';
import { VarietiesService } from '../../services/varieties.service';
import { trigger,style,transition,animate,keyframes,query,stagger } from '@angular/animations';
import { LoadingSpinnerComponent } from '../../ui/loading-spinner/loading-spinner.component';

@Component({
  selector: 'app-cultivations',
  templateUrl: './cultivations.component.html',
  styleUrls: ['./cultivations.component.scss'],
  animations: [

    trigger('showingAnnimation', [
      transition('* => *', [
        query(':enter',style({opacity: 0}), {optional: true}),
        query(':enter',stagger('050ms',[
          animate('.4s ease-in',keyframes([
            style({opacity: 0,transform: 'translateY(-20%)',offset:0}),
            style({opacity: .5,transform: 'translateY(20px)',offset:.3}),
            style({opacity: 1,transform: 'translateY(0)',offset:1})
          ]))
        ]), {optional: true})
      ])
    ])
  ]
})
export class CultivationsComponent implements OnInit {

  private loadingSpinner: LoadingSpinnerComponent;

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
    this.loadingSpinner = new LoadingSpinnerComponent();
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

    this.loadingSpinner.startLoadingDate = new Date();

    this.getCultivationsSubscription = this._cultivationInfoService.getCultivationsBasicInfo().subscribe(
      data => { this.cultivations = data },
      err => console.error('Erroren :' + err),
      () => {
        if (this.cultivations != null && this.cultivationCommentsShowingState == null) {
          this.cultivationCommentsShowingState = new Array(this.cultivations.length).fill(false);
        }

        let executionTime = this.loadingSpinner.GetLoadingTime();
        if(executionTime < this.loadingSpinner.minLoadingTime){
          setTimeout(()=>{this.loadingSpinner.loadingView = false; }, this.loadingSpinner.minLoadingTime - executionTime)
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
    console.log(cultivation);
    //cultivation.plant = this.plants.find(x => x.id == cultivation.plant)
    //cultivation.variety = this.varieties.find( x => x.id == cultivation.variety)
    //cultivation.tunnel = this.tunnels.find(x => x.id == cultivation.tunnel)
    this.addNewCultivationSubscription = this._cultivationInfoService.createCultivation(cultivation).subscribe(
      data => { },
      err => { this.responseIsOk = false; this.messageIsShowing = true; console.log(err);},
      () => { this.responseIsOk = true; this.messageIsShowing = true; }
    );
    this.getCultivations();
  }

  onPlantSelectChange(plantSpeciesId){
    this.getVarieties(plantSpeciesId);
  }

  getVarieties(plantSpeciesId){
    this.getVarietiesSubscription = this._varietiesService.getVarietiesForPlant(plantSpeciesId).subscribe(
      data => {this.varieties = data},
      err => console.error(err),
      () => {}
    )
  }

  onPlantTunnelDetails(plantId){
    this.router.navigate(['plants/' + plantId]);
  }

  onShowTunnelDetails(tunnelId){
    this.router.navigate(['tunnels/' + tunnelId]);
  }
}
