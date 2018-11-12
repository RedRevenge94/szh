import { Component, OnInit } from '@angular/core';
import { PlantsService } from '../../../services/plants.service';
import { ISubscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-plants-view',
  templateUrl: './plants-view.component.html',
  styleUrls: ['./plants-view.component.scss']
})
export class PlantsViewComponent implements OnInit {

plants;
private getPlantsSubscription: ISubscription;

  constructor(private _plantService: PlantsService) { }

  ngOnInit() {
    this.getPlants();
  }

  ngOnDestroy() {
    if (this.getPlantsSubscription) {
      this.getPlantsSubscription.unsubscribe();
    }
  }

  getPlants(){
    this.getPlantsSubscription = this._plantService.getPlants().subscribe(
      data => { this.plants = data },
      err => console.error('Erroren :' + err),
      () => {
      }
    );
  }

}
