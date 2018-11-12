import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { TunnelInfo } from '../models/tunnelInfo.model';
import { Tunnel } from '../models/tunnel.model';
import { ApiUrlConfigurator } from './apiConfig';
import { Measurement } from '../models/measurement.model';
import 'rxjs/add/operator/map';

@Injectable()
export class TunnelsService {

  constructor(private http: HttpClient) {}

  getTunnels(){
    return this.http.get<TunnelInfo[]>(ApiUrlConfigurator.GetApiUrl() + '/Tunnels');
  }

  getTunnelsInfo(){
    return this.http.get<TunnelInfo>(ApiUrlConfigurator.GetApiUrl()  + '/TunnelsInfo');
  }

  getTemperatureForLastWeek(tunnelId){
    return this.http.get<Measurement[]>(ApiUrlConfigurator.GetApiUrl() + '/Temperature/tunnel/' + tunnelId);
  }

  createTunnel(tunnelName){
    return this.http.post(ApiUrlConfigurator.GetApiUrl() + "/Tunnels/" + tunnelName,tunnelName);
  }
}