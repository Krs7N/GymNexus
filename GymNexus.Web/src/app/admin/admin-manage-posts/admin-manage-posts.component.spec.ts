import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminManagePostsComponent } from './admin-manage-posts.component';

describe('AdminManagePostsComponent', () => {
  let component: AdminManagePostsComponent;
  let fixture: ComponentFixture<AdminManagePostsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminManagePostsComponent]
    });
    fixture = TestBed.createComponent(AdminManagePostsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
