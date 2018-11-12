import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CultivationsComponent } from './cultivations.component';

describe('CultivationsComponent', () => {
  let component: CultivationsComponent;
  let fixture: ComponentFixture<CultivationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CultivationsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CultivationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
