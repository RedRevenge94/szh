import { Component, OnInit } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';
import { PlantsService } from '../../services/plants.service';
import { ActivatedRoute } from '@angular/router';
import { Plant } from '../../models/plant.model';
import { trigger,style,transition,animate,keyframes,query,stagger } from '@angular/animations';
import { LoadingSpinnerComponent } from '../../ui/loading-spinner/loading-spinner.component';

@Component({
  selector: 'app-plants-details-view',
  templateUrl: './plants-details-view.component.html',
  styleUrls: ['./plants-details-view.component.scss'],
  animations: [

    trigger('showingAnnimation', [
      transition('* => *', [
        query(':enter',style({opacity: 0}), {optional: true}),
        query(':enter',stagger('150ms',[
          animate('.6s ease-in',keyframes([
            style({opacity: 0,transform: 'translateY(-20%)',offset:0}),
            style({opacity: .5,transform: 'translateY(20px)',offset:.3}),
            style({opacity: 1,transform: 'translateY(0)',offset:1})
          ]))
        ]), {optional: true})
      ])
    ])
  ]
})
export class PlantsDetailsViewComponent implements OnInit {

  private loadingSpinner: LoadingSpinnerComponent;

  plantId;
  plant: Plant;
  private getPlantSubscription: ISubscription;

  //Creating new variety
  newVarietyName: string;
  messageIsShowing: boolean;
  responseIsOk: boolean;
  private addNewVarietySubscription: ISubscription;

  constructor(private route: ActivatedRoute, private _plantService: PlantsService) {
    this.route.params.subscribe(res => this.plantId = res.id);
   }

  ngOnInit() {
    this.loadingSpinner = new LoadingSpinnerComponent();
    this.getPlant();
  }

  ngOnDestro(){
    if (this.getPlantSubscription) {
      this.getPlantSubscription.unsubscribe();
    }
    if (this.addNewVarietySubscription) {
      this.addNewVarietySubscription.unsubscribe();
    }
  }

  getPlant(){

    this.loadingSpinner.startLoadingDate = new Date();

    this.getPlantSubscription = this._plantService.getPlant(this.plantId).subscribe(
      data => { this.plant = data },
      err => console.error('Erroren :' + err),
      () => {

        let executionTime = this.loadingSpinner.GetLoadingTime();
        if(executionTime < this.loadingSpinner.minLoadingTime){
          setTimeout(()=>{this.loadingSpinner.loadingView = false; }, this.loadingSpinner.minLoadingTime - executionTime)
        }

      }
    );
  }

  onAddNewVarietySubmit(varietyInfo){
    console.log(varietyInfo);
    this.addNewVarietySubscription = this._plantService.createVariety(varietyInfo).subscribe(
      data => { },
      err => { this.responseIsOk = false; this.messageIsShowing = true; console.log(err);},
      () => { this.responseIsOk = true; this.messageIsShowing = true; this.getPlant();}
    );
  }

}
