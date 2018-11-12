import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { CultivationInfo } from '../models/cultivationInfo.model';
import { CultivationBasicInfo } from '../models/cultivationBasicInfo.model';
import { ApiUrlConfigurator } from './apiConfig';


@Injectable()
export class CultivationsService {

  constructor(private http: HttpClient) {}

  getCultivationInfo(id){
    return this.http.get<CultivationInfo>(ApiUrlConfigurator.GetApiUrl()  + '/CultivationsInfo/' + id);
  }

  getCultivationsBasicInfo(){
    return this.http.get<CultivationBasicInfo>(ApiUrlConfigurator.GetApiUrl()  + '/CultivationsInfo/basic');
  }

  createCultivation(cultivation){
    console.log(cultivation);
    return this.http.post(ApiUrlConfigurator.GetApiUrl() + "/CultivationsInfo/",cultivation);
  }

  updateCultivation(cultivation){
    return this.http.put(ApiUrlConfigurator.GetApiUrl() + "/CultivationsInfo/",cultivation);
  }

}
