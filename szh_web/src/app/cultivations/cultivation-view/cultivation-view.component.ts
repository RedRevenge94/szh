import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CultivationsService } from '../../services/cultivations.service';
import { ISubscription } from 'rxjs/Subscription';
import { CultivationCommentsService } from '../../services/cultivationComments.service';
import { CultivationInfo } from '../../models/cultivationInfo.model';
import { TunnelsService } from '../../services/tunnels.service';
import { trigger,style,transition,animate,keyframes,query,stagger } from '@angular/animations';
import { LoadingSpinnerComponent } from '../../ui/loading-spinner/loading-spinner.component';

@Component({
  selector: 'app-cultivation-view',
  templateUrl: './cultivation-view.component.html',
  styleUrls: ['./cultivation-view.component.scss'],
  animations: [

    trigger('showingAnnimation', [
      transition('* => *', [
        query(':enter',style({opacity: 0}), {optional: true}),
        query(':enter',stagger('150ms',[
          animate('.6s ease-in',keyframes([
            style({opacity: 0,transform: 'translateY(-20%)',offset:0}),
            style({opacity: .5,transform: 'translateY(20px)',offset:.3}),
            style({opacity: 1,transform: 'translateY(0)',offset:1})
          ]))
        ]), {optional: true})
      ])
    ])
  ]
})
export class CultivationViewComponent implements OnInit {

  private loadingSpinner: LoadingSpinnerComponent;

  private intervalId;
  private getCultivationsSubscription: ISubscription;
  private addNewCultivationCommentSubscription: ISubscription;
  private updateCultivationSubscription: ISubscription;
  private getTunnelsSubscription: ISubscription;
  public cultivation: CultivationInfo;
  tunnels;

  commentTextForm;

  cultivationId;

  columns: string[] = ['Text','Created date'];

  //Creating new cultivation
  messageIsShowing: boolean;
  responseIsOk: boolean;

  editMode: boolean;

  constructor(private route: ActivatedRoute,
    private _cultivationInfoService: CultivationsService,
    private _cultivationCommentService: CultivationCommentsService,
    private _tunnelsService: TunnelsService) { 
    this.route.params.subscribe(res => this.cultivationId = res.id);
  }

  ngOnInit() {
    this.loadingSpinner = new LoadingSpinnerComponent();
    this.editMode = false;
    this.getCultivationInfo();
    this.getTunnels();
    this.StartInterval();
  }

  ngOnDestroy(){
    if (this.getCultivationsSubscription) {
      this.getCultivationsSubscription.unsubscribe();
    }
    if (this.getTunnelsSubscription) {
      this.getTunnelsSubscription.unsubscribe();
    }
    if (this.updateCultivationSubscription) {
      this.updateCultivationSubscription.unsubscribe();
    }
    this.StopInterval();
  }

  getCultivationInfo(){

    this.loadingSpinner.startLoadingDate = new Date();

    this.getCultivationsSubscription = this._cultivationInfoService.getCultivationInfo(this.cultivationId).subscribe(
      data => { this.cultivation = data },
      err => console.error('Erroren :' + err),
      () => { 

        let executionTime = this.loadingSpinner.GetLoadingTime();
        if(executionTime < this.loadingSpinner.minLoadingTime){
          setTimeout(()=>{this.loadingSpinner.loadingView = false; }, this.loadingSpinner.minLoadingTime - executionTime)
        }

      }
    );
  }

  onAddNewCultivationCommentSubmit(comment){
    this.addNewCultivationCommentSubscription = this._cultivationCommentService.createCultivationComment(comment).subscribe(
      data => { },
      err => { this.responseIsOk = false; this.messageIsShowing = true; console.log(err);},
      () => { this.responseIsOk = true; this.messageIsShowing = true; }
    );
    this.getCultivationInfo();
  }

  TurnOnOffEditMode(value:boolean){
    if(this.editMode && value){
      this.StartInterval();
      this.editMode = false;
    } else if(this.editMode && !value) {
      //Nie zapisuj
      this.getCultivationInfo();
      this.StartInterval();
      this.editMode = false;
    } else if (!this.editMode){
      this.StopInterval();
      this.editMode = true;
    }
  }

  StartInterval(){
    this.intervalId = setInterval(() => { this.getCultivationInfo(); }, 5000);
  }

  StopInterval(){
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }

  SaveChanges(cultivation){
    cultivation.id = this.cultivation.cultivation.id;
    cultivation.tunnel = this.tunnels.find(x => x.id == cultivation.tunnel)
    this.updateCultivationSubscription = this._cultivationInfoService.updateCultivation(cultivation).subscribe(
      data => { },
      err => { this.responseIsOk = false; this.messageIsShowing = true; console.log(err);},
      () => { this.responseIsOk = true; this.messageIsShowing = true; }
    );
    this.TurnOnOffEditMode(true);
  }

  getTunnels(){
    this.getTunnelsSubscription = this._tunnelsService.getTunnels().subscribe(
      data => {this.tunnels = data},
      err => console.error(err),
      () => {}
    )
  }

}
