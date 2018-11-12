import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { ApiUrlConfigurator } from './apiConfig';


@Injectable()
export class CultivationCommentsService {

  constructor(private http: HttpClient) {}

  createCultivationComment(comment){
    return this.http.post(ApiUrlConfigurator.GetApiUrl() + "/CultivationComments/",comment);
  }

}
