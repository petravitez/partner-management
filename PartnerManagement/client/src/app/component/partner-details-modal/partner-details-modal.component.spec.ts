import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PartnerDetailsModalComponent } from './partner-details-modal.component';

describe('PartnerDetailsModalComponent', () => {
  let component: PartnerDetailsModalComponent;
  let fixture: ComponentFixture<PartnerDetailsModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PartnerDetailsModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PartnerDetailsModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
