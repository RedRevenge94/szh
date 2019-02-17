import { Injectable } from '@angular/core';
import { ApiUrlConfigurator } from './apiConfig';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { NotificationInfo } from '../models/notification.model';

@Injectable()
export class NotificationsService {

  constructor(private http: HttpClient) { }

  getNotifications(){
    return this.http.get<NotificationInfo[]>(ApiUrlConfigurator.GetApiUrl() + '/Notifications/');
  }

  addNotification(notification){
    return this.http.post(ApiUrlConfigurator.GetApiUrl() + '/Notifications/',notification);
  }

}
