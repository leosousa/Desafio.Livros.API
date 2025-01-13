import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AutorListagemComponent } from './autor-listagem.component';

describe('AutorListagemComponent', () => {
  let component: AutorListagemComponent;
  let fixture: ComponentFixture<AutorListagemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AutorListagemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AutorListagemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
