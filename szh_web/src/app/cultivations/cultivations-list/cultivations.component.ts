import { Component, OnInit } from '@angular/core';
import { CultivationsService } from '../../services/cultivations.service';
import { ISubscription } from 'rxjs/Subscription';
import { TunnelsService } from '../../services/tunnels.service';
import { PlantsService } from '../../services/plants.service';
import { Router } from '@angular/router';
import { VarietiesService } from '../../services/varieties.service';
import { trigger,style,transition,animate,keyframes,query,stagger } from '@angular/animations';
import { LoadingSpinnerComponent } from '../../ui/loading-spinner/loading-spinner.component';
import { TableComponent } from '../../ui/table/table.component';
import { CultivationBasicInfo } from '../../models/cultivationBasicInfo.model';
import { RowTable } from '../../models/ui_models/rowTable.model';
import { FieldTable, FieldTableType } from '../../models/ui_models/fieldTable.model';

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

  public loadingSpinner: LoadingSpinnerComponent;
  public ui_Table: TableComponent;

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
  cultivations: CultivationBasicInfo[];
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
    this.ui_Table = new TableComponent(this.router);
    this.ui_Table.columns = ['Nazwa','Roślina','Odmiana','Ilość','Tunel','Czas startu','Czas końca', 'Status'];
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
        } else{
          this.loadingSpinner.loadingView = false;
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

  onAddNewCultivationSubmit(cultivation){
    console.log(cultivation);
    //cultivation.plant = this.plants.find(x => x.id == cultivation.plant)
    //cultivation.variety = this.varieties.find( x => x.id == cultivation.variety)
    //cultivation.tunnel = this.tunnels.find(x => x.id == cultivation.tunnel)
    this.addNewCultivationSubscription = this._cultivationInfoService.createCultivation(cultivation).subscribe(
      data => { },
      err => { this.responseIsOk = false; this.messageIsShowing = true; console.log(err);},
      () => { this.responseIsOk = true; this.messageIsShowing = true; this.getCultivations();}
    );
    
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

  getCutlivationDetailsLink(id) {
    return 'cultivation/' + id;
  }

  getPlantTunnelDetailsLink(plantId){
    return 'plants/' + plantId;
  }

  getTunnelDetailsLink(tunnelId){
    return 'tunnels/' + tunnelId;
  }

  cultivationBasicInfoArrayToRowArray(){

    let arrayOfRows = new Array<RowTable>();

    for (var v in this.cultivations)
        {  

          let row = new Array<FieldTable>();
          row.push(new FieldTable(this.cultivations[v].cultivation.name,FieldTableType.textLink,
                    this.getCutlivationDetailsLink(this.cultivations[v].cultivation.id)));
          row.push(new FieldTable(this.cultivations[v].cultivation.plant.plantSpecies.name,FieldTableType.textLink,
                    this.getPlantTunnelDetailsLink(this.cultivations[v].cultivation.plant.plantSpecies.id)));
          row.push(new FieldTable("",FieldTableType.text,""));
          row.push(new FieldTable(this.cultivations[v].cultivation.pieces,FieldTableType.text,""));
          row.push(new FieldTable(this.cultivations[v].cultivation.tunnel.name,FieldTableType.textLink,
                    this.getTunnelDetailsLink(this.cultivations[v].cultivation.tunnel.id)));
          row.push(new FieldTable(this.cultivations[v].cultivation.start_date,FieldTableType.textDate,""));
          row.push(new FieldTable(this.cultivations[v].cultivation.end_date,FieldTableType.textDate,""));
          row.push(new FieldTable(this.cultivations[v].online,FieldTableType.onlineStatus,""));

          if(this.cultivations[v].cultivation.plant.variety != null){
            row[2][0] = this.cultivations[v].cultivation.plant.variety.name;
          }

          arrayOfRows.push(new RowTable(row));
        } 
    return arrayOfRows;
  }
}
