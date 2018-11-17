import { Component, OnInit } from '@angular/core';
import { PlantsService } from '../../../services/plants.service';
import { ISubscription } from 'rxjs/Subscription';
import { Router } from '@angular/router';

@Component({
  selector: 'app-plants-view',
  templateUrl: './plants-view.component.html',
  styleUrls: ['./plants-view.component.scss']
})
export class PlantsViewComponent implements OnInit {

  plantSpecies;
  private getPlantsSubscription: ISubscription;

  //Creating new plantSpecies
  newPlantSpeciesName: string;
  messageIsShowing: boolean;
  responseIsOk: boolean;
  private addNewPlantSpeciesSubscription: ISubscription;

  constructor(private router: Router,private _plantService: PlantsService) { }

  ngOnInit() {
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
    this.getPlantsSubscription = this._plantService.getPlantSpeciesWithVarieties().subscribe(
      data => { this.plantSpecies = data },
      err => console.error('Erroren :' + err),
      () => {}
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
