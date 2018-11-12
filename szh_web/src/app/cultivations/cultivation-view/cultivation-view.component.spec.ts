import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CultivationViewComponent } from './cultivation-view.component';

describe('CultivationViewComponent', () => {
  let component: CultivationViewComponent;
  let fixture: ComponentFixture<CultivationViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CultivationViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CultivationViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
