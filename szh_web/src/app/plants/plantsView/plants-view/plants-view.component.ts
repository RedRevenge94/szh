import { Component, OnInit } from '@angular/core';
import { PlantsService } from '../../../services/plants.service';
import { ISubscription } from 'rxjs/Subscription';
import { Router } from '@angular/router';
import { LoadingSpinnerComponent } from '../../../ui/loading-spinner/loading-spinner.component';
import { trigger,style,transition,animate,keyframes,query,stagger } from '@angular/animations';

@Component({
  selector: 'app-plants-view',
  templateUrl: './plants-view.component.html',
  styleUrls: ['./plants-view.component.scss'],
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
export class PlantsViewComponent implements OnInit {

  private loadingSpinner: LoadingSpinnerComponent;

  plantSpecies;
  private getPlantsSubscription: ISubscription;

  //Creating new plantSpecies
  newPlantSpeciesName: string;
  messageIsShowing: boolean;
  responseIsOk: boolean;
  private addNewPlantSpeciesSubscription: ISubscription;

  constructor(private router: Router,private _plantService: PlantsService) { }

  ngOnInit() {
    this.loadingSpinner = new LoadingSpinnerComponent();
    this.getPlantSpecies();
  }

  ngOnDestroy() {
    if (this.getPlantsSubscription) {
      this.getPlantsSubscription.unsubscribe();
    }
    if (this.addNewPlantSpeciesSubscription) {
      this.addNewPlantSpeciesSubscription.unsubscribe();
    }
  }

  getPlantSpecies(){

    this.loadingSpinner.startLoadingDate = new Date();

    this.getPlantsSubscription = this._plantService.getPlantSpeciesWithVarieties().subscribe(
      data => { this.plantSpecies = data },
      err => console.error('Erroren :' + err),
      () => {
        let executionTime = this.loadingSpinner.GetLoadingTime();
        if(executionTime < this.loadingSpinner.minLoadingTime){
          setTimeout(()=>{this.loadingSpinner.loadingView = false; }, this.loadingSpinner.minLoadingTime - executionTime)
        }
      }
    );
  }

  showPlantDetails(id){
    this.router.navigate(['plants/' + id]);
  }

  onAddNewPlantSpeciesSubmit(plantSpecies) {
    console.log(JSON.stringify(plantSpecies));
    this.addNewPlantSpeciesSubscription = this._plantService.createPlantSpecies(plantSpecies).subscribe(
      data => { },
      err => { this.responseIsOk = false; this.messageIsShowing = true; },
      () => { this.responseIsOk = true; this.messageIsShowing = true;
        this.getPlantSpecies(); }
    );
  }

}
