import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateMarketplaceFormComponent } from './create-marketplace-form.component';

describe('CreateMarketplaceFormComponent', () => {
  let component: CreateMarketplaceFormComponent;
  let fixture: ComponentFixture<CreateMarketplaceFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateMarketplaceFormComponent]
    });
    fixture = TestBed.createComponent(CreateMarketplaceFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
