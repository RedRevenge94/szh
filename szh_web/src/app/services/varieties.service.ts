import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Variety } from '../models/variety.model';
import { ApiUrlConfigurator } from './apiConfig';


@Injectable()
export class VarietiesService {

  constructor(private http: HttpClient) {}

  getVarietiesForPlant(plantId){
    return this.http.get<Variety>(ApiUrlConfigurator.GetApiUrl() + '/Varieties/' + plantId);
  }
}
