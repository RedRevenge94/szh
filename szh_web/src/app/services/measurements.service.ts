import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Plant } from '../models/plant.model';
import { ApiUrlConfigurator } from './apiConfig';
import { PlantSpecies } from '../models/PlantSpecies';
import { PlantSpeciesInfo } from '../models/plantSpeciesInfo';
import { MeasurementType } from '../models/measurementType.model';


@Injectable()
export class MeasurementsService {

  constructor(private http: HttpClient) {}

  getMeasurementTypes(){
    return this.http.get<MeasurementType[]>(ApiUrlConfigurator.GetApiUrl() + '/Measurement/types');
  }
}
