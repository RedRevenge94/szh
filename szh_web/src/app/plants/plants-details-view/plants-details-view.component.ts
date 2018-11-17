import { Component, OnInit } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';
import { PlantsService } from '../../services/plants.service';
import { ActivatedRoute } from '@angular/router';
import { Plant } from '../../models/plant.model';

@Component({
  selector: 'app-plants-details-view',
  templateUrl: './plants-details-view.component.html',
  styleUrls: ['./plants-details-view.component.scss']
})
export class PlantsDetailsViewComponent implements OnInit {

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
    this.getPlant();
   }

  ngOnInit() {
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
    this.getPlantSubscription = this._plantService.getPlant(this.plantId).subscribe(
      data => { this.plant = data },
      err => console.error('Erroren :' + err),
      () => {
      }
    );
  }

  onAddNewVarietySubmit(varietyInfo){
    console.log(varietyInfo);
    this.addNewVarietySubscription = this._plantService.createVariety(varietyInfo).subscribe(
    );
  }

}
