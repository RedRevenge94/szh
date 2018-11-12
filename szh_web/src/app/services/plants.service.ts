import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Plant } from '../models/plant.model';
import { ApiUrlConfigurator } from './apiConfig';


@Injectable()
export class PlantsService {

  constructor(private http: HttpClient) {}

  getPlants(){
    return this.http.get<Plant>(ApiUrlConfigurator.GetApiUrl() + '/Plants');
  }
}
