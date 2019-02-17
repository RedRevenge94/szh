import { Component, OnInit, Input } from '@angular/core';
import { RowTable } from '../../models/ui_models/rowTable.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent implements OnInit {

  @Input() columns: String[];
  @Input() data: RowTable[];

  constructor(private router: Router) {
   }

  ngOnInit() {
  }

  onNavigate(path: string){
    this.router.navigate([path]);
  }

}
