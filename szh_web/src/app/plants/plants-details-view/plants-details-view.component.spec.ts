import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlantsDetailsViewComponent } from './plants-details-view.component';

describe('PlantsDetailsViewComponent', () => {
  let component: PlantsDetailsViewComponent;
  let fixture: ComponentFixture<PlantsDetailsViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlantsDetailsViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlantsDetailsViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
