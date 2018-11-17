import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Plant } from '../models/plant.model';
import { ApiUrlConfigurator } from './apiConfig';
import { PlantSpecies } from '../models/PlantSpecies';
import { PlantSpeciesInfo } from '../models/plantSpeciesInfo';


@Injectable()
export class PlantsService {

  constructor(private http: HttpClient) {}

  getPlant(id){
    return this.http.get<Plant>(ApiUrlConfigurator.GetApiUrl() + '/Plants/' + id);
  }

  getPlants(){
    return this.http.get<PlantSpecies[]>(ApiUrlConfigurator.GetApiUrl() + '/Plants');
  }

  getPlantSpeciesWithVarieties(){
    return this.http.get<PlantSpeciesInfo[]>(ApiUrlConfigurator.GetApiUrl() + '/Plants');
  }

  createPlantSpecies(plantSpeciesName){
    return this.http.post(ApiUrlConfigurator.GetApiUrl() + "/Plants/add",plantSpeciesName);
  }

  createVariety(varietyInfo){
    return this.http.post(ApiUrlConfigurator.GetApiUrl() + "/Varieties/add",varietyInfo);
  }
}
