import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MarketplaceMapComponent } from './marketplace-map.component';

describe('MarketplaceMapComponent', () => {
  let component: MarketplaceMapComponent;
  let fixture: ComponentFixture<MarketplaceMapComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MarketplaceMapComponent]
    });
    fixture = TestBed.createComponent(MarketplaceMapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
