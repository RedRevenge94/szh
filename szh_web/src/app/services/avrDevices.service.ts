import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { AvrDeviceInfo } from '../models/avrdeviceInfo.model';
import { ApiUrlConfigurator } from './apiConfig';


@Injectable()
export class AvrDevicesService {

  constructor(private http: HttpClient) {}

  getAvrDevicesInfo(){
    return this.http.get<AvrDeviceInfo>(ApiUrlConfigurator.GetApiUrl()  + '/AvrDevicesInfo');
  }

  createAvrDevice(avrDevice){
    console.log(avrDevice);
    return this.http.post(ApiUrlConfigurator.GetApiUrl() + "/AvrDevicesInfo/",avrDevice);
  }

}
